using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Room))]
public class RoomInspector : Editor
{
	private Room room;
	private int ySize;
	private int xSize;
	
	private int roomX => room.size.x;
	private int roomY => room.size.y;

	private GridBlock[] gridBlocks;
	private bool[,] roomCells;


	private void OnEnable()
	{
		room = target as Room;
		
		if (xSize == 0 || ySize == 0)
		{
			roomCells = new bool[roomX, roomY];
		}
	}

	public override void OnInspectorGUI()
	{
		room = target as Room;

		DrawInspector();

		EditorUtility.SetDirty(room);
	}

	private void DrawInspector()
	{
		xSize = EditorGUILayout.IntField("Size X", xSize);
		ySize = EditorGUILayout.IntField("Size Y", ySize);

		if (room == null)
		{
			GUILayout.BeginVertical("Help Box");
			{
			}
			GUILayout.EndVertical();
			
		}
		
		DrawInspectorButtons();
	}

	private void DrawRoomGrid()
	{
		roomCells = new bool[xSize, ySize];
			
		for (int y = 0; y < ySize; y++)
		{
			for (int x = 0; x < xSize; x++)
			{
				GUILayout.BeginVertical("HelpBox");

				
				roomCells[x, y] = EditorGUILayout.Toggle("", roomCells[x, y]);
				
				GUILayout.EndVertical();
			}
		}
	}
	
	private void DrawInspectorButtons()
	{
		GUILayout.BeginHorizontal();
		{
			if (GUILayout.Button("Spawn Room Grid", EditorStyles.centeredGreyMiniLabel))
			{
				SpawnRoomGrid();
			}
		}
		
		GUILayout.EndHorizontal();
	}

	private void SpawnRoomGrid()
	{
		GUILayout.BeginVertical("HelpBox");
		{
			roomCells = new bool[EditorGUILayout.IntField("Y Size", ySize) ,EditorGUILayout.IntField("X Size", ySize)]; 

			for (int y = 0; y < ySize; y++)
			{
				for (int x = 0; x < xSize; x++)
				{
					GUILayout.BeginHorizontal();
					{
						GUILayout.BeginVertical("HelpBox");
						{
							Debug.Log("Block " + x + ", " + y);
							roomCells[x,y] = false;
	
							DrawRoomGrid();
						}
						GUILayout.EndVertical();
					}
					GUILayout.EndHorizontal();
				}
			}									
		}
	}
}