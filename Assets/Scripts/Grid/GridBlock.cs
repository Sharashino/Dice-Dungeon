using System.Collections.Generic;
using UnityEngine;

public class GridBlock : MonoBehaviour
{
	[SerializeField] private MeshRenderer meshRenderer;
	[SerializeField] private SpriteRenderer overlayRenderer;
	[SerializeField] private SpriteRenderer arrowRenderer;

	[SerializeField] private Vector2Int gridPosition;
	[SerializeField] private Vector3 worldPosition;
	[SerializeField] private BlockType blockType;
	[SerializeField] private List<BlockActionTypes> blockActions;

	private Color offColor = new Color(0, 0, 0, 0);
	private Color onColor = new Color(1, 1, 1, 1);

	#region A*

	[HideInInspector] public int gCost;
	[HideInInspector] public int hCost;
	public int fCost => gCost + hCost;

	public GridBlock Previous { get; set; }

	#endregion

	public Vector2Int GridPosition { get => gridPosition; set => gridPosition = value; }

	public BlockType BlockType { get => blockType; set => blockType = value; }

	public List<BlockActionTypes> BlockActions { get => blockActions; set => blockActions = value; }
	public Vector3 WorldPosition { get => worldPosition; set => worldPosition = value; }


	public void ShowHideBlockOverlay(bool state)
	{
		overlayRenderer.color = state ? onColor : offColor;
	}

	public void ShowHideBlockArrow(bool state)
	{
		arrowRenderer.color = state ? onColor : offColor;
	}

	public void SetBlockArrow(Directions direction)
	{
		if (direction == Directions.None)
		{
			ShowHideBlockArrow(false);
		}
		else
		{
			ShowHideBlockArrow(true);
			arrowRenderer.sprite = GridManager.Instance.BlockArrows[(int)direction];
		}
	}
}

public enum BlockType
{
	Blocked,
	Walkable
}