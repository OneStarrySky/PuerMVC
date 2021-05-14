using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsModel
{
    public Sprite sprite = null;
    public string name = "";
    public GoodsModel(string name, string icon)
    {
        this.sprite = Resources.Load<Sprite>("icon/"+icon);
        this.name = name;
    }
}
