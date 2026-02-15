using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class No24_Ray : MonoBehaviour
{
    private void Start()
    {
        Ray ray = new Ray(Vector3.zero,Vector3.up);//创建射线，从0，0向上发射

        ray = Camera.main.ScreenPointToRay(Input.mousePosition);//从屏幕上获取点
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            //声明碰撞信息类
            RaycastHit hit;
            bool res = Physics.Raycast(ray, out hit);
            if (res)
            {
                Debug.Log(hit.point);
                transform.position = hit.point;
            }

            //多检测（，距离范围，只检测第十个10层）
            RaycastHit[] hits = Physics.RaycastAll(ray,100,1<<10);
            
        }
    }
}
