using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//
//
//资源加载
//
//需要创建固定文件夹

public class No20_Resourses : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //<类型>（文件相对路径（目前在Resources文件夹））
        AudioClip audioClip =  Resources.Load<AudioClip>("sound");
        AudioSource.PlayClipAtPoint(audioClip, transform.position);
        //AudioSource.PlayClipAtPoint(Resources.Load<AudioClip>("sound"), transform.position);

        //加载预制体：
        Instantiate(Resources.Load<GameObject>(@"Prefabs/Gris"));
        Instantiate(Resources.Load<GameObject>("Prefabs/Gris"));

        //加载的资源无法直接使用
        Object obj = Resources.Load("sound");
        AudioClip ac = obj as AudioClip;//转换类型
        //AudioClip ac = (AudioClip)obj;//强制转换类型
        AudioSource.PlayClipAtPoint(ac, transform.position);

        Resources.LoadAll<AudioClip>("Prefabs");//加载Prefabs下所有的AudioClip文件 返回的是数组。
       AudioClip[] audioClips =  Resources.LoadAll<AudioClip>("");//加载根目录下下所有的AudioClip文件
        foreach (AudioClip clip in audioClips)
        {
            Debug.Log(clip);
        }
        foreach (var item in audioClips)
        {
            Debug.Log(item);
        }

        //异步加载



        //卸载 一般用不到
        //卸载ab包
        Resources.UnloadAsset(audioClip);//卸载
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
