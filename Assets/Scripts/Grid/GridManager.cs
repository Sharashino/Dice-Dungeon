using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	private static GridManager _instance;
	public static GridManager Instance => _instance;

	[Header("Game Map")] 
	[SerializeField] private Vector2Int mapSize;
	[SerializeField] private GridBlock gridBlockPrefab;
	[SerializeField] private Transform gridParent;
	[SerializeField] private Transform itemsParent;
	[SerializeField] private Transform entitiesParent;
	[Space]
	[SerializeField] private List<GridBlock> map = new();
	[SerializeField] private Sprite[] blockArrows;
	private List<GridBlock> path = new();
	private GridBlock hoveredBlock;
	private bool isPathfinding;

	private Coroutine pathfindingCoroutine;

	public GridBlock HoveredBlock { get => hoveredBlock; set => hoveredBlock = value; }
	public Sprite[] BlockArrows => blockArrows;
	public Transform EntitiesParent => entitiesParent;
	
	private void Awake()
	{
		if (_instance == null) _instance = this;
	}

	private void Start()
	{
		GenerateMap();
		SpawnPlayer();
		ChatLog.LogGameStart();
	}

	private void GenerateMap()
	{
		var xOffset = mapSize.x / 2;
		var yOffset = mapSize.y / 2;

		for (var y = 0; y < mapSize.y; y++)
		for (var x = 0; x < mapSize.x; x++)
		{
			var gridBlock = Instantiate(gridBlockPrefab, gridParent);

			gridBlock.transform.localPosition = new Vector3(x - xOffset, 1, y - yOffset);
			gridBlock.WorldPosition = new Vector3(x - xOffset, 1, y - yOffset);
			gridBlock.GridPosition = new Vector2Int(x, y);
			gridBlock.name = $"GridBlock [{x}|{y}]";
			gridBlock.ShowHideBlockOverlay(false);
			gridBlock.ShowHideBlockArrow(false);
			map.Add(gridBlock);

			SpawnRandomItem(gridBlock);
		}
	}

	private void SpawnRandomItem(GridBlock gridBlock)
	{
		var random = Random.Range(0, 101);
		if (random > 75)
		{
			var item = Instantiate(InventoryManager.Instance.BlockItems.First(), itemsParent);
			item.transform.localPosition = new Vector3(gridBlock.WorldPosition.x, 1.75f, gridBlock.WorldPosition.z);
			gridBlock.BlockItem = item;
		}
	}

	private void SpawnPlayer()
	{
		PlayerManager.Instance.SpawnPlayer(map[Random.Range(0, map.Count)]);
	}
	
	public GridBlock GetGridBlock(Vector2Int gridPos)
	{
		if (gridPos.x < 0 || gridPos.y < 0) return null;

		return map.FirstOrDefault(x => x.GridPosition == gridPos);
	}
	
	public void MovePlayer()
	{
		if (Player.Instance.IsMoving) return;
		
		path = PathFinder.FindPath(Player.Instance.CurrentBlock, hoveredBlock, map);
		Player.Instance.StartCoroutine(Player.Instance.MovePlayerCoroutine(path));
	}

	public void PickUpItem(GridBlock clickedBlock)
	{
		// if not in range act as travel button
		if (Player.Instance.CurrentBlock != clickedBlock)
		{
			path = PathFinder.FindPath(Player.Instance.CurrentBlock, clickedBlock, map);
			MovePlayer();
		}
	
		// pick up item and remove it from map
		Player.Instance.PickUpItem(clickedBlock.BlockItem.Item);
		clickedBlock.RemoveBlockItem();
	}
}
