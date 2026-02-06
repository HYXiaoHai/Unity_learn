using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ShopPropUI : MonoBehaviour
{
    public PropData _propData;

    public Image _backImage;//背景
    public Image _avatar;//头像
    public Button _button;//按钮

    private void Awake()
    {
        _backImage = GetComponent<Image>();
        _avatar = transform.GetChild(0).GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    public void SetData(PropData propData)
    {
        _propData = propData;
        _avatar.sprite = Resources.Load<SpriteAtlas>("Image/其他/Props").GetSprite(propData.name);
        _button.onClick.AddListener(() =>
        {
            ButtonClick(propData);
        }
        );
    }
    public void ButtonClick(PropData w)
    {
        //
    }

    //鼠标移入
    public void OnPointerEnter(PointerEventData eventData)
    {
        _backImage.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);
    }

    //鼠标移出
    public void OnPointerExit(PointerEventData eventData)
    {
        _backImage.color = new Color(32 / 255f, 32 / 255f, 32 / 255f);
    }
}
