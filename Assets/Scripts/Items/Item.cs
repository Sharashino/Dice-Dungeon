using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{
	public int Id;
	public string Name;
	public Sprite Sprite;
	public string Description;
	public int Price;
	public int Quantity;

	[Header("Stats")]
	public bool Stackable;
	public int StackSize;
	

	public void AddToStack()
	{
		Quantity = Mathf.Clamp(Quantity, 1, StackSize);
	}

	public void RemoveFromStack()
	{
		Quantity = Mathf.Clamp(Quantity, 0, StackSize);
	}
}
