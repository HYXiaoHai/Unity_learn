using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//
//播放音频的类
//
public class No18_AudioSourse : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip musicClip;
    public AudioClip soudClip;

    private bool pause;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = musicClip;
        audioSource.Play();//播放
        //audioSource.Stop();//停止
        audioSource.volume = 100;//音量
        audioSource.pitch = 1;//快进 1正常 >1快进
        audioSource.time = 3;//在第三秒的位置开始播放。！！！！！！！！！！！！！！！！！
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space)&&pause == false)
        {
            pause = !pause;
            audioSource.mute = !audioSource.mute;//静音 音频还是会播放的
            audioSource.Pause();//暂停
        }
        if(Input.GetKeyDown(KeyCode.Space)&&pause == true)
        {
            pause = !pause;
            audioSource.mute = !audioSource.mute;//静音 音频还是会播放的
            audioSource.UnPause();
        }
        //音效播放一次
        if(Input.GetKeyDown(KeyCode.K))
        {
            //audioSource.PlayOneShot(soudClip);//与背景音乐互相不影响。
            AudioSource.PlayClipAtPoint(soudClip,transform.position);//会实例化一个对象。默认3d立体音
        }

    }
}
