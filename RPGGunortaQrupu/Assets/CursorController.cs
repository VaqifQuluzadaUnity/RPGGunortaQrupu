using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorController : MonoBehaviour
{
  [SerializeField] private Texture2D enemyCursor;

  [SerializeField] private Texture2D chestCursor;

  [SerializeField] private Texture2D groundCursor;

  [SerializeField] private Texture2D rockCursor;

	private void Start()
	{
		InvokeRepeating("DetectCursor", 0, 0.05f);
	}

	private void DetectCursor()
	{
		Ray mousePosRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hit;

		if(Physics.Raycast(mousePosRay,out hit))
		{
			switch (hit.collider.tag)
			{
				case "Enemy":
					Cursor.SetCursor(enemyCursor, Vector2.zero, CursorMode.Auto);
					break;
				case "Chest":
					Cursor.SetCursor(chestCursor, Vector2.zero, CursorMode.Auto);
					break;
				case "Ground":
					Cursor.SetCursor(groundCursor, Vector2.zero, CursorMode.Auto);
					break;
				case "Rock":
					Cursor.SetCursor(rockCursor, Vector2.zero, CursorMode.Auto);
					break;
			}
			
		}
		else
		{
			Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
		}
	}



}
