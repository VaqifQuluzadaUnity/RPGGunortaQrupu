using DynamicBox.EventManagement;
using System;
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

	[SerializeField] private Transform choiceButtonParent;

	[SerializeField] private GameObject choiceButtonPrefab;

	[Header("Parameters")]

	[Range(0.0001f,0.9999f)]
	[SerializeField] private float dialogueWriteDelay;

	private IEnumerator dialoguePlayCoroutine;

	private DialogueDataSO currentDialogueData;

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


	



	#region Event Handlers

	private void OnDialogueEnterEventHandler(OnDialogueEnterEvent eventDetails)
	{

		dialoguePlayCoroutine = PlayDialogueState(eventDetails.DialogueData);

		StartCoroutine(dialoguePlayCoroutine);
	}

	#endregion

	IEnumerator PlayDialogueState(DialogueDataSO dialogueData)
	{
		currentDialogueData = dialogueData;

		InvokeRepeating("CheckUserInput", 0, Time.deltaTime);

		for(int i = 0; i < choiceButtonParent.childCount; i++)
		{
			Destroy(choiceButtonParent.GetChild(i).gameObject);
		}

		dialogueText.text = "";

		string dialogueQuote = dialogueData.dialogueLine.characterQuote;

		for (int i = 0; i < dialogueQuote.Length; i++)
		{
			dialogueText.text += dialogueQuote[i];

			yield return new WaitForSeconds(dialogueWriteDelay);
		}

		CancelInvoke("CheckUserInput");

		print("button spawn input");

		SpawnChoiceButtons(dialogueData);
	}

	private void CheckUserInput()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StopCoroutine(dialoguePlayCoroutine);

			dialogueText.text = currentDialogueData.dialogueLine.characterQuote;

			print("Check user input button");

			SpawnChoiceButtons(currentDialogueData);

			CancelInvoke("CheckUserInput");
		}
	}

	private void SpawnChoiceButtons(DialogueDataSO diaData)
	{
		foreach(DialogueState state in diaData.nextStates)
		{
			GameObject buttonInstance = Instantiate(choiceButtonPrefab, choiceButtonParent);

			buttonInstance.GetComponentInChildren<Text>().text = state.stateQuote;

			buttonInstance.GetComponent<Button>().onClick.AddListener(delegate { OnChoiceButtonPressed(state.nextState); });
		}
	}

	private void OnChoiceButtonPressed(DialogueDataSO diaData)
	{
		dialoguePlayCoroutine = PlayDialogueState(diaData);
		StartCoroutine(dialoguePlayCoroutine);
	}
}
