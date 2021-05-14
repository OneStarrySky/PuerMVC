using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsMediator : Mediator
{
    private GoodsProxy goodsProxy = null;
    public new const string NAME = "GoodsMediator";
    private GoodsView View
    {
        get { return (GoodsView)ViewComponent; }
    }
    public GoodsMediator(GoodsView view) : base(NAME,view)
    {

    }
    public override void OnRegister()
    {
        base.OnRegister();
        goodsProxy = Facade.RetrieveProxy(GoodsProxy.NAME) as GoodsProxy;
        if (null == goodsProxy)
            throw new Exception("获取" + GoodsProxy.NAME + "代理失败");
        Action<object> actionList = (my) => { SendNotification(OrderSystemEvent.AddBag, my, "add"); };
        View.UpdateClient(goodsProxy.goodsModels, actionList);
    }

    public override IList<string> ListNotificationInterests()
    {
        List<string> notifications = new List<string>();
        notifications.Add(OrderSystemEvent.OpenGoods);
        return notifications;
    }

    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case OrderSystemEvent.OpenGoods:
                View.Show();
                break;
        }
    }
}
