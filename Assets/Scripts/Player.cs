using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
	private static Player _instance;
	public static Player Instance => _instance;

	[SerializeField] private Transform transform;
	[SerializeField] private GridBlock currentBlock;

	[SerializeField] private float speed;
	[SerializeField] private Inventory inventory;
	[SerializeField] private Stat health;
	[SerializeField] private Stat mana;
	[SerializeField] private Stat armor;
	[SerializeField] private Stat strength;
	private Coroutine movePlayerCoroutine;
	private bool isMoving;
	
	public GridBlock CurrentBlock { get => currentBlock; set => currentBlock = value; }
	public Transform Transform { get => transform; set => transform = value; }

	public bool IsMoving => isMoving;
	
	private void Awake()
	{
		if (_instance == null) _instance = this;
		inventory = new Inventory(10);
	}
	
	public IEnumerator MovePlayerCoroutine(List<GridBlock> path)
	{
		while (path.Count > 0)
		{
			isMoving = true;
			
			var step = speed * Time.deltaTime;
			var playerVec = new Vector3(transform.position.x, 1.5f, transform.position.z);
			var targetVec = new Vector3(path[0].WorldPosition.x, 1.5f, path[0].WorldPosition.z);

			transform.position = Vector3.MoveTowards(playerVec, targetVec, step);

			if (Vector3.Distance(transform.position, targetVec) < 0.001f)
			{
				currentBlock = path[0];
				currentBlock.ShowHideBlockArrow(false);
				path.RemoveAt(0);
			}

			if (path.Count == 0) isMoving = false;
			yield return null;
		}
	}

	public void PickUpItem(BlockItem blockItem)
	{
		if(blockItem == null) return;
		
		inventory.AddItem(blockItem.Item);
		Debug.Log($"Picking up an item...  {blockItem.Item.Name}");
	}

	public void TakeDamage(float damage)
	{
		health.BaseValue -= damage;
		Debug.Log($"Player took {damage} damage");
		
		if(health.BaseValue <= 0) Die();
	}

	public void Heal(float amount)
	{
		health.BaseValue = Mathf.Clamp(health.BaseValue += amount, 0, 100);
		Debug.Log($"Player healed for {amount} health");
	}

	public void Die()
	{
		Debug.Log("Player died");
	}
}