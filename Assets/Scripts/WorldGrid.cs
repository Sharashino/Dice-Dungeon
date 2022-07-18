using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    [SerializeField] private Material[] worldMaterials;
    [SerializeField] private GameObject[] cellPrefabs;
    [SerializeField] private GameObject[] worldObjects;
    [SerializeField] private GridCell[,] grid;
    [SerializeField] private int cellSize;
    [SerializeField] private int height;
    [SerializeField] private int width;

    public float Offset => cellSize / 2;
    
    void Start()
    {
        InitializeWorldGrid();
    }

    private void InitializeWorldGrid()
    {
        worldObjects = new GameObject[width * height];
        grid = new GridCell[width, height];
        
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                var pos = GetGridCellWorldPosition(x, y);
                worldObjects[x + y] = Instantiate(cellPrefabs[0].gameObject, transform);
                
                var obj = worldObjects[x + y];
                Debug.Log(pos);
                obj.transform.position = new Vector3(pos.x + Offset, -0.1f, pos.z + Offset);
                grid[x, y] = new GridCell(new Vector2Int(x, y), pos);
            }
        }
    }

    private Vector3Int GetGridCellWorldPosition(int x, int y)
    {
        Vector3 vec = new Vector3(x, 0, y) * cellSize + transform.position;
        return Vector3Int.FloorToInt(vec);
    }
}
