using UnityEngine;

public class WorldGrid : MonoBehaviour
{
    [SerializeField] private Material[] worldMaterials;
    
    [SerializeField] private int cellSize;
    [SerializeField] private int height;
    [SerializeField] private int width;

    public float Offset => cellSize / 2;
   

    
}
