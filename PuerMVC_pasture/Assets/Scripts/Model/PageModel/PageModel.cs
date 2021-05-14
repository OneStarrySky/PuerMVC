using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PageModel
{
    public int good;

    public PageModel() { }
    public PageModel(int good)
    {
        this.good = good;
    }
    public override string ToString()
    {
        return good.ToString();
    }
}
