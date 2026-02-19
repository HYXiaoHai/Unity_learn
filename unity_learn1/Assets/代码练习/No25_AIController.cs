using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class No25_AIController : MonoBehaviour
{
   public NavMeshAgent agent;
    void Start()
    {
        //获取代理组件
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //按下鼠标
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                Vector3 point = hit.point;
                //设置目标点
                agent.SetDestination(point);
            }
        }
    }
}
