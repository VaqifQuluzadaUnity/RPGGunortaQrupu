using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEquipmentDetector : MonoBehaviour
{
  [SerializeField] private PlayerEquipmentController equipmentController;

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Equipment"))
		{
			EquipmentDataSO equipmentData =
				other.GetComponent<EquipmentInteractableContoller>().GetEquipmentData();

			equipmentController.EquipItem(equipmentData);
		}

		Destroy(other.gameObject);
	}
}
