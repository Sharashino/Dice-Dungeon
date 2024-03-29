﻿using UnityEngine;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance;

	[SerializeField] private Player playerPrefab;
	[SerializeField] private GameStates gameState;

	public GameStates GameState { get => gameState; set => gameState = value; }

	public Player PlayerPrefab => playerPrefab;
	
	private void Awake()
	{
		if (Instance == null) Instance = this;
	}
}

public enum GameStates
{
	PlayerTurn,
	GameWait,
	Pause
}