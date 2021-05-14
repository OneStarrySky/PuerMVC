using OrderSystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagView : MonoBehaviour
{
    private ObjectPool<BagItemView> objectPool = null;
    private List<BagItemView> goodsItems = new List<BagItemView>();
    private Transform parent = null;
    private void Awake()
    {
        parent = transform.Find("ScrollView/Viewport/Content").transform;
        GameObject go = Resources.Load<GameObject>("UI/item/itemBag");
        objectPool = new ObjectPool<BagItemView>(go, "BagPool");
        gameObject.SetActive(false);
    }
    private Action<object> childActuon = null;
    public void UpdateClient(IList<GoodsModel> labels, Action<object> objs)
    {
        for (int i = 0; i < this.goodsItems.Count; i++)
            objectPool.Push(this.goodsItems[i]);

        this.goodsItems.AddRange(objectPool.Pop(labels.Count));
        childActuon = objs;
        for (int i = 0; i < this.goodsItems.Count; i++)
        {
            var client = this.goodsItems[i];
            client.transform.SetParent(parent);
            client.InitClient(labels[i]);
            client.actionList = objs;
            client.Refresh();
        }
    }
    public void RemoveChild(BagItemView bagItem)
    {
        for (int i = 0; i < this.goodsItems.Count; i++)
        {
            if(this.goodsItems[i] = bagItem)
            {
                this.goodsItems.RemoveAt(i);
                bagItem.RemoveGo();
                Destroy(bagItem.gameObject);
                return;
            }
        }
    }
    public void AddChild(GoodsModel model)
    {
        BagItemView temp = objectPool.Pop();
        temp.transform.SetParent(parent);
        this.goodsItems.Add(temp);
        temp.actionList = childActuon;
        temp.labekItem = model;
        temp.Refresh();
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
