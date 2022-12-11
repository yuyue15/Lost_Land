using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName ="ItemDateList",menuName ="面板/物品数据")]
public class ItemDateList : ScriptableObject
{
    public List<ItemDate> items;

    public ItemDate GetItemDate(ItemName itemName)
    {
        return items.Find(i=>i.ItemName== itemName);
    }
}
[Serializable]
public class ItemDate
{
    public ItemName ItemName;
    public Sprite ItemSprite;
}
