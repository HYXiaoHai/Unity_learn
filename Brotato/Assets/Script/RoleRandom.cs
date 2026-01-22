using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RoleRandom : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Image _backImage;
    public Button _button;
    public List<RoleUI>unlockedRoles = new List<RoleUI>();
    private void Awake()
    {
        _backImage = GetComponent<Image>();
        _button = GetComponent<Button>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _backImage.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);
        RoleSelectPanel.Instance._contentCanvasGroup.alpha = 0;

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _backImage.color = new Color(32 / 255f, 32 / 255f, 32 / 255f);
    }
    void Start()
    {
        _button.onClick.AddListener(()=>
        {
            //遍历
            foreach (RoleUI role in RoleSelectPanel.Instance._roleList.GetComponentsInChildren<RoleUI>())
            {
                if(role._roleData.unlock == 1)
                {
                    unlockedRoles.Add(role);
                }
            }

            //随机获取
            RoleUI r = GameManage.Instance.RandomOne(unlockedRoles) as RoleUI ;

            //修改大屏
            if (RoleSelectPanel.Instance._contentCanvasGroup.alpha != 1)
                RoleSelectPanel.Instance._contentCanvasGroup.alpha = 1;
            r.RenewUI(r._roleData);
            r.ButtonClick(r._roleData);
        }
        );
    }

    void Update()
    {
        
    }
}
