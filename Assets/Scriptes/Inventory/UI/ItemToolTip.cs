using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemToolTip : MonoBehaviour
{
    public Text ItemNameText;
    public void UpdateItemName(ItemName itemName)
    {
        ItemNameText.text = itemName switch
        {
            ItemName.Key => "钥匙",
            ItemName.Ticket => "一张船票",
            _=> ""
        };
    }
}
