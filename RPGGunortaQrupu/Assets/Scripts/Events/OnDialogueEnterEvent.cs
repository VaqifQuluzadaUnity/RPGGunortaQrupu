using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DynamicBox.EventManagement;

public class OnDialogueEnterEvent : GameEvent
{
  public DialogueDataSO DialogueData;

  public OnDialogueEnterEvent(DialogueDataSO _dialogueData)
	{
		DialogueData = _dialogueData;
	}
}
