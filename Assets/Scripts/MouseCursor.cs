using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MouseCursor : MonoBehaviour
{
	[SerializeField] private Camera mainCamera;
	[SerializeField] private List<GridBlock> path;
	private List<GridBlock> blocksInRange = new List<GridBlock>();
	private GridBlock hoveredBlock;
	private GridBlock endBlock;
	private bool isPathfinding = false;
	private bool isMoving;

	public GridBlock HoveredBlock {
		get => GridManager.Instance.HoveredBlock;
		set => GridManager.Instance.HoveredBlock = value;
	}

	private void Awake()
	{
		InputActions.OnRightClick += OnRightClick;
		InputActions.OnLeftClick += OnLeftClick;
	}

	private void OnRightClick()
	{
		InputActions.OnRightBlockClick?.Invoke(HoveredBlock);
	}

	private void OnLeftClick()
	{
		GridManager.Instance.MovePlayer();
	}

	private void LateUpdate()
	{
		HoveredBlock = FocusOnTile();
		SetCursorPosition();
	}

	private void SetCursorPosition()
	{
		if (HoveredBlock != null)
			transform.localPosition = new Vector3(HoveredBlock.WorldPosition.x, 1.525f, HoveredBlock.WorldPosition.z);
	}

	private GridBlock FocusOnTile()
	{
		if (GameUIController.Instance.GameMenus.Any(x => x.IsShown)) return null;
		if (DiceDungeonUtils.IsMouseOverUI()) return null;

		var ray = mainCamera.ScreenPointToRay(Input.mousePosition);

		if (Physics.Raycast(ray, out var hit))
		{
			var gridBlock = hit.transform.GetComponent<GridBlock>();
			if (gridBlock != null) return gridBlock;
		}

		return null;
	}
}