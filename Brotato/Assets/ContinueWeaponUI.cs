using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ContinueWeaponUI : MonoBehaviour
{
    WeaponData weaponData;
    public Image _backImage;
    public Image _avatar;

    private Color _color;
    private void Awake()
    {
        _backImage = GetComponent<Image>();
        _avatar = transform.GetChild(0).GetComponent<Image>();
    }

    public void InitWeapon(WeaponData data)
    {
        weaponData = data;
        _avatar.sprite = Resources.Load<Sprite>(weaponData.avatar);
        //按等级初始化默认颜色
        UpdateColorByGrade(weaponData.grade);
        _backImage.color = _color;
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
    }
}
