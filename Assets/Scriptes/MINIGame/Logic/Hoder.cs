using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hoder : InterActive
{
    public BallName matchBall;
    private Ball currentBall;
    public HashSet<Hoder> LinkHolders = new HashSet<Hoder>();

    public bool IsEmpty;

    public void CheackBall(Ball ball)
    {
        currentBall= ball;
        if(ball.ballDetail.ballName==matchBall)
        {
            currentBall.IsMatch = true;
            currentBall.SetRight();
        }
        else
        {
            currentBall.IsMatch = false;
            currentBall.SetWrong();
        }
    }
    public override void EmptyClicked()
    {
        foreach(var holder in LinkHolders) 
        {
            if(holder.IsEmpty)
            {
                //移动球
                currentBall.transform.position=holder.transform.position;
                currentBall.transform.SetParent(holder.transform);
                //交换球
                holder.CheackBall(currentBall);
                this.currentBall = null;

                //改变状态
                this.IsEmpty = true;
                holder.IsEmpty= false;

                EventHanderler.CallCheckGameStateEvent();
            }
        }

    }
}
