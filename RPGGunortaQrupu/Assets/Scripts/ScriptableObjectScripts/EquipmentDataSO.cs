using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="NewEquipmentData",
  menuName ="ScriptableObjects/EquipmentData")]
public class EquipmentDataSO : ScriptableObject
{
  public EquipmentType equipmentType;

  public Sprite equipmentIcon;

  public GameObject equipmentSMR;

  public string EquipmentName;

  public float equipmentHealth;

  public float equipmentResistance;
}

public enum EquipmentType {NONE,HEAD,BODY,LEGS,FEET };
