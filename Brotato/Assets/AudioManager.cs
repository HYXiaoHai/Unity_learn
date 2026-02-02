using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class SimpleAudioManager : MonoBehaviour
{
    public static SimpleAudioManager Instance;

    [Header("背景音乐")]
    public AudioClip menuMusic;        // 菜单场景音乐（场景1、2）
    public AudioClip gameplayMusic;    // 游戏场景音乐（场景3、4）

    [Header("音频源")]
    public AudioSource musicSource;    // 背景音乐源

    private string currentMusicGroup = "";

    void Awake()
    {
        // 单例模式
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            // 初始化音频源
            if (musicSource == null)
            {
                musicSource = GetComponent<AudioSource>();
                musicSource.loop = true;
                musicSource.playOnAwake = false;
            }

            // 监听场景切换事件
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        menuMusic = Resources.Load<AudioClip>("Music/背景音乐");
        gameplayMusic = Resources.Load<AudioClip>("Music/背景音乐");
    }

    void OnDestroy()
    {
        // 取消事件订阅
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // 场景加载完成时的回调
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        HandleSceneMusic(scene.buildIndex);
    }

    // 处理场景音乐
    void HandleSceneMusic(int sceneIndex)
    {
        // 定义场景组
        bool isMenuGroup = (sceneIndex == 0 || sceneIndex == 1);     // 场景1、2
        bool isGameplayGroup = (sceneIndex == 2 || sceneIndex == 3); // 场景3、4

        if (isMenuGroup)
        {
            if (currentMusicGroup != "Menu")
            {
                PlayMusic(menuMusic, "Menu");
            }
        }
        else if (isGameplayGroup)
        {
            if (currentMusicGroup != "Gameplay")
            {
                PlayMusic(gameplayMusic, "Gameplay");
            }
        }
    }

    // 播放音乐
    void PlayMusic(AudioClip clip, string musicGroup)
    {
        if (clip == null) return;

        currentMusicGroup = musicGroup;

        // 如果已经在播放同一首音乐，则不重复播放
        if (musicSource.clip == clip && musicSource.isPlaying)
        {
            Debug.Log($"音乐 {clip.name} 已在播放，跳过");
            return;
        }

        // 直接切换音乐
        musicSource.clip = clip;
        musicSource.Play();

        Debug.Log($"开始播放: {clip.name}");
    }

    // 暂停/恢复音乐
    public void ToggleMusic()
    {
        if (musicSource.isPlaying)
        {
            PauseMusic();
        }
        else
        {
            ResumeMusic();
        }
    }

    public void PauseMusic()
    {
        musicSource.Pause();
    }

    public void ResumeMusic()
    {
        musicSource.UnPause();
    }

    // 停止音乐
    public void StopMusic()
    {
        musicSource.Stop();
        currentMusicGroup = "";
    }

    // 设置音乐音量
    public void SetMusicVolume(float volume)
    {
        musicSource.volume = Mathf.Clamp01(volume);
    }

    // 静音/取消静音
    public void MuteMusic(bool mute)
    {
        musicSource.mute = mute;
    }

    // 获取当前播放的音乐组
    public string GetCurrentMusicGroup()
    {
        return currentMusicGroup;
    }

    // 是否正在播放音乐
    public bool IsMusicPlaying()
    {
        return musicSource.isPlaying;
    }
}