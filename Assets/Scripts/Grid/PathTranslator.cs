using UnityEngine;

public static class PathTranslator
{
	public static Directions TranslateDirection(GridBlock prevBlock, GridBlock currentBlock, GridBlock futureBlock)
	{
		var isFinal = futureBlock == null;

		var pastDirection =
			prevBlock != null ? currentBlock.GridPosition - prevBlock.GridPosition : new Vector2Int(0, 0);
		var futureDirection =
			futureBlock != null ? futureBlock.GridPosition - currentBlock.GridPosition : new Vector2Int(0, 0);
		var dir = pastDirection != futureDirection ? pastDirection + futureDirection : futureDirection;

		if (dir == new Vector2Int(0, 1) && !isFinal) return Directions.Up;
		if (dir == new Vector2Int(0, -1) && !isFinal) return Directions.Down;
		if (dir == new Vector2Int(1, 0) && !isFinal) return Directions.Right;
		if (dir == new Vector2Int(-1, 0) && !isFinal) return Directions.Left;

		if (dir == new Vector2Int(1, 1) && !isFinal)
		{
			if (pastDirection.y < futureDirection.y) return Directions.DownLeft;
			return Directions.UpRight;
		}

		if (dir == new Vector2Int(-1, 1) && !isFinal)
		{
			if (pastDirection.y < futureDirection.y) return Directions.DownRight;
			return Directions.UpLeft;
		}

		if (dir == new Vector2Int(1, -1) && !isFinal)
		{
			if (pastDirection.y > futureDirection.y) return Directions.UpLeft;
			return Directions.DownRight;
		}

		if (dir == new Vector2Int(-1, -1) && !isFinal)
		{
			if (pastDirection.y > futureDirection.y) return Directions.UpRight;
			return Directions.DownLeft;
		}

		if (dir == new Vector2Int(0, 1) && isFinal) return Directions.UpFinish;
		if (dir == new Vector2Int(0, -1) && isFinal) return Directions.DownFinish;
		if (dir == new Vector2Int(1, 0) && isFinal) return Directions.RightFinish;
		if (dir == new Vector2Int(-1, 0) && isFinal) return Directions.LeftFinish;

		return Directions.None;
	}
}

/*
 *
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
			path = PathFinder.FindPath(Player.Instance.CurrentBlock, hoveredBlock, map);

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
 * 
 */


public enum Directions
{
	None,
	Up,
	Down,
	Left,
	Right,
	UpRight,
	DownRight,
	UpLeft,
	DownLeft,
	UpFinish,
	DownFinish,
	LeftFinish,
	RightFinish
}