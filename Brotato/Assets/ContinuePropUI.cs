using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

public class ContinuePropUI : MonoBehaviour
{
    public PropData propData;
    public Image _avatar;
    private void Awake()
    {
        _avatar = GetComponentInChildren<Image>();
    }

    public void InitProp(PropData data)
    {
        propData = data;
        _avatar.sprite = Resources.Load<SpriteAtlas>("Image/ÆäËû/Props").GetSprite(propData.name);
    }
}
