using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode
    {
        TextureMap,
        NoiseMap,
        ColorMap
    }
    
    [Header("Map Settings")]
    [SerializeField] private int mapSeed = 10;
    [SerializeField] private int mapWidth = 10;
    [SerializeField] private int mapHeight = 10;
    [SerializeField] private float mapDepth = 10;
    [SerializeField] private int cellSize = 10;
    [SerializeField] private int octaves;
    [Range(0,1), SerializeField] private float persistence;
    [SerializeField] private float lacunarity;
    [SerializeField] private Vector2 offset;
    [SerializeField] private DrawMode drawMode;
    [Space]
    [SerializeField, Space] private bool autoUpdate;
    [Header("Map Objects")]
    [SerializeField] private MapDisplay mapDisplay;
    [SerializeField] private WorldGrid worldGrid;
    [Header("Map Data")]
    [SerializeField] private CellObject[] worldObjects;
    [SerializeField] private GridCell[,] grid;
    [SerializeField] private CellObject[] cellPrefabs;
    [SerializeField] private TerrainType[] regions;
    public bool AutoUpdate => autoUpdate;
    public float Offset => cellSize / 2;

    public void GenerateMap()
    {
        DestroyOldMap();
        
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, mapSeed, mapDepth, octaves, persistence, lacunarity, offset);
        worldObjects = new CellObject[mapWidth * mapHeight];
        grid = new GridCell[mapWidth, mapHeight];
        
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        var pos = GetGridCellWorldPosition(x, y);
                        worldObjects[x + y] = Instantiate(cellPrefabs[0], transform);
                        var cell = worldObjects[x + y];
                        cell.MeshRenderer.material = regions[i].material;
                        cell.transform.position = new Vector3(pos.x + Offset, -0.1f, pos.z + Offset);
                        grid[x, y] = new GridCell(new Vector2Int(x, y), pos);
                        break;
                    }
                }
            }
        }
    }

    public void DestroyOldMap()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
        
        Debug.Log("Deleted old map...");
    }

    public void OnValidate()
    {
        if (mapWidth < 1) mapWidth = 1;
        if (mapHeight < 1) mapHeight = 1;
        if (lacunarity < 1) lacunarity = 1;
        if (octaves < 0) octaves = 0;
    }
    
    private Vector3Int GetGridCellWorldPosition(int x, int y)
    {
        Vector3 vec = new Vector3(x, 0, y) * cellSize + transform.position;
        return Vector3Int.FloorToInt(vec);
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
    public Material material;
}
