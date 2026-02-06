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

    private Color _color;
    private Color _selectedColor = new Color(255 / 255f, 215 / 255f, 0 / 255f); // 金色表示选中
    private Color _hoverColor = new Color(207 / 255f, 207 / 255f, 207 / 255f); // 鼠标悬停颜色
    // 是否被选中
    private bool isSelected = false;
    // 是否鼠标悬停
    private bool isHovering = false;

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
        //按等级初始化默认颜色
        UpdateColorByGrade(weaponData.grade);

        _button.onClick.AddListener(() =>
        {
            ButtonClick(weaponData);
        }
        );
    }

    private void UpdateColorByGrade(int grade)
    {
        switch (grade)
        {
            case 1:
                _color = new Color(32 / 255f, 32 / 255f, 32 / 255f);//灰色
                break;
            case 2:
                _color = new Color(74 / 255f, 155 / 255f, 209 / 255f);//蓝色
                break;
            case 3:
                _color = new Color(173 / 255f, 90 / 255f, 255 / 255f);//紫色
                break;
            case 4:
                _color = new Color(255 / 255f, 59 / 255f, 59 / 255f);//红色
                break;
        }

        UpdateAppearance();
    }
    public void ButtonClick(WeaponData w)
    {
        // 通知PropsSelectPanel这个武器被选中了
        if (PropsSelectPanel.instance != null)
        {
            PropsSelectPanel.instance.SetSelectedWeapon(this.gameObject, _weaponData);
        }
    }
    // 设置选中状态
    public void SetSelected(bool selected)
    {
        isSelected = selected;
        UpdateAppearance();
    }
    // 更新外观
    private void UpdateAppearance()
    {
        if (isSelected)
        {
            // 设置为金色
            _backImage.color = _selectedColor;
        }
        else if (isHovering)
        {
            // 鼠标悬停颜色
            _backImage.color = _hoverColor;
        }
        else
        {
            // 恢复等级颜色
            _backImage.color = _color;
        }
    }
    //鼠标移入
    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
        if (!isSelected) // 如果不是选中状态，才更新颜色
        {
            UpdateAppearance();
        }
    }

    //鼠标移出
    public void OnPointerExit(PointerEventData eventData)
    {
        isHovering = false;
        if (!isSelected) // 如果不是选中状态，才更新颜色
        {
            UpdateAppearance();
        }
    }
}
