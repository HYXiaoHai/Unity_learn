using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class RoleUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public RoleData _roleData;

    public Image _backImage;//����
    public Image _avatar;//ͷ��
    public Button _button;//��ť
    private AudioSource audioSource;
    private AudioClip audioClip;

    private void Awake()
    {
        _backImage = GetComponent<Image>();
        _avatar = transform.GetChild(0).GetComponent<Image>();
        _button = GetComponent<Button>();

        audioSource = GetComponent<AudioSource>();
        audioClip = Resources.Load<AudioClip>("Music/菜单音效");
    }
    public void SetData(RoleData roleData)
    {
        
        _roleData = roleData;
        if (_roleData.unlock == 0)
        {
            _avatar.sprite = Resources.Load<Sprite>("Image/UI/锁");
        }
        else
        {
            _avatar.sprite = Resources.Load<Sprite>(roleData.avatar);
        }
        _button.onClick.AddListener(() =>
        {
            ButtonClick(roleData);
        }
        );
    }

    public void ButtonClick(RoleData r)
    {
        GameManage.Instance.currentRole = r;

        //�رս�ɫѡ�����
        RoleSelectPanel.Instance._canvasGroup.alpha = 0;
        RoleSelectPanel.Instance._canvasGroup.interactable = false;
        RoleSelectPanel.Instance._canvasGroup.blocksRaycasts = false;

        //��¡��ɫui
        GameObject go = Instantiate(RoleSelectPanel.Instance._roleDetails, WeaponSelectPanel.Instance._weaponContent);
        go.transform.SetSiblingIndex(0);//����λ�ã������������ŵ�һλ��

        //������ѡ��ģ��
        WeaponSelectPanel.Instance._canvasGroup.alpha = 1;
        WeaponSelectPanel.Instance._canvasGroup.interactable = true;
        WeaponSelectPanel.Instance._canvasGroup.blocksRaycasts = true;
        WeaponSelectPanel.Instance._canvasGroup.blocksRaycasts = true;
    }

    //�������
    public void OnPointerEnter(PointerEventData eventData)
    {
        _backImage.color = new Color(207 / 255f, 207 / 255f, 207 / 255f);
        audioSource.PlayOneShot(audioClip);
        if (RoleSelectPanel.Instance._contentCanvasGroup.alpha != 1)
        RoleSelectPanel.Instance._contentCanvasGroup.alpha = 1;//��ʾ����

        RenewUI(_roleData);
    }
    public void RenewUI(RoleData r)
    {
        //未解锁
        if (r.unlock == 0)
        {
            RoleSelectPanel.Instance._roleName.text = "???";
            RoleSelectPanel.Instance._avatar.sprite = Resources.Load<Sprite>("Image/UI/锁");
            RoleSelectPanel.Instance._roleDescribe.text = r.unlockConditions;
            RoleSelectPanel.Instance._text3.text = "尚无记录";
        }
        else//�ѽ���
        {
            RoleSelectPanel.Instance._roleName.text = r.name;
            RoleSelectPanel.Instance._avatar.sprite = Resources.Load<Sprite>(r.avatar);
            RoleSelectPanel.Instance._roleDescribe.text = r.describe;
            RoleSelectPanel.Instance._text3.text = GetRecord(r.record);
        }
    }
    //��ȡͨ�ؼ�¼
    string GetRecord(int record)
    {
        string result = "";
        switch (record)
        {
            case -1:
                result = "尚无记录"; break;
            case 0:
                result = "通关危险0"; break;
            case 1:
                result = "通关危险1"; break;
            case 2:
                result = "通关危险2"; break;
            case 3:
                result = "通关危险3"; break;
            case 4:
                result = "通关危险4"; break;
            case 5:
                result = "通关危险5"; break;
        }

        return result;
    }
    //����Ƴ�
    public void OnPointerExit(PointerEventData eventData)
    {
        _backImage.color = new Color(32 / 255f, 32 / 255f, 32 / 255f);
    }
}
