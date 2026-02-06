using System.Collections;
using System.Collections.Generic;
using System.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class roleProgressDetails : MonoBehaviour
{
    public RoleData roleData;
    public GameProgressData.CharacterStats characterStats;
    public Image _profile;//头像
    public TMP_Text _name;//名字
    public TMP_Text _playCount;//游玩次数
    public TMP_Text _winCount;//获胜次数
    public TMP_Text _highestWave;//最高波数
    public TMP_Text _highestDifficultyId;//最高难度

    private void Awake()
    {
        _profile = GetComponentsInChildren<Image>()[1];
        _name = GetComponentsInChildren<TMP_Text>()[0];//第一个子对象
        _playCount = GetComponentsInChildren<TMP_Text>()[1];//第二个子对象
        _winCount = GetComponentsInChildren<TMP_Text>()[2];//第三个子对象
        _highestWave = GetComponentsInChildren<TMP_Text>()[3];//第四个子对象
        _highestDifficultyId = GetComponentsInChildren<TMP_Text>()[4];//第五个子对象
    }
    public void Init(RoleData data, GameProgressData.CharacterStats stats)
    {
        roleData = data;
        if(stats != null)
        {
            characterStats = stats;
        }
        else
        {

            GameProgressData.CharacterStats initstst =  new GameProgressData.CharacterStats
            {
                characterId = roleData.id,
                characterName = roleData.name,
                playCount = 0,
                winCount = 0,
                highestWave = 0,
                highestDifficultyId = 0,
            };
            characterStats = initstst;
        }

        _profile.sprite = Resources.Load<Sprite>(roleData.avatar);
        _name.text = roleData.name;
        _playCount.text = characterStats.playCount.ToString();
        _winCount.text = characterStats.winCount.ToString();
        _highestWave.text = characterStats.highestWave.ToString();
        _highestDifficultyId.text = characterStats.highestDifficultyId.ToString();
    }
}
