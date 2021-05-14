using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartUpCommand : SimpleCommand
{
    public override void Execute(INotification notification)
    {
        PageProxy pageProxy = new PageProxy();
        Facade.RegisterProxy(pageProxy);

        GoodsProxy goodsProxy = new GoodsProxy();
        Facade.RegisterProxy(goodsProxy);

        BagProxy bagProxy = new BagProxy();
        Facade.RegisterProxy(bagProxy);

        MainUI mainUI = notification.Body as MainUI;
        if (null == mainUI)
            throw new Exception("程序启动失败..");

        Facade.RegisterMediator(new PageMediator(mainUI.pageView));
        Facade.RegisterMediator(new GoodsMediator(mainUI.goodsView));
        Facade.RegisterMediator(new BagMeditor(mainUI.bagView));

        Facade.RegisterCommand(OrderSystemEvent.AddBag, typeof(AddBagcommand));

    }
}
