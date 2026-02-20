using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InputSystemRebinBall : MonoBehaviour
{
    public InputActionReference jumpAction;
    public PlayerInput playerInput;
    public Text rebindText;
    private Rigidbody2D rb;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        // 从 PlayerPrefs 加载覆盖前，先确保动作已禁用（避免加载时触发事件）
        jumpAction.action.Disable();

        string json = PlayerPrefs.GetString("InputActions", null);
        if (!string.IsNullOrEmpty(json))
        {
            try
            {
                playerInput.actions.LoadBindingOverridesFromJson(json);
                Debug.Log("Loaded binding overrides.");
            }
            catch (System.Exception e)
            {
                Debug.LogError("Failed to load binding overrides: " + e.Message);
                // 如果加载失败，清除损坏的数据
                PlayerPrefs.DeleteKey("InputActions");
            }
        }

        // 重新启用动作
        jumpAction.action.Enable();

        // 更新显示文本
        UpdateRebindText();
    }

    private void UpdateRebindText()
    {
        if (jumpAction.action.bindings.Count > 0 && jumpAction.action.bindings[0].effectivePath != null)
        {
            rebindText.text = InputControlPath.ToHumanReadableString(
                jumpAction.action.bindings[0].effectivePath,
                InputControlPath.HumanReadableStringOptions.OmitDevice
            );
        }
        else
        {
            rebindText.text = "未绑定";
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            rb.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        }
    }

    public void StartRebind()
    {
        Debug.Log("开始重绑定");

        // 切换到 UI 动作映射，防止游戏内输入干扰
        playerInput.SwitchCurrentActionMap("UI");
        rebindText.text = "请输入新按键...";

        // 禁用要重绑定的动作，避免在重绑定过程中触发事件
        jumpAction.action.Disable();

        // 执行重绑定操作
        var rebindOperation = jumpAction.action.PerformInteractiveRebinding()
            .WithControlsExcluding("Mouse")          // 排除鼠标
            .WithCancelingThrough("<Keyboard>/escape") // 允许按 ESC 取消
            .OnComplete(operation => OnRebindComplete(operation))
            .OnCancel(operation => OnRebindCancel(operation))
            .Start();
    }

    private void OnRebindComplete(InputActionRebindingExtensions.RebindingOperation operation)
    {
        Debug.Log("重绑定完成");

        // 重新启用动作
        jumpAction.action.Enable();

        // 更新显示文本
        UpdateRebindText();

        // 保存覆盖到 PlayerPrefs
        try
        {
            string json = playerInput.actions.SaveBindingOverridesAsJson();
            PlayerPrefs.SetString("InputActions", json);
            PlayerPrefs.Save();
            Debug.Log("覆盖已保存");
        }
        catch (System.Exception e)
        {
            Debug.LogError("保存覆盖失败: " + e.Message);
        }

        // 切换回游戏动作映射
        playerInput.SwitchCurrentActionMap("GamePlay");

        // 释放操作资源
        operation.Dispose();
    }

    private void OnRebindCancel(InputActionRebindingExtensions.RebindingOperation operation)
    {
        Debug.Log("重绑定取消");

        // 重新启用动作
        jumpAction.action.Enable();

        // 恢复原来的显示文本
        UpdateRebindText();

        // 切换回游戏动作映射
        playerInput.SwitchCurrentActionMap("GamePlay");

        // 释放操作资源
        operation.Dispose();
    }
}