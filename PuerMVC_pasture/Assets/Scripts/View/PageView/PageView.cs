using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PageView : MonoBehaviour
{
    public UnityAction clickBag = null;
    public UnityAction clickGoods = null;
    public UnityAction clickSweep = null;

    private Text good;
    private void Awake()
    {
        good = transform.Find("panel/good/text_good").GetComponent<Text>();
        transform.Find("panel/tools/but_bag").GetComponent<Button>().onClick.AddListener(() => { clickBag(); }) ;
        transform.Find("panel/but_goods").GetComponent<Button>().onClick.AddListener(() => { clickGoods(); });
        transform.Find("panel/tools/but_sweep").GetComponent<Button>().onClick.AddListener(() => { clickSweep(); });
    }
    public void RefreshShow(PageModel pageModel)
    {
        if (good != null)
        {
            good.text = pageModel.good.ToString();
        }
    }
}
