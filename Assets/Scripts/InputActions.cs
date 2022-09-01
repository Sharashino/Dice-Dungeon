using UnityEngine;
using System;

public class InputActions : MonoBehaviour
{
	public static Action OnLeftClick;
	public static Action OnRightClick;
	public static Action<GridBlock> OnRightBlockClick;
	public static Action OnExitModes;

	private void Awake()
	{
		OnExitModes += () => GridManager.Instance.EndPathfinding();
	}

	private void Update()
	{
		// Left Click
		if (Input.GetMouseButtonDown(0)) OnLeftClick?.Invoke();

		// Right Click
		// Click with grid block executed in Mouse Cursor
		if (Input.GetMouseButtonDown(1)) OnRightClick?.Invoke();

		if (Input.GetKeyDown(KeyCode.Escape)) OnExitModes?.Invoke();
	}
}