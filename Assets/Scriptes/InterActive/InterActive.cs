using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterActive : MonoBehaviour
{
    public ItemName RequireItem;
    public bool IsDone;
    public void CheckITem(ItemName itemName)
    {
        if (itemName == RequireItem && !IsDone)
        {
            IsDone= true;
            //使用并移除物品
            OnClickedAction();
            EventHanderler.CallItemNameEvent(itemName);
        }
    }
    /// <summary>
    /// 默认是正确物品的情况下
    /// </summary>
    protected virtual void OnClickedAction()
    {

    }
    /// <summary>
    /// 空物品情况下
    /// </summary>
    public virtual void EmptyClicked()
    {
        Debug.Log("空点");
    }
}
