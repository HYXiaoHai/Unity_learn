using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;


public class CheckAssetUpate : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(FetchRemoteLabelDownloadSize());
       
    }

    private IEnumerator FetchRemoteLabelDownloadSize()
    {
        AsyncOperationHandle<long> downloadSizeOpHandel = Addressables.GetDownloadSizeAsync("all");
        yield return downloadSizeOpHandel;
        if (downloadSizeOpHandel.Status == AsyncOperationStatus.Succeeded)
        {
            if (downloadSizeOpHandel.Result <= 0)
            {
                Debug.Log("没有更新");
                //直接进入游戏
                EnterGame();
            }
            else
            {
                Debug.Log("有更新");
                //下载
                StartCoroutine(DownloadDependencies());
            }
        }
    }

    private IEnumerator DownloadDependencies()
    {
        AsyncOperationHandle remoteAssetsDownloadDepenciesOpHandle = Addressables.DownloadDependenciesAsync("all");
        while (!remoteAssetsDownloadDepenciesOpHandle.IsDone)
        {
            var dowloadedBytes = remoteAssetsDownloadDepenciesOpHandle.GetDownloadStatus().DownloadedBytes;
            var totalBytes = remoteAssetsDownloadDepenciesOpHandle.GetDownloadStatus().TotalBytes;
            Debug.Log(Mathf.Round(dowloadedBytes / 1048579f * 100) / 100);
            var status = remoteAssetsDownloadDepenciesOpHandle.GetDownloadStatus();
            float progress = status.Percent;
            Debug.Log(progress);
            yield return null;
        }
        if (remoteAssetsDownloadDepenciesOpHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Addressables.Release(remoteAssetsDownloadDepenciesOpHandle);
            //进入游戏
            EnterGame();
        }
    }
    private async Task EnterGame()
    {
        var loaDllAsync = Addressables.LoadAssetAsync<TextAsset>("HotUpdate.dll");
        await loaDllAsync.Task;
        Assembly hotUpdateAss = Assembly.Load(loaDllAsync.Result.bytes);
        Debug.Log("转移场景");
        AsyncOperationHandle<SceneInstance> lastloadHandle = Addressables.LoadSceneAsync("01-MainMenu", LoadSceneMode.Single,true);//原游戏主菜单
        
        //lastloadHandle.Completed += (AsyncOperationHandle<SceneInstance> op) =>
        //{
        //    if (op.Status == AsyncOperationStatus.Succeeded)
        //    {
        //        GameObject kong = new GameObject("GameMain");
        //        kong.AddComponent<GameMain>();
        //    }
        //    //加载场景
        //};

    }

    private void Update()
    {
        
    }
    //边玩边下
    //private void Start()
    //{
    //    //Addressables.LoadAssetAsync<GameObject>("Man").Completed += LoadAssets_Completed;
    //    Addressables.LoadAssetAsync<TextAsset>("HotUpdate.dll").Completed += LoadAssets_Completed;

    //}

    //void LoadAssets_Completed(AsyncOperationHandle<TextAsset> handle)
    //{
    //    Assembly hotUpdateAss = Assembly.Load(handle.Result.bytes);
    //    Type type = hotUpdateAss.GetType("Hello");
    //    type.GetMethod("Run").Invoke(null, null);

    //    //var a = Instantiate(handle.Result);
    //}
}
