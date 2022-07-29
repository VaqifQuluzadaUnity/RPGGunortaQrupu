using DynamicBox.EventManagement;
using UnityEngine;

public class PlayerColllisionDetector : MonoBehaviour
{
	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("DialogueNPC"))
		{
			DialogueDataSO dialogueData = 
				other.GetComponent<NPCDialogueHolder>().ReturnDialogueData();

			EventManager.Instance.Raise(new OnDialogueEnterEvent(dialogueData));
		}
	}
}
