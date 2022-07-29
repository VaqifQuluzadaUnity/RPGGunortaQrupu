using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentInteractableContoller : MonoBehaviour
{
  [SerializeField] private EquipmentDataSO equipmentData;

  public EquipmentDataSO GetEquipmentData()
	{
		return equipmentData;
	}
}
