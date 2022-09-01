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
	
	[SerializeField] private PlayerManager player;
	[SerializeField] private List<GridBlock> map = new List<GridBlock>();
	[SerializeField] private Sprite[] blockArrows;

	private List<GridBlock> blocksInRange = new List<GridBlock>();
	private List<GridBlock> path = new List<GridBlock>();
	private GridBlock hoveredBlock;
	private bool isPathfinding = false;

	private Coroutine pathfindingCoroutine;

	public GridBlock HoveredBlock { get => hoveredBlock; set => hoveredBlock = value; }
	public Sprite[] BlockArrows => blockArrows;
	public bool IsPathfinding => isPathfinding;

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
		}
	}

	private void SpawnPlayer()
	{
		var randBlock = map[Random.Range(0, map.Count)];
		var newPlayer = Instantiate(player, entitiesParent);
		
		newPlayer.transform.localPosition = new Vector3(randBlock.WorldPosition.x, 1.5f, randBlock.WorldPosition.z);
		newPlayer.CurrentBlock = randBlock;
	}

	public GridBlock GetTile(Vector2Int gridPos)
	{
		if (gridPos.x < 0 || gridPos.y < 0) return null;

		return map.FirstOrDefault(x => x.GridPosition == gridPos);
	}

	public List<GridBlock> GetTravelRange()
	{
		// Make old blocks invisible
		foreach (var block in blocksInRange)
		{
			if (block == null) continue;
			block.ShowHideBlockOverlay(false);
		}

		blocksInRange =
			PathFinder.GetBlocksInRange(PlayerManager.Instance.CurrentBlock, PlayerManager.Instance.PlayerRange);

		ShowHideTravelRange(true);

		return blocksInRange;
	}

	public void StartPathfinding()
	{
		pathfindingCoroutine = StartCoroutine(DrawPathArrows());
		blocksInRange = GetTravelRange();
		isPathfinding = true;
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

	private IEnumerator DrawPathArrows()
	{
		while (true)
		{
			path = PathFinder.FindPath(PlayerManager.Instance.CurrentBlock, HoveredBlock, blocksInRange);

			SetBlockArrows(Directions.None);

			for (var i = 0; i < path.Count; i++)
			{
				var prev = i > 0 ? path[i - 1] : PlayerManager.Instance.CurrentBlock;
				var future = i < path.Count - 1 ? path[i + 1] : null;
				var arrowDir = PathTranslator.TranslateDirection(prev, path[i], future);
				path[i].SetBlockArrow(arrowDir);
			}

			yield return null;
		}
	}

	public void ShowHideTravelRange(bool state)
	{
		foreach (var block in blocksInRange)
		{
			if (block == null) continue;
			block.ShowHideBlockOverlay(state);
		}
	}

	public void SetBlockArrows(Directions direction)
	{
		foreach (var block in blocksInRange)
		{
			if (block == null) continue;
			block.SetBlockArrow(direction);
		}
	}

	public void MovePlayer()
	{
		if (path.Count > 0)
		{
			player.transform.position = path.Last().WorldPosition;
		}
	}
}