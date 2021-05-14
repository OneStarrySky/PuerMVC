using OrderSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodsView : MonoBehaviour
{
    private ObjectPool<GoodsItemView> objectPool = null;
    private List<GoodsItemView> goodsItems = new List<GoodsItemView>();
    private Transform parent = null;
    private void Awake()
    {
        parent = transform.Find("ScrollView/Viewport/Content").transform;
        GameObject go = Resources.Load<GameObject>("UI/item/itemGoods");
        objectPool = new ObjectPool<GoodsItemView>(go, "GoodsPool");
        gameObject.SetActive(false);
    }

    public void UpdateClient(IList<GoodsModel> labels, Action<object> objs)
    {
        for (int i = 0; i < this.goodsItems.Count; i++)
            objectPool.Push(this.goodsItems[i]);

        this.goodsItems.AddRange(objectPool.Pop(labels.Count));

        for (int i = 0; i < this.goodsItems.Count; i++)
        {
            var client = this.goodsItems[i];
            client.transform.SetParent(parent);
            client.InitClient(labels[i]);
            client.actionList = objs;
            client.Refresh();
        }
    }
    public void Show()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }

    }
}
