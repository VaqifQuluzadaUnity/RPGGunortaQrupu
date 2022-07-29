using UnityEngine;

public class PlayerEquipmentController : MonoBehaviour
{
	[SerializeField] private SkinnedMeshRenderer characterSMR;

	[Header("Equipment Pieces")]
	[SerializeField] private GameObject headEquipment;

	[SerializeField] private GameObject bodyEquipment;

	[SerializeField] private GameObject LegsEquipment;

	[SerializeField] private GameObject FeetEquipment;

	[Header("Equipment Data")]

	[SerializeField] private EquipmentDataSO headData;

	[SerializeField] private EquipmentDataSO bodyData;

	[SerializeField] private EquipmentDataSO legData;

	[SerializeField] private EquipmentDataSO feetData;

	public void EquipItem(EquipmentDataSO equipmentData)
	{
		GameObject equipmentInstance = Instantiate(equipmentData.equipmentSMR,transform);

		SkinnedMeshRenderer equipmentSMR = equipmentInstance.GetComponent<SkinnedMeshRenderer>();

		equipmentSMR.rootBone = characterSMR.rootBone;

		equipmentSMR.bones = characterSMR.bones;


		switch (equipmentData.equipmentType)
		{
			case EquipmentType.HEAD:
				headEquipment = equipmentInstance;

				headData = equipmentData;

				break;

			case EquipmentType.BODY:

				bodyEquipment = equipmentInstance;

				bodyData = equipmentData;

				break;

			case EquipmentType.LEGS:

				LegsEquipment = equipmentInstance;

				legData = equipmentData;

				break;

			case EquipmentType.FEET:

				FeetEquipment = equipmentInstance;

				feetData = equipmentData;

				break;
		}



	}
}
