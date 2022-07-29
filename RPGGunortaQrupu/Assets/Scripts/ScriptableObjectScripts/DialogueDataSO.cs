using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewDialogueData",menuName ="ScriptableObjects/DialogueData")]
public class DialogueDataSO : ScriptableObject
{
	public DialogueLine[] dialogueLines;

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