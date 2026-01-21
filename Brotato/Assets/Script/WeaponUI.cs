using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class WeaponUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public WeaponData _weaponData;

    public Image _backImage;//背景
    public Image _avatar;//头像
    public Button _button;//按钮

    private void Awake()
    {
        _backImage = GetComponent<Image>();
        //_avatar = GetComponentInChildren<Image>();
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
    void ButtonClick(WeaponData w)
    {
        //保存武器信息
        GameManage.Instance.currentWeapon.Add(w);

        //关闭武器选择面板
        WeaponSelectPanel.Instance._canvasGroup.alpha = 0;
        WeaponSelectPanel.Instance._canvasGroup.interactable = false;
        WeaponSelectPanel.Instance._canvasGroup.blocksRaycasts = false;

        //celon武器面板
        GameObject go1 = Instantiate(WeaponSelectPanel.Instance._weaponDetails, DifficultySelectPanel.Instance._difficultyContent);
        go1.transform.SetSiblingIndex(0);
        GameObject go2 = Instantiate(RoleSelectPanel.Instance._roleDetails, DifficultySelectPanel.Instance._difficultyContent);
        go2.transform.SetSiblingIndex(0);

        //打开难度选择
        DifficultySelectPanel.Instance._canvasGroup.alpha = 1;
        DifficultySelectPanel.Instance._canvasGroup.interactable = true;
        DifficultySelectPanel.Instance._canvasGroup.blocksRaycasts = true;
    }

    //鼠标移入
    public void OnPointerEnter(PointerEventData eventData)
    {
        _backImage.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);

        if (WeaponSelectPanel.Instance._WeaponDetallscanvasGroup.alpha != 1)
            WeaponSelectPanel.Instance._WeaponDetallscanvasGroup.alpha = 1;//显示大屏

        //设置上方面板
        ShowUI(_weaponData);
    }

    void ShowUI(WeaponData w)
    {
        //修改武器名称
        WeaponSelectPanel.Instance._weaponName.text = w.name;
        WeaponSelectPanel.Instance._weaponavatar.sprite = Resources.Load<Sprite>(w.avatar);
        //修改武器类型
        WeaponSelectPanel.Instance._weaponType.text = w.isLong == 1 ? "远程" : "近战";
        //修改武器描述
        WeaponSelectPanel.Instance._weaponDescribe.text = w.describe;
    }

    //鼠标移出
    public void OnPointerExit(PointerEventData eventData)
    {
        _backImage.color = new Color(32 / 255f, 32 / 255f, 32 / 255f);
    }
}
