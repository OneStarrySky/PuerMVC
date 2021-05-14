using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagProxy : Proxy
{
    public new static string NAME = "BagProxy";
    public IList<GoodsModel> goodsModels
    {
        get { return (IList<GoodsModel>)base.Data; }
    }
    public BagProxy() : base(NAME, new List<GoodsModel>())
    {

    }
    public void AddBag(GoodsModel goods)
    {
        goodsModels.Add(goods);
        SendNotification(OrderSystemEvent.AddBagChildM, goods);
    }
    public void RemoveBag(GoodsModel goods)
    {
        for(int i = 0; i < goodsModels.Count; i++)
        {
            if(goodsModels[i] == goods)
            {
                goodsModels.RemoveAt(i);
                return;
            }
        }
    }
}
