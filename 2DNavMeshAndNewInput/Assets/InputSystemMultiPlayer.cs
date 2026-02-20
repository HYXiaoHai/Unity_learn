using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemMultiPlayer : MonoBehaviour
{

    public int index = -1;
    public Vector2 movement;
    public InputSystemMultiData data = null;
    private Rigidbody2D rg;
    private void Awake()
    {
        rg = GetComponent<Rigidbody2D>();
        data = null;
    }
    private void Update()
    {
        if(data == null || data._id == 0)
        {
            data = InputSystemMultifer.instance.GetdataByIndex(index);
            if(data != null)
            {
                Debug.Log(index+"Jump");
                data.onJump += OnBallJump;
                

            }
            else
            {
                return;
            }
        }
        movement = data.onMove;
        transform.Translate(movement*5*Time.deltaTime);
    }

    private void OnBallJump()
    {
        rg.AddForce(Vector2.up * 5f, ForceMode2D.Impulse);
        //手柄震动
        StartCoroutine(StartShock());
    }
    private IEnumerator StartShock()
    {
        if(data._device is Gamepad==false)
        {
            yield break;
        }
        Gamepad gamepad = data._device as Gamepad;
        gamepad.SetMotorSpeeds(0.3f,0.6f);//范围
        yield return new WaitForSeconds(0.1f);//时间
        gamepad.SetMotorSpeeds(0f, 0f);
    }

}
