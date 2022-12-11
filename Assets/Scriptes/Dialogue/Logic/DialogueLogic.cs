using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLogic : MonoBehaviour
{
    public DialogueDate dialogueEmpty;
    public DialogueDate dialogueFinish;
    private Stack<string> dialogueEmptyStack;
    private Stack<string> dialogueFinishStack;
    private bool isTalking;

    private void Awake()
    {
        FillDialogueStack();
    }

    private void FillDialogueStack()
    {
        dialogueEmptyStack= new Stack<string>();
        dialogueFinishStack= new Stack<string>();
        for (int i = dialogueEmpty.DialogueDateList.Count - 1; i > -1; i--)
        {
            dialogueEmptyStack.Push(dialogueEmpty.DialogueDateList[i]);
        }
        for (int i = dialogueFinish.DialogueDateList.Count - 1; i > -1; i--)
        {
            dialogueFinishStack.Push(dialogueFinish.DialogueDateList[i]);
        }
    }
    public void ShowDialogueEmpty()
    {
        if(!isTalking)
        {
            StartCoroutine(DialogueRoutine(dialogueEmptyStack));
        }
    }
    public void ShowDialogueFinish()
    {
        if(!isTalking)
        {
            StartCoroutine(DialogueRoutine(dialogueFinishStack));
        }
    }
    private IEnumerator DialogueRoutine(Stack<string> data)
    {
        isTalking=true;
        if(data.TryPop(out string result))
        {
            EventHanderler.CallShowDialogueTextEvent(result);
            yield return null;
            isTalking=false;
            EventHanderler.CallGameStateChangeEvent(GameState.Pause);
        }
        else
        {
            EventHanderler.CallShowDialogueTextEvent(string.Empty);
            FillDialogueStack();
            isTalking=false ;
            EventHanderler.CallGameStateChangeEvent(GameState.GamePlayer);
        }
    }
}
