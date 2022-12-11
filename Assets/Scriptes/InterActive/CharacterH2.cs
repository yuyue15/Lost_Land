using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueLogic))]

public class CharacterH2 : InterActive
{
    private DialogueLogic dialogueLogic;
    private void Awake()
    {
        dialogueLogic=GetComponent<DialogueLogic>();
    }
    public override void EmptyClicked()
    {
        //对话内容Empty
        if(IsDone) 
            dialogueLogic.ShowDialogueFinish();
        else 
            dialogueLogic.ShowDialogueEmpty();
    }
    protected override void OnClickedAction()
    {
        //对话内容finish
        dialogueLogic.ShowDialogueFinish();
    }
}
