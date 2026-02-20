using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
[Serializable]
public class InputSystemMultiData
{
    public int _index;
    public int _id;
    public InputDevice _device;//设备
    public Action onJump;
    public Vector2 onMove;
    public InputSystemMultiData(int i, InputDevice device)
    {
        _index = i;
        _device = device;
        _id = device.deviceId;
    }
}
public class InputSystemMultifer : MonoBehaviour
{
    public static InputSystemMultifer instance;
    public List<InputSystemMultiData> datas = new List<InputSystemMultiData>();

    private MyInputAction myInputAction;

    private void Awake()
    {
        instance = this;

        myInputAction = new MyInputAction();
        myInputAction.GamePlay.MoveMent.performed += OnMove;
        myInputAction.GamePlay.Jump.performed += Onjump;
        myInputAction.Enable();
    }

   public InputSystemMultiData GetdataByIndex(int index)
    {
        if(index >= 0&& index <= datas.Count -1)
        {
            Debug.Log("获取成功：" + index);
            return datas[index];
        }
        Debug.Log("获取失败：" + index);
        return null;
    }

    public int GetIndex(InputDevice device)
    {
       
        for (int i = 0; i < datas.Count; i++)
        {
            if (datas[i]._id == device.deviceId)
            {
                return i;
            }
        }
        InputSystemMultiData data = new InputSystemMultiData(datas.Count, device);
        datas.Add(data);
        return data._index;
    }
    private void Onjump(InputAction.CallbackContext context)
    {
        
        InputDevice device = context.control.device;
        int index = GetIndex(device);
        datas[index].onJump?.Invoke();
    }

    private void OnMove(InputAction.CallbackContext context)
    {
        Debug.Log("move");
        InputDevice device = context.control.device;
        int index = GetIndex(device);
        datas[index].onMove = context.ReadValue<Vector2>();
    }

}
