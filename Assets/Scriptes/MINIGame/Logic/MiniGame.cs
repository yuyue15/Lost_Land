using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MiniGame : MonoBehaviour
{
    public UnityEvent OnGameFinish;
    public string gameName;
    public bool IsPass;

    public void UpDateMiniGameState()
    {
        if(IsPass)
        {
            GetComponent<Collider2D>().enabled = false;
            GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0);
            OnGameFinish.Invoke();
        }
    }
}
