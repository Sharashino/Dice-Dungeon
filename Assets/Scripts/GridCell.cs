using UnityEngine;

[System.Serializable]
public class GridCell
{
    public Vector2Int GridPosition;
    public Vector3 WorldPosition;
    public CellObject CellObject;
    public CellType CellType;
    
    public GridCell(Vector2Int gridPos = default, Vector3Int worldPos = default, CellType cellType = default)
    {
        GridPosition = gridPos;
        WorldPosition = worldPos;
        CellType = cellType;
    }
    
   
}
