using System.Collections.Generic;
using System;

[Serializable]
public class Inventory
{
	public List<Item> items = new();
	public int inventorySize;


	public Inventory(int size)
	{
		inventorySize = size;
	}

	public void AddItem(Item item)
	{
		if (items.Count <= inventorySize)
		{
			items.Add(item);
		}
	}
	
	public void RemoveItem(Item item)
	{
		if (items.Contains(item))
		{
			items.Remove(item);
		}
	}
}
