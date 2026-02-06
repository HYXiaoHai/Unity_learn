using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    public static SaveManager Instance { get; private set; }

    private GameProgressData progressData;
    private const string SAVE_FILE_NAME = "game_progress.json";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadProgress();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 加载存档
    public void LoadProgress()
    {
        string filePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);

        try
        {
            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                progressData = JsonUtility.FromJson<GameProgressData>(json);
                Debug.Log("进度加载成功");
            }
            else
            {
                progressData = new GameProgressData();
                SaveProgress();
                Debug.Log("创建新存档");
            }
        }
        catch (Exception e)
        {
            Debug.LogError($"加载存档失败: {e.Message}");
            progressData = new GameProgressData();
        }
    }

    // 保存存档
    public void SaveProgress()
    {
        try
        {
            progressData.saveTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
            string json = JsonUtility.ToJson(progressData, true);
            string filePath = Path.Combine(Application.persistentDataPath, SAVE_FILE_NAME);
            File.WriteAllText(filePath, json);
            Debug.Log("进度保存成功");
        }
        catch (Exception e)
        {
            Debug.LogError($"保存存档失败: {e.Message}");
        }
    }

    // 重置存档
    public void ResetProgress()
    {
        progressData = new GameProgressData();
        SaveProgress();
        Debug.Log("存档已重置");
    }

    // 保存当前游戏进度
    public void SaveCurrentGame()
    {
        if (GameManage.Instance == null)
        {
            Debug.LogWarning("无法保存：GameManage不存在");
            return;
        }

        // 标记有当前游戏
        progressData.hasCurrentGame = true;

        // 直接保存各种Data
        progressData.currentRole = GameManage.Instance.currentRole;
        progressData.currentAttribute = GameManage.Instance.currentAttribute;
        progressData.currentDifficulty = GameManage.Instance.currentDifficulty;

        // 保存武器（需要深度复制）
        progressData.currentWeapons = new List<WeaponData>();
        foreach (var weapon in GameManage.Instance.currentWeapon)
        {
            progressData.currentWeapons.Add(weapon.Clone());
        }

        // 保存道具（直接引用，因为PropData是简单数据）
        progressData.currentProps = new List<PropData>();
        foreach (var prop in GameManage.Instance.currentProp)
        {
            progressData.currentProps.Add(prop);
        }

        // 保存其他状态
        progressData.lockedItems = new List<ShopLock>(GameManage.Instance.lockedPropIds);
        progressData.currentWave = GameManage.Instance.currentWave;
        progressData.currentMoney = GameManage.Instance.currentMoney;
        progressData.slot = GameManage.Instance.slot;
        progressData.shopDiscount = GameManage.Instance.shopDiscount;
        progressData.expMuti = GameManage.Instance.expMuti;

        SaveProgress();
        Debug.Log("游戏进度已保存");
    }

    // 加载当前游戏进度
    public void LoadCurrentGame()
    {
        if (!progressData.hasCurrentGame)
        {
            Debug.Log("没有可继续的游戏");
            return;
        }

        if (GameManage.Instance == null)
        {
            Debug.LogError("GameManage不存在，无法加载存档");
            return;
        }

        // 恢复所有Data
        GameManage.Instance.currentRole = progressData.currentRole;
        GameManage.Instance.currentAttribute = progressData.currentAttribute;
        GameManage.Instance.currentDifficulty = progressData.currentDifficulty;

        // 恢复武器
        GameManage.Instance.currentWeapon = new List<WeaponData>();
        foreach (var weapon in progressData.currentWeapons)
        {
            GameManage.Instance.currentWeapon.Add(weapon.Clone());
        }

        // 恢复道具
        GameManage.Instance.currentProp = new List<PropData>(progressData.currentProps);

        // 恢复其他状态
        GameManage.Instance.lockedPropIds = new List<ShopLock>(progressData.lockedItems);
        GameManage.Instance.currentWave = progressData.currentWave;
        GameManage.Instance.currentMoney = progressData.currentMoney;
        GameManage.Instance.slot = progressData.slot;
        GameManage.Instance.shopDiscount = progressData.shopDiscount;
        GameManage.Instance.expMuti = progressData.expMuti;

        Debug.Log("游戏进度已加载");
    }

    // 开始新游戏
    public void StartNewGame(RoleData role, DifficutyData difficulty)
    {
        // 更新全局统计
        progressData.totalPlayCount++;

        // 更新角色统计
        UpdateCharacterStat(role.id, role.name, false, 0, 0);

        // 清空当前游戏标记
        progressData.hasCurrentGame = false;

        SaveProgress();
    }

    // 游戏胜利
    public void GameWon(int characterId, int wave, int difficultyId)
    {
        progressData.totalWinCount++;
        UpdateCharacterStat(characterId, null, true, wave, difficultyId);

        // 检查是否解锁角色4（完成一局游戏）
        if (!progressData.unlockedCharacters.Contains(4))
        {
            progressData.unlockedCharacters.Add(4);
            Debug.Log("解锁角色：多面手");
        }

        // 游戏结束，清除当前游戏标记
        progressData.hasCurrentGame = false;

        SaveProgress();
    }

    // 游戏失败
    public void GameLost(int characterId, int wave, int difficultyId)
    {
        UpdateCharacterStat(characterId, null, false, wave, difficultyId);

        // 游戏结束，清除当前游戏标记
        progressData.hasCurrentGame = false;

        SaveProgress();
    }

    // 检查是否解锁角色5（最大生命值达到50）
    public void CheckMaxHealthUnlock(float maxHealth)
    {
        if (!progressData.unlockedCharacters.Contains(5) && maxHealth >= 50)
        {
            progressData.unlockedCharacters.Add(5);
            Debug.Log("解锁角色：公牛");
            SaveProgress();
        }
    }

    // 更新角色统计
    private void UpdateCharacterStat(int characterId, string characterName, bool isWin, int wave, int difficultyId)
    {
        var stat = progressData.characterStats.Find(s => s.characterId == characterId);
        if (stat == null)
        {
            stat = new GameProgressData.CharacterStats
            {
                characterId = characterId,
                characterName = characterName ?? $"角色{characterId}"
            };
            progressData.characterStats.Add(stat);
        }

        stat.playCount++;
        if (isWin) stat.winCount++;

        if (wave > stat.highestWave)
        {
            stat.highestWave = wave;
        }

        if (difficultyId > stat.highestDifficultyId)
        {
            stat.highestDifficultyId = difficultyId;
        }
    }

    // 获取存档数据
    public GameProgressData GetProgressData()
    {
        return progressData;
    }

    // 是否有可继续的游戏
    public bool HasContinueGame()
    {
        return progressData.hasCurrentGame;
    }

    // 获取角色统计数据
    public GameProgressData.CharacterStats GetCharacterStats(int characterId)
    {
        return progressData.characterStats.Find(s => s.characterId == characterId);
    }

    // 获取已解锁角色列表
    public List<int> GetUnlockedCharacters()
    {
        return progressData.unlockedCharacters;
    }

    // 退出时自动保存
    private void OnApplicationQuit()
    {
        // 如果游戏正在进行中，自动保存
        if (GameManage.Instance != null && GameManage.Instance.currentRole != null)
        {
            SaveCurrentGame();
            Debug.Log("游戏退出，自动保存进度");
        }
    }

    // 移动端暂停时保存
    private void OnApplicationPause(bool pauseStatus)
    {
        if (pauseStatus && GameManage.Instance != null && GameManage.Instance.currentRole != null)
        {
            SaveCurrentGame();
        }
    }
}