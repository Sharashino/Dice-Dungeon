using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	private static PlayerManager _instance;
	public static PlayerManager Instance => _instance;

	[SerializeField] private Transform player;
	[SerializeField] private GridBlock currentBlock;

	[Header("Player Stats")] 
	[SerializeField] private int travelRange;
	[SerializeField] private int pickUpRange;

	[SerializeField] private float playerSpeed;
	[SerializeField] private Stat health;
	[SerializeField] private Stat armor;
	[SerializeField] private Stat strength;
	[SerializeField] private Stat actionPoints;
	private Coroutine movePlayerCoroutine;
	
	public GridBlock CurrentBlock { get => currentBlock; set => currentBlock = value; }
	public Transform Player { get => player; set => player = value; }

	public int TravelRange => travelRange;

	
	private void Awake()
	{
		if (_instance == null) _instance = this;
	}
	
	public IEnumerator MovePlayerCoroutine(List<GridBlock> path)
	{
		while (path.Count > 0)
		{
			var step = playerSpeed * Time.deltaTime;

			var playerVec = new Vector3(player.position.x, 1.5f, player.position.z);
			var targetVec = new Vector3(path[0].WorldPosition.x, 1.5f, path[0].WorldPosition.z);

			transform.position = Vector3.MoveTowards(playerVec, targetVec, step);

			if (Vector3.Distance(player.position, targetVec) < 0.001f)
			{
				currentBlock = path[0];
				currentBlock.ShowHideBlockArrow(false);
				path.RemoveAt(0);
			}

			yield return null;
		}
	}
}