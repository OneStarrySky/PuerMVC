using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BagItemView : MonoBehaviour
{
    private Button but_click = null;
    private Text text_name = null;
    private Image img_icon = null;
    public GoodsModel labekItem = null;
    public Action<object> actionList = null;
    private void Awake()
    {
        but_click = transform.GetComponent<Button>();
        img_icon = transform.GetComponent<Image>();
        text_name = transform.Find("Text").GetComponent<Text>();
        but_click.onClick.AddListener(() => { actionList(this); });
    }
    public void InitClient(GoodsModel labekItem)
    {
        this.labekItem = labekItem;
    }
    public void Refresh()
    {
        text_name.text = labekItem.name;
        img_icon.sprite = labekItem.sprite;
    }
    public void RemoveGo()
    {
        labekItem = null;
        actionList = null;
        but_click = null;
        text_name = null;
        img_icon = null;
    }
}
