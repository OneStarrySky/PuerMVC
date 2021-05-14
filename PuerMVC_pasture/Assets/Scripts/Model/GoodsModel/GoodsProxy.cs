using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsProxy : Proxy
{
    public new static string NAME = "GoodsProxy";
    public IList<GoodsModel> goodsModels
    {
        get { return (IList<GoodsModel>)base.Data; }
    }
    public GoodsProxy() : base(NAME, new List<GoodsModel>())
    {
        goodsModels.Add(new GoodsModel("mv1", "1"));
        goodsModels.Add(new GoodsModel("mv2", "2"));
        goodsModels.Add(new GoodsModel("mv3", "3"));
        goodsModels.Add(new GoodsModel("mv4", "4"));
        goodsModels.Add(new GoodsModel("mv5", "5"));
        goodsModels.Add(new GoodsModel("mv6", "6"));
        goodsModels.Add(new GoodsModel("mv7", "7"));
        goodsModels.Add(new GoodsModel("mv8", "8"));
        goodsModels.Add(new GoodsModel("mv9", "9"));
        goodsModels.Add(new GoodsModel("mv10", "10"));
        goodsModels.Add(new GoodsModel("mv11", "11"));
        goodsModels.Add(new GoodsModel("mv12", "12"));
        goodsModels.Add(new GoodsModel("mv13", "13"));
        goodsModels.Add(new GoodsModel("mv14", "14"));
        goodsModels.Add(new GoodsModel("mv15", "15"));
        goodsModels.Add(new GoodsModel("mv16", "16"));
        goodsModels.Add(new GoodsModel("mv17", "17"));

    }
}
