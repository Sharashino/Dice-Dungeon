using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class ActionsController : Menu
{
	[SerializeField] private Button actionButtonPrefab;
	[SerializeField] private Button exitButton;

	[SerializeField] private Transform actionButtonsParent;

	private Camera mainCamera;

	private void Start()
	{
		mainCamera = Camera.main;

		exitButton.onClick.AddListener(() => ShowHideMenu(false, null));
		ShowHideMenu(false, null);
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
		GridManager.Instance.MovePlayer();
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
	
	public override void ShowHideMenu(bool state, GridBlock clickedBlock)
	{
		if (state && clickedBlock != null)
		{
			GetActionMenuSize(clickedBlock);
			var point = mainCamera.WorldToScreenPoint(clickedBlock.transform.position);
			transform.position = new Vector3(point.x + 60, point.y - 30, point.z);

			menuCanvasGroup.Enable();
		}
		else
		{
			menuCanvasGroup.Disable();
		}
	}
}

public enum BlockActionTypes
{
	Travel,
	Examine,
}