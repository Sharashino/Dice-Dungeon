using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryController : Menu
{
	protected override void Awake()
	{
		base.Awake();
		ShowHideMenu(false);
	}
}
