using OrderSystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUI : MonoBehaviour
{
    public PageView pageView = null;
    public GoodsView goodsView = null;
    public BagView bagView = null;
    private void Start()
    {
        ApplicationFacade facade = new ApplicationFacade();
        facade.StartUp(this);
    }
}
