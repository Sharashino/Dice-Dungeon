using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public static class DiceDungeonUtils
{
	public static IEnumerable<T> GetEnumValues<T>()
	{
		return Enum.GetValues(typeof(T)).Cast<T>();
	}

	public static bool IsMouseOverUI()
	{
		var results = new List<RaycastResult>();
		var pointerData = new PointerEventData(EventSystem.current)
		{
			position = Input.mousePosition
		};

		GameUIController.Instance.MenusRaycaster.Raycast(pointerData, results);

		return results.Count > 0;
	}

	public static void Disable(this CanvasGroup cg)
	{
		cg.blocksRaycasts = false;
		cg.interactable = false;
		cg.alpha = 0;
	}

	public static void Enable(this CanvasGroup cg)
	{
		cg.blocksRaycasts = true;
		cg.interactable = true;
		cg.alpha = 1;
	}

	public static bool IsShown(this CanvasGroup cg)
	{
		return cg.alpha > 0;
	}
}