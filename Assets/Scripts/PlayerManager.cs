using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
	public static PlayerManager Instance;

	[SerializeField] private Transform player;
	[SerializeField] private GridBlock currentBlock;

	[Header("Player Stats")] [SerializeField]
	private int playerRange;

	[SerializeField] private float playerSpeed;
	[SerializeField] private Stat health;
	[SerializeField] private Stat armor;
	[SerializeField] private Stat strength;
	[SerializeField] private Stat actionPoints;

	public GridBlock CurrentBlock { get => currentBlock; set => currentBlock = value; }
	public Transform Player { get => player; set => player = value; }

	public int PlayerRange => playerRange;

	private void Awake()
	{
		if (Instance == null) Instance = this;
	}

	public IEnumerator MovePlayerCoroutine(List<GridBlock> path)
	{
		while (path.Count > 0)
		{
			var step = playerSpeed * Time.deltaTime;

			var playerVec = new Vector3(player.position.x, 1.5f, player.position.z);
			var targetVec = new Vector3(path[0].transform.position.x, 1.5f, path[0].transform.position.z);

			player.position = Vector3.MoveTowards(playerVec, targetVec, step);

			if (Vector3.Distance(player.position, targetVec) < 0.001f)
			{
				currentBlock = path[0];
				currentBlock.ShowHideBlockArrow(false);
				path.RemoveAt(0);
			}

			if (path.Count == 0) Debug.Log("Done walking");
			yield return null;
		}
	}
}