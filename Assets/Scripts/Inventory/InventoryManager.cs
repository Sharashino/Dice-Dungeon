using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
	private static InventoryManager _instance;
	public static InventoryManager Instance => _instance;
	
	public List<BlockItem> BlockItems = new();

	private void Awake()
	{
		if(_instance == null) _instance = this;
	}
	
	
}