using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagMeditor : Mediator
{
    private BagProxy bagProxy = null;
    public new const string NAME = "BagMeditor";
    private BagView View
    {
        get { return (BagView)ViewComponent; }
    }
    public BagMeditor(BagView view) : base(NAME, view)
    {

    }
    public override void OnRegister()
    {
        base.OnRegister();
        bagProxy = Facade.RetrieveProxy(BagProxy.NAME) as BagProxy;
        if (null == bagProxy)
            throw new Exception("获取" + GoodsProxy.NAME + "代理失败");
        Action<object> actionList = (my) => {SendNotification(OrderSystemEvent.RemoveChild, my); };
        View.UpdateClient(bagProxy.goodsModels, actionList);
    }

    public override IList<string> ListNotificationInterests()
    {
        List<string> notifications = new List<string>();
        notifications.Add(OrderSystemEvent.OpenBag);
        notifications.Add(OrderSystemEvent.AddBagChildM);
        notifications.Add(OrderSystemEvent.RemoveChild);
        return notifications;
    }

    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case OrderSystemEvent.OpenBag:
                View.Show();
                break;
            case OrderSystemEvent.AddBagChildM:
                GoodsModel goods = notification.Body as GoodsModel;
                View.AddChild(goods);
                break;
            case OrderSystemEvent.RemoveChild:
                BagItemView bagItem = notification.Body as BagItemView;
                bagProxy.RemoveBag(bagItem.labekItem);
                View.RemoveChild(bagItem);
                break;
        }
    }
}
