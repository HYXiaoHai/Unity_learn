using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//
//鼠标回调函数的实现MonoBehaviour里的方法
//
public class No14_OnMouseEventFunction : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // 需要加碰撞体 //
    public GameObject gris;
    private void OnMouseDown()
    {
        Debug.Log("在Gri上按下鼠标");
    }
    private void OnMouseUp()
    {
        Debug.Log("在Gris身上按下的鼠标抬起时");//可以不在对象身上抬起。
    }
    private void OnMouseDrag()
    {
        Debug.Log("在Gris身上进行了拖拽操作");
    }
    private void OnMouseEnter()
    {
        Debug.Log("鼠标移入了gris");
    }
    private void OnMouseExit()
    {
        Debug.Log("鼠标移出了gris");
    }
    private void OnMouseOver()
    {
        Debug.Log("鼠标悬停在了gris上方");
    }
    private void OnMouseUpAsButton()
    {
        Debug.Log("鼠标在gris身上松开了");
    }

    //专门给ui的接口：
    //需要增加：IPointerEnterHandler, IPointerExitHandler
    public void OnPointerEnter(PointerEventData eventData)
    {
        // 鼠标悬停在UI按钮上
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // 鼠标离开UI按钮
    }
}
