using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using XLua;

public class xLuaMgr : UnitySingleTon<xLuaMgr>
{
    private static string lauScriptFolder = "LuaScripts";
    LuaEnv env = null;
    private bool isGameStarted = false;
    public override void Awake()
    {
        base.Awake();
        InitLuaEnv();
    }

    public byte[] LuaScriptLoader( ref string fileapth )
    {
        string scriptPath = string.Empty;
        fileapth = fileapth.Replace(".","/") + ".lua";//game/init
#if UNITY_EDITOR //编译器模式
        scriptPath = Path.Combine(Application.dataPath, lauScriptFolder);
        scriptPath = Path.Combine(scriptPath, fileapth);

        byte[] data = GameUtility.SafeReadAllBytes(scriptPath);
        return data;
#endif
        // 打包后模式：尝试从 StreamingAssets 加载（可根据需要扩展其他来源）
        // 构建 StreamingAssets 下的完整路径
        string streamingPath = Path.Combine(Application.streamingAssetsPath, lauScriptFolder, fileapth);
        return GameUtility.SafeReadBytesFromStreamingAssets(streamingPath);

        return null;
    }

    // Start is called before the first frame update
    private void InitLuaEnv()
    {
        //添加自定义的lua代码装载器
        env = new LuaEnv();
        env.AddLoader(LuaScriptLoader);
        isGameStarted = false;
    }

    public void EnterGame()
    {
        isGameStarted = true; //游戏正式开始
        

        //进入游戏逻辑 lua代码
        //lua代码：print("Helloworlf")
        //this.env.DoString("print(\"HelloWorld\")");
        this.env.DoString("require(\"main\")");
        this.env.DoString("main.init()");
        //end
    }

    void Update()
    {
        if(isGameStarted)
        {
            env.DoString("main.update()");
        }
    }
    private void FixedUpdate()
    {
        if (isGameStarted)
        {
            env.DoString("main.fixedUpdate()");
        }
    }

    private void LateUpdate()
    {
        if (isGameStarted)
        {
            env.DoString("main.lateUpdate()");
        }
    }
}


public static class GameUtility
{
    /// <summary>
    /// 安全读取文件所有字节（带异常处理）
    /// </summary>
    /// <param name="path">文件路径</param>
    /// <returns>字节数组，如果失败返回 null</returns>
    public static byte[] SafeReadAllBytes(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }
            else
            {
                Debug.LogError($"文件不存在: {path}");
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"读取文件失败: {path}\n{e.Message}");
            return null;
        }
    }

    /// <summary>
    /// 从 StreamingAssets 安全读取文件（支持所有平台，含 Android）
    /// 注意：此方法会阻塞当前线程，适合在加载器中使用。
    /// </summary>
    public static byte[] SafeReadBytesFromStreamingAssets(string path)
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        // Android 平台必须使用 UnityWebRequest
        try
        {
            UnityWebRequest request = UnityWebRequest.Get(path);
            // 发送请求并等待完成（同步方式）
            request.SendWebRequest();
            while (!request.isDone) { /* 等待 */ }

            if (request.result == UnityWebRequest.Result.Success)
            {
                return request.downloadHandler.data;
            }
            else
            {
                Debug.LogError($"从 StreamingAssets 读取失败: {path}\n{request.error}");
                return null;
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Android 读取异常: {path}\n{e.Message}");
            return null;
        }
#else
        // 其他平台（Windows、macOS、iOS、Linux）可直接文件 IO
        return SafeReadAllBytes(path);
#endif
    }
}