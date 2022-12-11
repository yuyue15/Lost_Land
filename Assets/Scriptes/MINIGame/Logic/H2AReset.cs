using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H2AReset : InterActive
{
    private Transform gearSprite;

    void Awake()
    {
        gearSprite= GetComponent<Transform>();
    }
    public override void EmptyClicked()
    {
        //重置游戏
        GameController.Instance.ResetGame();
        gearSprite.DOPunchRotation(Vector3.forward * 180, 1, 1, 0);
    }
}
