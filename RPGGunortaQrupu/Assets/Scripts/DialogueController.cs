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

	[SerializeField] private GameObject choiceButtonPrefab;

	[SerializeField] private Transform choiceButtonParent;

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

	private void PlayDialogue(DialogueDataSO dialogueData)
	{
		StartCoroutine(WriteDialogueQuoteToText(dialogueData));

		for(int i = 0; i < choiceButtonParent.transform.childCount; i++)
		{
			Destroy(choiceButtonParent.transform.GetChild(i).gameObject);
		}
	}

	IEnumerator WriteDialogueQuoteToText(DialogueDataSO diaData)
	{
		dialogueText.text = "";

		string charQuoute = diaData.characterDialogueLine.characterQuote;

		for (int i = 0; i < charQuoute.Length; i++)
		{
			dialogueText.text += charQuoute[i];
			yield return new WaitForSeconds(dialogueWriteDelay);
		}

		SpawnDialogueChoiceButtons(diaData);
	}
	
	private void SpawnDialogueChoiceButtons(DialogueDataSO diaData)
	{
		for(int i = 0; i < diaData.dialogueChoiceStates.Length; i++)
		{
			GameObject choiceButtonInstance = Instantiate(choiceButtonPrefab,choiceButtonParent);

			choiceButtonInstance.GetComponentInChildren<Text>().text =
				diaData.dialogueChoiceStates[i].nextStatesChoiceTextString;

			int index = i;
			choiceButtonInstance.GetComponent<Button>()
				.onClick.AddListener(delegate { OnChoiceButtonPressed(index,diaData); });
		}
	}

	private void OnChoiceButtonPressed(int choiceIndex,DialogueDataSO dialogueData)
	{
		Debug.Log(choiceIndex);
		PlayDialogue(dialogueData.dialogueChoiceStates[choiceIndex].nextStatesData);
	}

	#region Event Handlers

	private void OnDialogueEnterEventHandler(OnDialogueEnterEvent eventDetails)
	{
		PlayDialogue(eventDetails.DialogueData);
	}

	#endregion

}
