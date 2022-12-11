using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public GameObject Panel;
    public Text DialogueText;

    void OnEnable()
    {
        EventHanderler.ShowDialogueTextEvent += ShowDialogue;
    }
    void OnDisable()
    {
        EventHanderler.ShowDialogueTextEvent -= ShowDialogue;
    }

    private void ShowDialogue(string dialogue)
    {
        if(dialogue!=string.Empty)
        {
            Panel.SetActive(true);
        }
        else
        {
            Panel.SetActive(false);
        }
        DialogueText.text= dialogue;
    }
}
