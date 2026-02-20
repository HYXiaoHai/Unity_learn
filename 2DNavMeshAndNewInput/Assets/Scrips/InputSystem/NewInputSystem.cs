using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class NewInputSystem : MonoBehaviour
{
    public Image image;
    public Vector2 mousePosition;
    public Vector2 mouseScroll;

    public float qKeyValue;
    private void Update()
    {
        //if(Input.GetKeyDown(KeyCode.Q))
        if (Keyboard.current != null &&Keyboard.current.qKey.wasPressedThisFrame)
        {
            Debug.Log("按下了Q键");
        }
        if (Keyboard.current != null &&Keyboard.current.qKey.wasReleasedThisFrame)
        {
            Debug.Log("松下了Q键");
        }

        //鼠标左键
        if(Mouse.current!=null&&Mouse.current.leftButton.wasPressedThisFrame)
        {
            Debug.Log("按下了左键");
        }
        if(Mouse.current!=null&&Mouse.current.rightButton.wasPressedThisFrame)
        {
            Debug.Log("按下了右键");
        }
        if(Mouse.current!=null&&Mouse.current.middleButton.wasPressedThisFrame)
        {
            Debug.Log("按下了右键");
        }

        qKeyValue = Keyboard.current.qKey.ReadValue();//0 没有按下 1按下了
        Debug.Log(qKeyValue);
        if(Keyboard.current.qKey.ReadValue()==1)
        {
            Debug.Log("q正在被按下");
        }

        //Input.mousePosition
        mousePosition = Mouse.current.position.ReadValue();
        mouseScroll = Mouse.current.scroll.ReadValue();//滚轮的值

        //
        Gamepad.current.bButton.ReadValue();
    }
   
}
