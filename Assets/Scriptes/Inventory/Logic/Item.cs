using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    /// <summary>
    /// 物品名称
    /// </summary>
    public ItemName ItemName;

    public void ItemClicked()
    {
        //添加到背包后隐藏物体
        InventoryManager.Instance.AddItem(ItemName);
        foreach(var item in FindObjectsOfType<Item>())
        this.gameObject.SetActive(false);
        
    }
}
