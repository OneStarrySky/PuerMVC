using PureMVC.Interfaces;
using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddBagcommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        string _type = notification.Type;
        if(_type == "add")
        {
            GoodsModel goods = notification.Body as GoodsModel;
            BagProxy bagProxy = Facade.RetrieveProxy(BagProxy.NAME) as BagProxy;
            bagProxy.AddBag(goods);
            return;
        }
        if (_type == "remove")
        {
            GoodsModel goods = notification.Body as GoodsModel;
            BagProxy bagProxy = Facade.RetrieveProxy(BagProxy.NAME) as BagProxy;
            bagProxy.RemoveBag(goods);
            return;
        }
    }
}
