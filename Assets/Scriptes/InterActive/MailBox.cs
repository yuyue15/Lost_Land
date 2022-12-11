using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MailBox : InterActive
{
    private SpriteRenderer SpriteRenderer;
    private BoxCollider2D boxCollider;
    public Sprite OpenSprite;

    private void Awake()
    {
        SpriteRenderer= GetComponent<SpriteRenderer>();
        boxCollider= GetComponent<BoxCollider2D>();
    }
    void OnEnable()
    {
        EventHanderler.AfterSceneUnLoadEvent += OnAfterSceneUnLoadEvent;
        //EventHanderler.BeforeSceneUnLoadEvent += OnBeforeSceneUnLoadEvent;
    }



    void OnDisable()
    {
        EventHanderler.AfterSceneUnLoadEvent -= OnAfterSceneUnLoadEvent;
        //EventHanderler.BeforeSceneUnLoadEvent -= OnBeforeSceneUnLoadEvent;
    }

    //private void OnBeforeSceneUnLoadEvent()
    //{
    //    if(!IsDone) 
    //    {
    //        SpriteRenderer.sprite = OpenSprite;
    //        boxCollider.enabled = false;
    //    }
    //    else
    //    {
    //        transform.GetChild(0).gameObject.SetActive(false);
    //    }
    //}

    private void OnAfterSceneUnLoadEvent()
    {
        if(!IsDone)
        {
            transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            SpriteRenderer.sprite = OpenSprite;
            boxCollider.enabled= false;
        }
    }
    protected override void OnClickedAction()
    {
        SpriteRenderer.sprite = OpenSprite;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
