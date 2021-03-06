using UnityEngine;

namespace PureMVC.Core
{
    using PureMVC.Interfaces;
    using PureMVC.Patterns;
    using System;
    using System.Collections.Generic;

    public class View : IView
    {
        protected static volatile IView m_instance;
        protected IDictionary<string, IMediator> m_mediatorMap = new Dictionary<string, IMediator>();//中介库
        protected IDictionary<string, IList<IObserver>> m_observerMap = new Dictionary<string, IList<IObserver>>();//updatemenu handleNotification  menuMediator//OrderSystemEvent.STARTUP  new Observer("executeCommand", controller)
        protected static readonly object m_staticSyncRoot = new object();
        protected readonly object m_syncRoot = new object();

        protected View()
        {
            this.InitializeView();
        }

        public virtual bool HasMediator(string mediatorName)
        {
            lock (this.m_syncRoot)
            {
                return this.m_mediatorMap.ContainsKey(mediatorName);
            }
        }

        protected virtual void InitializeView()
        {
        }

        public virtual void NotifyObservers(INotification notification)//startup mainui //updatemenu
        {
            IList<IObserver> list = null;
            lock (this.m_syncRoot)
            {
                if (this.m_observerMap.ContainsKey(notification.Name))
                {
                    IList<IObserver> collection = this.m_observerMap[notification.Name];//new Observer("executeCommand", controller)//handleNotification  menuMediator
                    list = new List<IObserver>(collection);
                    //list = new List<IObserver>();
                    //for 遍历 list.add(collection);
                }
            }
            if (list != null)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    list[i].NotifyObserver(notification);//startup
                }
            }
        }

        public virtual void RegisterMediator(IMediator mediator)//menuMediator
        {
            lock (this.m_syncRoot)
            {
                if (this.m_mediatorMap.ContainsKey(mediator.MediatorName))
                {
                    return;
                }
                this.m_mediatorMap[mediator.MediatorName] = mediator;
                IList<string> list = mediator.ListNotificationInterests();// notifications.Add(OrderSystemEvent.UPMENU);
                                                                            //notifications.Add(OrderSystemEvent.CANCEL_ORDER);
                                                                            //notifications.Add(OrderSystemEvent.SUBMITMENU);
                if (list.Count > 0)
                {
                    IObserver observer = new Observer("handleNotification", mediator);//  handleNotification  menuMediator
                    for (int i = 0; i < list.Count; i++)
                    {
                        //将方法名注册给观察者
                        this.RegisterObserver(list[i].ToString(), observer);
                    }
                }
            }
            mediator.OnRegister();
        }

        public virtual void RegisterObserver(string notificationName, IObserver observer)//OrderSystemEvent.STARTUP  new Observer("executeCommand", this)
        {
            lock (this.m_syncRoot)
            {
                if (!this.m_observerMap.ContainsKey(notificationName))
                {
                    this.m_observerMap[notificationName] = new List<IObserver>();
                }
                this.m_observerMap[notificationName].Add(observer);
            }
        }

        public virtual IMediator RemoveMediator(string mediatorName)
        {
            IMediator notifyContext = null;
            lock (this.m_syncRoot)
            {
                if (!this.m_mediatorMap.ContainsKey(mediatorName))
                {
                    return null;
                }
                notifyContext = this.m_mediatorMap[mediatorName];
                IList<string> list = notifyContext.ListNotificationInterests();
                for (int i = 0; i < list.Count; i++)
                {
                    this.RemoveObserver(list[i], notifyContext);
                }
                this.m_mediatorMap.Remove(mediatorName);
            }
            if (notifyContext != null)
            {
                notifyContext.OnRemove();
            }
            return notifyContext;
        }

        public virtual void RemoveObserver(string notificationName, object notifyContext)
        {
            lock (this.m_syncRoot)
            {
                if (this.m_observerMap.ContainsKey(notificationName))
                {
                    IList<IObserver> list = this.m_observerMap[notificationName];
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].CompareNotifyContext(notifyContext))
                        {
                            list.RemoveAt(i);
                            break;
                        }
                    }
                    if (list.Count == 0)
                    {
                        this.m_observerMap.Remove(notificationName);
                    }
                }
            }
        }

        public virtual IMediator RetrieveMediator(string mediatorName)
        {
            lock (this.m_syncRoot)
            {
                if (!this.m_mediatorMap.ContainsKey(mediatorName))
                {
                    return null;
                }
                return this.m_mediatorMap[mediatorName];
            }
        }

        public static IView Instance
        {
            get
            {
                if (m_instance == null)
                {
                    lock (m_staticSyncRoot)
                    {
                        if (m_instance == null)
                        {
                            m_instance = new View();
                        }
                    }
                }
                return m_instance;
            }
        }
    }
}

