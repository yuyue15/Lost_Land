using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class CursorManager : Singleton<CursorManager>
{
    public RectTransform Hand;

    /// <summary>
    /// 屏幕坐标转换为世界坐标
    /// </summary>
    private Vector3 MouseWorldPos => Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
    /// <summary>
    /// 设置能否点击条件
    /// </summary>
    private bool CanClick;
    private bool HoldItem;
    private ItemName CurrentItemName;
    
    void OnEnable()
    {
        EventHanderler.ItemSelectedEvent += OnItemSelectedEvent;
        EventHanderler.ItemNameUseEvent += OnItemNameUseEvent;
    }
    void OnDisable()
    {
        EventHanderler.ItemSelectedEvent -= OnItemSelectedEvent;
        EventHanderler.ItemNameUseEvent -= OnItemNameUseEvent;
    }

    private void OnItemNameUseEvent(ItemName ItemName)
    {
        CurrentItemName = ItemName.None;
        HoldItem = false;
        Hand.gameObject.SetActive(false);
    }

    private void OnItemSelectedEvent(ItemDate itemDate, bool isSelected)
    {
        HoldItem= isSelected;
        if(isSelected)
        {
            CurrentItemName = itemDate.ItemName;

        }
        Hand.gameObject.SetActive(HoldItem);
    }

    private void Update()
    {
        CanClick = ObjectAtMousePosition();
        if(Hand.gameObject.activeInHierarchy) 
        {
            Hand.position=Input.mousePosition;
        }
        if (InteractWithUI()) return;
        if (CanClick && Input.GetMouseButtonDown(0))
        {
            //检测鼠标互动情况
            ClickAction(ObjectAtMousePosition().gameObject);

        }
    }
    /// <summary>
    /// 检测是否是物体
    /// </summary>
    /// <param name="clickObject"></param>
    private void ClickAction(GameObject clickObject)
    {
        switch (clickObject.tag)
        {
            case "TelePort":
                var teleport = clickObject.GetComponent<Teleport>();
                teleport?.TeleportToScene();
                break;
            case "Item":
                var item = clickObject.GetComponent<Item>();
                item?.ItemClicked();
                break;
            case "InterActive":
                var InterActive = clickObject.GetComponent<InterActive>();
                if(HoldItem)
                {
                    InterActive?.CheckITem(CurrentItemName);
                }
                else
                {
                    InterActive?.EmptyClicked();
                }
                break;
        }
    }
    /// <summary>
    /// 检测鼠标点击的范围的碰撞体
    /// </summary>
    /// <returns></returns>
    private Collider2D ObjectAtMousePosition()
    {

        return Physics2D.OverlapPoint(MouseWorldPos);
        
    }
    /// <summary>
    /// 画出检测图形
    /// </summary>
    //private void OnDrawGizmos()
    //{
    //    Gizmos.DrawCube(mouseWorldPos, new Vector3(1, 1, 0));
    //}
    private bool InteractWithUI()
    {
        if(EventSystem.current!=null&&EventSystem.current.IsPointerOverGameObject())
            return true;
        return false;
    }
}
