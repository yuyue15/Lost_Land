using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public string ScenceFrom;
    public string ScenceToGo;
    public void TeleportToScene()
    {
        TransitionManager.Instance.Transition(ScenceFrom,ScenceToGo);
    }
}
