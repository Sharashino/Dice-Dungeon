using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public static class PathFinder
{
	public static List<GridBlock> GetBlocksInRange(GridBlock start, int range)
	{
		var blocks = new List<GridBlock> { start };
		var stepCount = 0;

		var blockForPrevStep = new List<GridBlock> { start };

		while (stepCount < range)
		{
			var neighbours = new List<GridBlock>();

			foreach (var block in blockForPrevStep)
			{
				if (block == null) continue;

				neighbours.AddRange(GetNeighbours(block));
			}

			blocks.AddRange(neighbours);
			blockForPrevStep = neighbours.Distinct().ToList();
			stepCount++;
		}

		return blocks.Distinct().ToList();
	}

	public static List<GridBlock> FindPath(GridBlock start, GridBlock end, List<GridBlock> searchable)
	{
		var openList = new List<GridBlock> { start };
		var closedList = new List<GridBlock>();

		while (openList.Count > 0)
		{
			var currentBlock = openList.OrderBy(x => x.fCost).First();

			openList.Remove(currentBlock);
			closedList.Add(currentBlock);

			if (currentBlock == end) return GetPath(start, end);

			var neighbours = GetNeighbours(currentBlock, searchable);

			foreach (var block in neighbours)
			{
				if (closedList.Contains(block) || block == null) continue;

				block.gCost = GetManhattanDistance(start, block);
				block.hCost = GetManhattanDistance(end, block);
				block.Previous = currentBlock;

				if (!openList.Contains(block)) openList.Add(block);
			}
		}

		return new List<GridBlock>();
	}

	private static List<GridBlock> GetPath(GridBlock startBlock, GridBlock endBlock)
	{
		var path = new List<GridBlock>();
		var currentBlock = endBlock;

		while (currentBlock != startBlock)
		{
			path.Add(currentBlock);
			currentBlock = currentBlock.Previous;
		}

		path.Reverse();
		return path;
	}

	private static int GetManhattanDistance(GridBlock startBlock, GridBlock neighbour)
	{
		if (startBlock == null || neighbour == null) return 0;

		return Mathf.Abs(startBlock.GridPosition.x - neighbour.GridPosition.x) +
		       Mathf.Abs(startBlock.GridPosition.y - neighbour.GridPosition.y);
	}

	private static List<GridBlock> GetNeighbours(GridBlock currentBlock)
	{
		var grid = GridManager.Instance;

		var neighbours = new List<GridBlock>
		{
			grid.GetTile(new Vector2Int(currentBlock.GridPosition.x + 1, currentBlock.GridPosition.y)),
			grid.GetTile(new Vector2Int(currentBlock.GridPosition.x - 1, currentBlock.GridPosition.y)),
			grid.GetTile(new Vector2Int(currentBlock.GridPosition.x, currentBlock.GridPosition.y + 1)),
			grid.GetTile(new Vector2Int(currentBlock.GridPosition.x, currentBlock.GridPosition.y - 1))
		};

		return neighbours;
	}

	private static List<GridBlock> GetNeighbours(GridBlock currentBlock, List<GridBlock> searchable)
	{
		var blocksToSearch = new Dictionary<Vector2Int, GridBlock>();

		if (searchable.Count == 0) return new List<GridBlock>();

		foreach (var block in searchable)
		{
			if (block == null) continue;
			blocksToSearch.Add(block.GridPosition, block);
		}

		var right = new Vector2Int(currentBlock.GridPosition.x + 1, currentBlock.GridPosition.y);
		var left = new Vector2Int(currentBlock.GridPosition.x - 1, currentBlock.GridPosition.y);
		var up = new Vector2Int(currentBlock.GridPosition.x, currentBlock.GridPosition.y + 1);
		var down = new Vector2Int(currentBlock.GridPosition.x, currentBlock.GridPosition.y - 1);

		return new List<GridBlock>
		{
			blocksToSearch.ContainsKey(right) ? blocksToSearch[right] : null,
			blocksToSearch.ContainsKey(left) ? blocksToSearch[left] : null,
			blocksToSearch.ContainsKey(up) ? blocksToSearch[up] : null,
			blocksToSearch.ContainsKey(down) ? blocksToSearch[down] : null
		};
	}
}