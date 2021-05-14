using PureMVC.Interfaces;
using PureMVC.Patterns;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageMediator : Mediator
{
    private PageProxy guestProxy = null;
    public new const string NAME = "PageMediator";
    private PageView _PageView
    {
        get { return (PageView)ViewComponent; }
    }
    public PageMediator(PageView view) : base(NAME, view)
    {
        _PageView.clickBag += () => { SendNotification(OrderSystemEvent.OpenBag); };
        _PageView.clickGoods += () => { SendNotification(OrderSystemEvent.OpenGoods); };
        _PageView.clickSweep += () => { SendNotification(OrderSystemEvent.OpenSweep); };
    }
    public override void OnRegister()
    {
        base.OnRegister();
        guestProxy = Facade.RetrieveProxy(PageProxy.NAME) as PageProxy;
        if (null == guestProxy)
            throw new Exception(PageProxy.NAME + "is null!");
        _PageView.RefreshShow(guestProxy.page);
    }
    public override IList<string> ListNotificationInterests()
    {
        IList<string> notifications = new List<string>();
        notifications.Add(OrderSystemEvent.RefreshPanle);
        return notifications;
    }
    public override void HandleNotification(INotification notification)
    {
        switch (notification.Name)
        {
            case OrderSystemEvent.RefreshPanle:
                PageModel order = notification.Body as PageModel;
                if (null == order)
                    throw new Exception("order is null ,plase check it!");
                _PageView.RefreshShow(order);
            break;
        }
    }
}
