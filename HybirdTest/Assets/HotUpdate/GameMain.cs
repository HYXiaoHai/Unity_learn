using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class GameMain : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("我是主场景");
        Addressables.LoadAssetAsync<GameObject>("2").Completed += CheckUpdateAssets_Completed;

    }

    private void CheckUpdateAssets_Completed(AsyncOperationHandle<GameObject> handle)
    {
        var a = Instantiate(handle.Result);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
