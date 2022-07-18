using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public enum DrawMode
    {
        NoiseMap,
        ColorMap
    }
    
    [Header("Map Settings")]
    [SerializeField] private int mapSeed = 10;
    [SerializeField] private int mapWidth = 10;
    [SerializeField] private int mapHeight = 10;
    [SerializeField] private float mapDepth = 10;
    [SerializeField] private int octaves;
    [Range(0,1), SerializeField] private float persistence;
    [SerializeField] private float lacunarity;
    [SerializeField] private Vector2 offset;
    [SerializeField] private DrawMode drawMode;
    [Space]
    [SerializeField, Space] private bool autoUpdate;
    [Header("Map Objects")]
    [SerializeField] private MapDisplay mapDisplay;

    [SerializeField] private TerrainType[] regions;
    public bool AutoUpdate => autoUpdate;
    
    public void GenerateMap()
    {
        float[,] noiseMap = Noise.GenerateNoiseMap(mapWidth, mapHeight, mapSeed, mapDepth, octaves, persistence, lacunarity, offset);
        Color[] colorMap = new Color[mapWidth * mapHeight];
        
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float currentHeight = noiseMap[x, y];
                
                for (int i = 0; i < regions.Length; i++)
                {
                    if (currentHeight <= regions[i].height)
                    {
                        colorMap[y * mapWidth + x] = regions[i].color;
                        break;
                    }
                }
            }
        }
        
        if (drawMode == DrawMode.NoiseMap) mapDisplay.DrawTexture(TextureGenerator.TextureFromHeightMap(noiseMap));
        else if (drawMode == DrawMode.ColorMap) mapDisplay.DrawTexture(TextureGenerator.TextureFromColorMap(colorMap, mapWidth, mapHeight));
    }

    public void OnValidate()
    {
        if (mapWidth < 1) mapWidth = 1;
        if (mapHeight < 1) mapHeight = 1;
        if (lacunarity < 1) lacunarity = 1;
        if (octaves < 0) octaves = 0;
    }
}

[System.Serializable]
public struct TerrainType
{
    public string name;
    public float height;
    public Color color;
    
}
