using UnityEngine;

public class BlockItem : MonoBehaviour
{
	[SerializeField] private Item gridItem;

	public Item Item { get => gridItem; set => gridItem = value; }
}