using DynamicBox.EventManagement;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
	#region Params
	[Header("Links")]

  [SerializeField] private TMP_Text dialogueText;

  [SerializeField] private TMP_Text mainCharacterText;

  [SerializeField] private TMP_Text npcText;

  [SerializeField] private Image mainCharacterImage;

  [SerializeField] private Image npcImage;

	[Header("Parameters")]

	[Range(0.0001f,0.9999f)]
	[SerializeField] private float dialogueWriteDelay;

	#endregion

	#region Unity Methods

	private void OnEnable()
	{
		EventManager.
			Instance.AddListener<OnDialogueEnterEvent>(OnDialogueEnterEventHandler);
	}

	private void OnDisable()
	{
		EventManager.
			Instance.RemoveListener<OnDialogueEnterEvent>(OnDialogueEnterEventHandler);
	}
	#endregion


	IEnumerator PlayDialogueCoroutine(DialogueDataSO dialogueData)
	{
		for(int i = 0; i < dialogueData.dialogueLines.Length; i++)
		{
			dialogueText.text = "";

			DialogueLine currrentLine = dialogueData.dialogueLines[i];

			if (currrentLine.isMainCharacter)
			{
				mainCharacterText.gameObject.SetActive(true);

				npcText.gameObject.SetActive(false);

				mainCharacterText.text = currrentLine.characterName;
			}
			else
			{
				mainCharacterText.gameObject.SetActive(false);

				npcText.gameObject.SetActive(true);

				npcText.text = currrentLine.characterName;
			}

			for(int j = 0; j < currrentLine.characterQuote.Length; j++)
			{
				dialogueText.text += currrentLine.characterQuote[j];

				yield return new WaitForSeconds(dialogueWriteDelay);
			}

			yield return new WaitForSeconds(currrentLine.dialogueLineDelay);
		}
	}



	#region Event Handlers

	private void OnDialogueEnterEventHandler(OnDialogueEnterEvent eventDetails)
	{
		StartCoroutine(PlayDialogueCoroutine(eventDetails.DialogueData));
	}

	#endregion

}
