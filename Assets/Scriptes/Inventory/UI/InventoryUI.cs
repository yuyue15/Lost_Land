using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Button LeftButton, RightButton;
    public int CurrentIndex;//显示物品的序列号
    public SlotUI slotUI;
    

    private void OnEnable()
    {
        EventHanderler.UpdateUIEvent+= OnUpdateUIEvent;
    }
    private void OnDisable()
    {
        EventHanderler.UpdateUIEvent -= OnUpdateUIEvent;
    }
    private void OnUpdateUIEvent(ItemDate itemDate, int Index)
    {
        if (itemDate == null)
        {
            slotUI.SetEmpty();
            CurrentIndex = -1;
            LeftButton.interactable = false;
            RightButton.interactable = false;
        }
        else
        {
            CurrentIndex = Index;
            slotUI.SetItem(itemDate);
            if(Index>0)
            {
                LeftButton.interactable = true;
            }
            if(Index==-1)
            {
                RightButton.interactable = false;
                LeftButton.interactable = false;
            }
        }
    }
    /// <summary>
    /// 左右按钮Event事件
    /// </summary>
    /// <param name="amount"></param>
    public void SwitchItem(int amount)
    {
        var index = CurrentIndex + amount;
        if(index<CurrentIndex)
        {
            LeftButton.interactable = false;
            RightButton.interactable = true;
        }
        else if(index>CurrentIndex) 
        {
            LeftButton.interactable = true;
            RightButton.interactable = false;
        }
        else
        {
            LeftButton.interactable = true;
            RightButton.interactable = true;
        }
        EventHanderler.CallChangeItemEvent(index);
    }
}
