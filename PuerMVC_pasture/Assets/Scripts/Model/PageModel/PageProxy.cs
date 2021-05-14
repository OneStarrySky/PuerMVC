using PureMVC.Patterns;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageProxy : Proxy
{
    public new const string NAME = "GuestProxy";
    public PageModel page
    {
        get { return (PageModel)base.Data; }
    }
    public PageProxy() : base(NAME, new PageModel())
    {
        Init();
    }
    private void Init()
    {
        page.good = 100;
    }
}
