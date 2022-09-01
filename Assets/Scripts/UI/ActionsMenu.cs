using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class ActionsMenu : Menu
{
	[SerializeField] private Button actionButtonPrefab;
	[SerializeField] private Button exitButton;

	[SerializeField] private Transform actionButtonsParent;

	[SerializeField] private List<Button> actionButtons = new List<Button>();

	private Camera mainCamera;

	private void Start()
	{
		mainCamera = Camera.main;

		exitButton.onClick.AddListener(() => ShowHideActionsMenu(false, null));
		ShowHideActionsMenu(false, null);
	}

	private void ShowHideActionsMenu(bool state, GridBlock relativeBlock)
	{
		if (state)
		{
			GetActionMenuSize(relativeBlock);
			var point = mainCamera.WorldToScreenPoint(relativeBlock.transform.position);
			transform.position = new Vector3(point.x + 60, point.y - 30, point.z);

			menuCanvasGroup.Enable();
		}
		else
		{
			menuCanvasGroup.Disable();
		}
	}

	private void GetActionMenuSize(GridBlock clickedBlock)
	{
		foreach (Transform btn in actionButtonsParent) Destroy(btn.gameObject);

		foreach (var action in DiceDungeonUtils.GetEnumValues<BlockActionTypes>())
		{
			if (!clickedBlock.BlockActions.Contains(action)) continue;
			
			var newButton = Instantiate(actionButtonPrefab, actionButtonsParent);
			
			switch (action)
			{
				case BlockActionTypes.Travel:
				{
					newButton.GetComponentInChildren<TMP_Text>().text = "Travel";
					newButton.onClick.AddListener(OnTravelClick);
					break;
				}
				case BlockActionTypes.Examine:
				{
					newButton.GetComponentInChildren<TMP_Text>().text = "Examine";
					newButton.onClick.AddListener(() => OnExamineClick(clickedBlock));
					break;
				}
			}
		}
		
		if (clickedBlock.BlockItem != null)
		{
			var newButton = Instantiate(actionButtonPrefab, actionButtonsParent);
			newButton.GetComponentInChildren<TMP_Text>().text = "Pick Up";
			newButton.onClick.AddListener(() => OnPickUpClick(clickedBlock));
		}
	}

	private void OnTravelClick()
	{
		ShowHideMenu(false, null);
		GridManager.Instance.StartPathfinding();
	}

	private void OnExamineClick(GridBlock clickedBlock)
	{
		ShowHideMenu(false, null);
		Debug.Log($"Pretty interesting block...  {clickedBlock.GridPosition}");
	}

	private void OnPickUpClick(GridBlock clickedBlock)
	{
		GridManager.Instance.PickUpItem(clickedBlock);
		ShowHideMenu(false, null);
	}
	
	public override void ShowHideMenu(bool state, object obj)
	{
		base.ShowHideMenu(state, obj);

		if (obj as GridBlock == null) return;
		ShowHideActionsMenu(state, (GridBlock)obj);
	}
}

public enum BlockActionTypes
{
	Travel,
	Examine,
}