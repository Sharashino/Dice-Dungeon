using UnityEngine;
using System;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Room", menuName = "Room/New Room"), Serializable]
public class Room : ScriptableObject
{
	public Vector2Int size;
	public List<GridBlock> cells = new List<GridBlock>(); 
	private bool[,] roomCells;
}
