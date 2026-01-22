using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class WeaponRandom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image _backImage;
    public Button _button;
    public List<WeaponUI> weaponUIs = new List<WeaponUI>();

    private void Awake()
    {
        _backImage = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _backImage.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);
        WeaponSelectPanel.Instance._WeaponDetallscanvasGroup.alpha = 0;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _backImage.color = new Color(32 / 255f, 32 / 255f, 32 / 255f);
    }
    void Start()
    {
        _button.onClick.AddListener(() =>
        {
            //遍历
            foreach (WeaponUI weapon in WeaponSelectPanel.Instance._weaponList.GetComponentsInChildren<WeaponUI>())
            {
                weaponUIs.Add(weapon);
            }

            //随机获取
            WeaponUI w = GameManage.Instance.RandomOne(weaponUIs) as WeaponUI;

            //修改大屏
            if (WeaponSelectPanel.Instance._WeaponDetallscanvasGroup.alpha != 1)
                WeaponSelectPanel.Instance._WeaponDetallscanvasGroup.alpha = 1;
            w.ShowUI(w._weaponData);
            w.ButtonClick(w._weaponData);
        }
        );
    }
}
