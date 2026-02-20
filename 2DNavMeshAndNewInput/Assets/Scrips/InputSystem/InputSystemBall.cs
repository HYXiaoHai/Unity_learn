using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemBall : MonoBehaviour
{
    public Vector2 moveMent;

    float horizontal;
    public Transform ballTRansform;
    private MyInputAction action;
    private Rigidbody2D rb;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        action = new MyInputAction();
        //action.GamePlay.Jump.performed += OnJump; 外部事件调用
        action.Enable();
    }
    public void OnJump(InputAction.CallbackContext context)
    {
        //start perform cncel
        if(context.phase == InputActionPhase.Performed)//阶段判断
        rb.AddForce(Vector2.up*5f,ForceMode2D.Impulse);
    }

    void Start()
    {

    }
    private void OnDestroy()
    {
        action.Disable();
    }
    void Update()
    {
        moveMent = action.GamePlay.MoveMent.ReadValue<Vector2>();
        horizontal = action.GamePlay.Horizontal.ReadValue<float>();
        MoveBall();
    }

   public void MoveBall()
    {
        ballTRansform.transform.Translate(moveMent * 5 * Time.deltaTime);
    }
}
