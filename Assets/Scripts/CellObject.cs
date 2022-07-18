using UnityEngine;

public class CellObject : MonoBehaviour
{
    [SerializeField] private MeshRenderer meshRenderer;
    
    public MeshRenderer MeshRenderer => meshRenderer;
}

