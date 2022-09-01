using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridManager : MonoBehaviour
{
	private static GridManager _instance;
	public static GridManager Instance => _instance;

	[Header("Game Map")] [SerializeField] private Vector2Int mapSize;
	[SerializeField] private GridBlock gridBlockPrefab;
	[SerializeField] private Transform gridParent;
	[SerializeField] private Transform itemsParent;
	[SerializeField] private Transform entitiesParent;
	
	[SerializeField] private Player player;
	[SerializeField] private List<GridBlock> map = new();
	[SerializeField] private Sprite[] blockArrows;

	private List<GridBlock> blocksInRange = new();
	private List<GridBlock> path = new();
	private GridBlock hoveredBlock;
	private bool isPathfinding;

	private Coroutine pathfindingCoroutine;

	public GridBlock HoveredBlock { get => hoveredBlock; set => hoveredBlock = value; }
	public Sprite[] BlockArrows => blockArrows;
	public bool IsPathfinding => isPathfinding;
	public Player Player => player;
	
	private void Awake()
	{
		if (_instance == null) _instance = this;
	}

	private void Start()
	{
		GenerateMap();
		SpawnPlayer();
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

			RandomItemSpawn(gridBlock);
		}
	}

	private void RandomItemSpawn(GridBlock gridBlock)
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
		var randBlock = map[Random.Range(0, map.Count)];
		player = Instantiate(GameManager.Instance.PlayerPrefab, entitiesParent);
		
		player.transform.position = new Vector3(randBlock.WorldPosition.x, 1.5f, randBlock.WorldPosition.z);
		player.CurrentBlock = randBlock;
	}

	private List<GridBlock> GetTravelRange()
	{
		// Make old blocks invisible
		ShowHideTravelRange(false);

		blocksInRange =
			PathFinder.GetBlocksInRange(Player.Instance.CurrentBlock, Player.Instance.TravelRange);

		ShowHideTravelRange(true);

		return blocksInRange;
	}

	private void ShowHideTravelRange(bool state)
	{
		foreach (var block in blocksInRange)
		{
			if (block == null) continue;
			block.ShowHideBlockOverlay(state);
		}
	}

	private void SetBlockArrows(Directions direction)
	{
		foreach (var block in blocksInRange)
		{
			if (block == null) continue;
			block.SetBlockArrow(direction);
		}
	}

	private IEnumerator DrawPathArrows()
	{
		while (isPathfinding)
		{
			path = PathFinder.FindPath(Player.Instance.CurrentBlock, hoveredBlock, blocksInRange);

			SetBlockArrows(Directions.None);

			for (var i = 0; i < path.Count; i++)
			{
				var prev = i > 0 ? path[i - 1] : Player.Instance.CurrentBlock;
				var future = i < path.Count - 1 ? path[i + 1] : null;
				var arrowDir = PathTranslator.TranslateDirection(prev, path[i], future);
				path[i].SetBlockArrow(arrowDir);
			}

			yield return null;
		}
	}
	
	public GridBlock GetTile(Vector2Int gridPos)
	{
		if (gridPos.x < 0 || gridPos.y < 0) return null;

		return map.FirstOrDefault(x => x.GridPosition == gridPos);
	}

	public void StartPathfinding()
	{
		isPathfinding = true;
		blocksInRange = GetTravelRange();
		pathfindingCoroutine = StartCoroutine(DrawPathArrows());
	}

	public void EndPathfinding()
	{
		if (pathfindingCoroutine != null)
		{
			StopCoroutine(pathfindingCoroutine);
			pathfindingCoroutine = null;
		}

		SetBlockArrows(Directions.None);
		ShowHideTravelRange(false);
		isPathfinding = false;
	}

	public void MovePlayer()
	{
		EndPathfinding();
		player.StartCoroutine(player.MovePlayerCoroutine(path));
	}

	public void PickUpItem(GridBlock clickedBlock)
	{
		// if not in range act as travel button
		if (player.CurrentBlock != clickedBlock)
		{
			blocksInRange = GetTravelRange();
			path = PathFinder.FindPath(Player.Instance.CurrentBlock, clickedBlock, blocksInRange);
			Debug.Log(path.Count);
			MovePlayer();
		}
	
		// pick up item and remove it from map
		player.PickUpItem(clickedBlock.BlockItem);
		clickedBlock.RemoveBlockItem();
	}
}
