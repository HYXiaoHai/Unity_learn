using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using YooAsset;

public class YooTest : MonoBehaviour
{
    private string packageName = "TestPackage";
    private string assetLocation = "YooGreenCube";

    private async void Start()
    {
        // 1. 初始化 YooAsset 系统
        YooAssets.Initialize();

        // 2. 获取或创建包
        var package = YooAssets.TryGetPackage(packageName);
        if (package == null)
            package = YooAssets.CreatePackage(packageName);

        // 3. ★ 使用模拟构建生成清单，并获取清单所在目录 ★
        var buildResult = EditorSimulateModeHelper.SimulateBuild(packageName);
        string packageRoot = buildResult.PackageRootDirectory; // 例如：Library/YooAsset/TestPackage/Simulate/

        // 4. 创建编辑器模拟模式的初始化参数
        var initParameters = new EditorSimulateModeParameters();
        initParameters.EditorFileSystemParameters = FileSystemParameters.CreateDefaultEditorFileSystemParameters(packageRoot);

        // 5. 初始化包
        var initOp = package.InitializeAsync(initParameters);
        await initOp.Task;

        if (initOp.Status != EOperationStatus.Succeed)
        {
            Debug.LogError($"包初始化失败：{initOp.Error}");
            return;
        }

        Debug.Log("资源包初始化成功！");

        // 6. 设为默认包（可选）
        YooAssets.SetDefaultPackage(package);

        // 7. 加载预制体
        var handle = package.LoadAssetAsync<GameObject>(assetLocation);
        await handle.Task;

        if (handle.Status == EOperationStatus.Succeed)
        {
            GameObject go = handle.InstantiateSync();
            go.transform.position = Vector3.zero;
            Debug.Log("绿色方块加载成功！");
        }
        else
        {
            Debug.LogError($"加载失败：{handle.LastError}");
        }

        // 8. 释放句柄（如果不需长期持有）
        handle.Release();
    }
}
