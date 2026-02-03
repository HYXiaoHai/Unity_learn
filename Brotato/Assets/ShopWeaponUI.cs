using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopWeaponUI : MonoBehaviour
{
    public WeaponData _weaponData;

    public Image _backImage;//背景
    public Image _avatar;//头像
    public Button _button;//按钮

    private void Awake()
    {
        _backImage = GetComponent<Image>();
        _avatar = transform.GetChild(0).GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    public void SetData(WeaponData weaponData)
    {
        _weaponData = weaponData;
        _avatar.sprite = Resources.Load<Sprite>(_weaponData.avatar);
        _button.onClick.AddListener(() =>
        {
            ButtonClick(weaponData);
        }
        );
    }
    public void ButtonClick(WeaponData w)
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
