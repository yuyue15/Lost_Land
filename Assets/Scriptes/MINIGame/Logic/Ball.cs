using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private SpriteRenderer sprite;
    public BallDetail ballDetail;
    public bool IsMatch;
    private void Awake()
    {
        sprite= GetComponent<SpriteRenderer>();
    }
    public void SetBall(BallDetail ball)
    {
        ballDetail = ball;
        if(IsMatch)
        {
            SetRight();
        }
        else
        {
            SetWrong();
        }
    }
    public void SetRight()
    {
        sprite.sprite = ballDetail.rightSprite;

    }
    public void SetWrong()
    {
        sprite.sprite = ballDetail.wrongSprite; 
    }
}
