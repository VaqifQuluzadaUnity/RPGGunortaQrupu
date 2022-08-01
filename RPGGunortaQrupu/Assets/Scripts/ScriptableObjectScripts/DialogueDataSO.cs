using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewDialogueData",menuName ="ScriptableObjects/DialogueData")]
public class DialogueDataSO : ScriptableObject
{
	public DialogueLine characterDialogueLine;

	public DialogueChoiceStateClass[] dialogueChoiceStates;

}


[System.Serializable]
public class DialogueLine
{
	public string characterName;

	public Sprite characterIcon;

	public AudioClip dialogueSFX;

	public bool isMainCharacter;

	[TextArea(5,10)]
	public string characterQuote;

	public int dialogueLineDelay;

}

[System.Serializable]
public class DialogueChoiceStateClass
{
	public string nextStatesChoiceTextString;

	public DialogueDataSO nextStatesData;
}