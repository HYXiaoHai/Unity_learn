using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelScrollRect : MonoBehaviour,IBeginDragHandler, IEndDragHandler
{
    private ScrollRect scroll;

    private float[] pagePosition = new float[4] {0,0.3333f,0.66666f,1 };
    // Start is called before the first frame update
    void Start()
    {
        scroll = GetComponent<ScrollRect>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnValueChanged(Vector2 v)
    {
        Debug.Log("OnValueChanged："+v);

    }

    public void OnBeginDrag(PointerEventData eventData)//开始拖拽的事件
    {
    }

    public void OnEndDrag(PointerEventData eventData)//结束拖拽
    {
        float currentPosition = scroll.verticalNormalizedPosition;
        int index = 3;
        float offest = currentPosition - pagePosition[3];
        for (int i = 2; i >= 0; i--)
        {
            if (Mathf.Abs(currentPosition - pagePosition[i]) < offest)
            {
                index = i;
                offest = Mathf.Abs(currentPosition - pagePosition[i]);
            }
        }
        scroll.verticalNormalizedPosition = pagePosition[index];
    }
}
