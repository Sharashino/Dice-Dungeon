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
	[SerializeField] private Stat intelligence;
	[SerializeField] private Stat agility;
	[SerializeField] private Stat luck;
	private Coroutine movePlayerCoroutine;
	private bool isMoving;
	
	public GridBlock CurrentBlock { get => currentBlock; set => currentBlock = value; }

	public Stat Health => health;
	public Stat Mana => mana;
	public Stat Armor => armor;
	public Stat Strength => strength;
	public Stat Intelligence => intelligence;
	public Stat Agility => agility;
	public Stat Luck => luck;
	
	public bool IsMoving => isMoving;
	
	private void Awake()
	{
		if (_instance == null) _instance = this;
		inventory = new Inventory(10);
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.H)) TakeDamage(10);
		if (Input.GetKeyDown(KeyCode.J)) Heal(10);
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

	public void PickUpItem(Item item)
	{
		if(item == null) return;
		
		inventory.AddItem(item);
		
		ChatLog.LogPickup(item);
	}

	public void TakeDamage(float amount)
	{
		health.BaseValue -= amount;
		HealthBar.OnHealthChanged.Invoke(health.BaseValue);

		if (health.BaseValue <= 0)
		{
			Die();
			return;
		}
		
		ChatLog.LogDamage((int)amount);
	}

	public void Heal(float amount)
	{
		health.BaseValue = Mathf.Clamp(health.BaseValue += amount, 0, 100);
		HealthBar.OnHealthChanged.Invoke(health.BaseValue);
		
		ChatLog.LogHeal((int)amount);
	}

	public void Die()
	{
		// TODO Add death animation
		// TODO Add death screen
		// TODO Add death sound
		
		ChatLog.LogDeath();
	}
}