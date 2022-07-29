using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDialogueHolder : MonoBehaviour
{
  [SerializeField] private DialogueDataSO dialogueData;

  public DialogueDataSO ReturnDialogueData()
	{
		return dialogueData;
	}
}
