﻿
using UnityEngine;

namespace PureMVC.Patterns
{
    using PureMVC.Interfaces;
    using System;
    using System.Reflection;

    public class Observer : IObserver
    {
        private object m_notifyContext;
        private string m_notifyMethod;
        protected readonly object m_syncRoot = new object();

        public Observer(string notifyMethod, object notifyContext)// executeCommand     controller
        {
            this.m_notifyMethod = notifyMethod;//函数名字
            this.m_notifyContext = notifyContext;//类  controller
        }

        public bool CompareNotifyContext(object obj)
        {
            lock (this.m_syncRoot)
            {
                return this.NotifyContext.Equals(obj);
            }
        }

        public object NotifyContext
        {
            private get
            {
                return this.m_notifyContext;
            }
            set
            {
                this.m_notifyContext = value;
            }
        }

        public string NotifyMethod
        {
            private get
            {
                return this.m_notifyMethod;
            }
            set
            {
                this.m_notifyMethod = value;
            }
        }

        public void NotifyObserver(INotification notification)////startup  mainui//handleNotification  menuMediator
        {
            object notifyContext;
            lock (this.m_syncRoot)
            {
                notifyContext = this.NotifyContext;   //controller
                //string notifyMethod = this.NotifyMethod;
            }
            //利用反射获取方法然后执行
            Type type = notifyContext.GetType();
            //这里设置忽略字母的大小写
            BindingFlags bindingAttr = BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase;
            //根据设置的中介者的名字或者是命令的名字执行对应的方法，具体方法的执行在中介者和命令中已经重写对应的方法实现
            MethodInfo method = type.GetMethod(this.NotifyMethod, bindingAttr);   //executeCommand  //handleNotification
            method.Invoke(notifyContext , new object[] { notification });//controller   //startup  mainui
        }
    }
}