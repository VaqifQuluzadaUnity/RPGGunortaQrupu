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

	[SerializeField] DialogueDataSO currentStateData;

	private IEnumerator currentCoroutine;

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



	#region Event Handlers

	private void OnDialogueEnterEventHandler(OnDialogueEnterEvent eventDetails)
	{
		currentCoroutine = PlayDialogue(eventDetails.DialogueData);

		StartCoroutine(currentCoroutine);
	}

	#endregion

	#region Coroutines

	IEnumerator PlayDialogue(DialogueDataSO dialogueData)
	{
		currentStateData = dialogueData;

		dialogueText.text = "";

		InvokeRepeating("CheckSpaceSkip", 0, Time.deltaTime);

		for(int i = 0; i < choiceButtonParent.childCount; i++)
		{
			Destroy(choiceButtonParent.GetChild(i).gameObject);
		}

		string dialogueQuote = dialogueData.dialogueLine.characterQuote;

		for (int i = 0; i < dialogueQuote.Length; i++)
		{
			dialogueText.text += dialogueQuote[i];

			yield return new WaitForSeconds(dialogueWriteDelay);
		}

		CancelInvoke("CheckSpawnSkip");

		SpawnChoiceButtons(currentStateData);
	}


	private void CheckSpaceSkip()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			StopCoroutine(currentCoroutine);

			dialogueText.text = currentStateData.dialogueLine.characterQuote;

			SpawnChoiceButtons(currentStateData);

			CancelInvoke("CheckSpaceSkip");
		}

		
	}

	private void SpawnChoiceButtons(DialogueDataSO diaData)
	{
		for(int i = 0; i < diaData.nextStates.Length; i++)
		{
			GameObject choiceButtonInstance = Instantiate(choiceButtonPrefab, choiceButtonParent);

			choiceButtonInstance.GetComponentInChildren<Text>().text = 
				diaData.nextStates[i].stateQuestionQuote;


			int index = i;

			choiceButtonInstance.GetComponent<Button>().
				onClick.AddListener(delegate { OnChoiceButtonPressed(index,diaData); });

		}
	}

	private void OnChoiceButtonPressed(int choiceIndex,DialogueDataSO diaData)
	{

		DialogueDataSO nextState = diaData.nextStates[choiceIndex].nextState;

		StartCoroutine(PlayDialogue(nextState));
	}

	#endregion

}
