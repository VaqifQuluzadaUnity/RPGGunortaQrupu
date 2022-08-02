using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
  [SerializeField] private GameObject canvasObjectPrefab;

	[SerializeField] private int objectCount;

	private void Start()
	{
		for(int x = 0; x < objectCount; x++)
		{
			for(int y = 0; y < objectCount; y++)
			{
				Vector3 objectPos = new Vector3(x, 0, y);
				GameObject canvasObjectInstance = Instantiate(canvasObjectPrefab,objectPos,Quaternion.identity);
			}
		}
	}
}
