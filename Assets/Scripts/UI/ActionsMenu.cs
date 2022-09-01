using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

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

	private void GetActionMenuSize(GridBlock actionBlock)
	{
		foreach (Transform btn in actionButtonsParent) Destroy(btn.gameObject);

		foreach (var action in DiceDungeonUtils.GetEnumValues<BlockActionTypes>())
			if (actionBlock.BlockActions.Contains(action))
			{
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
						newButton.onClick.AddListener(() => OnExamineClick(actionBlock));
						break;
					}
				}
			}
	}

	private void OnTravelClick()
	{
		ShowHideMenu(false, null);
		GridManager.Instance.StartPathfinding();
	}

	private void OnExamineClick(GridBlock gridBlock)
	{
		ShowHideMenu(false, null);
		Debug.Log($"Pretty interesting block...  {gridBlock.GridPosition}");
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
	PickUp
}