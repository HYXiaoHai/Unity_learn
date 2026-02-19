using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAI : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform targetTrans;
    public bool isClickCtrl;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;//ËøÒ»ÏÂ
        agent.updateUpAxis = false;
    }
    // Update is called once per frame
    void Update()
    {
        if(isClickCtrl)
        {
            if(Input.GetMouseButtonDown(0))
            {
                Vector3 targetPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                targetPos.z = 0;
                agent.SetDestination(targetPos);
            }
        }
        else
        {
            SetDestination(targetTrans.position);
        }
    }
    private void SetDestination(Vector3 pos)
    {
        float agentOffset = 0.0001f;
        Vector3 agentPos = (Vector3)(agentOffset*Random.insideUnitCircle)+pos;//Ëæ»úÆ«ÒÆ
        agent.SetDestination(agentPos);
    }
}
