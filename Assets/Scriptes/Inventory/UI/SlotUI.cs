using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SlotUI : MonoBehaviour,IPointerClickHandler,IPointerEnterHandler,IPointerExitHandler
{
    public Image ItemImage;
    private ItemDate currentItem;
    public ItemToolTip itemTool;
    private bool IsSelected;

    public void SetItem(ItemDate itemDateList)
    {
        currentItem= itemDateList;
        this.gameObject.SetActive(true);
        ItemImage.sprite = itemDateList.ItemSprite;
        ItemImage.SetNativeSize();
    }
    public void SetEmpty()
    {
        IsSelected= false;
        this.gameObject.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        IsSelected = !IsSelected;
        EventHanderler.CallItemSelectedEvent(currentItem,IsSelected);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(this.gameObject.activeInHierarchy)
        {
            itemTool.gameObject.SetActive(true);
            itemTool.UpdateItemName(currentItem.ItemName);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        itemTool.gameObject.SetActive(false);
    }
}
