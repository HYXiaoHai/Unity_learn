using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using static UnityEngine.Rendering.VirtualTexturing.Debugging;

public class AddressableLearn : MonoBehaviour
{
    //不能将代码作为可寻址资源

    //右键资源时菜单内容
    //Move Addressables to Group...移入到现有的另一个组中
    //Move Addressables to New Group with settings from...//使用当前组相同设置创建一个新组。并移入
    //Remove Addressables移除资源
    //Simplify Addressable Names简化可寻址资源名 删除路径和拓展
    //Copy Address to Clipboard 复制地址名
    //Change Address 改名
    //Create New Group创建新租
    //Clear Content Update Warnings

    public AssetReference assetReference;//所有
    public AssetReferenceAtlasedSprite atlasedSprite;//图集
    public AssetReferenceGameObject referenceGameObject;
    public AssetReferenceSprite sprite;//j精灵图片
    public AssetReferenceTexture referenceTexture;//贴图资源
                                                  //public AssetReferenceT<> 指定的

   async void Start()
    {
        //初始化
        await Addressables.InitializeAsync().Task;//awak 等待结束
        //使用资源的addressable加载以恶搞资源
        var cubPrefabs = await Addressables.LoadAssetAsync<GameObject>("Assets/Resources_moved/Model/Prefabs/GreenCube.prefab").Task; //根据路径加载
        Instantiate(cubPrefabs);

        ////AsyncOperationHandle<GameObject> handel = assetReference.LoadAssetAsync<GameObject>();//异步加载 并记录加载后的对象 监听
        ////handel.Completed += Handel_Complete; //加载成功后会把handle当成参数并调用函数。

        ////lamada
        //assetReference.LoadAssetAsync<GameObject>().Completed += (handle) =>
        //{
        //    //加载成功后，使用加载的资源
        //    if (handle.Status == AsyncOperationStatus.Succeeded)
        //    {
        //        //判断是否加载成功
        //        Instantiate(handle.Result);
        //    }
        //};

        //referenceGameObject.LoadAssetAsync().Completed += (handle) =>
        //{
        //    //对应逻辑
        //};
    }

    private void Handel_Complete(AsyncOperationHandle<GameObject> handle)
    {
        //加载成功后，使用加载的资源
        if(handle.Status == AsyncOperationStatus.Succeeded)
        {
            //判断是否加载成功
            Instantiate(handle.Result);
        }
    }
}
