using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
	public static GameUIController Instance;

	[SerializeField] private CanvasGroup mainUI;
	[SerializeField] private List<Menu> gameMenus = new List<Menu>();

	[SerializeField] private GraphicRaycaster menusRaycaster;

	public GraphicRaycaster MenusRaycaster => menusRaycaster;
	public List<Menu> GameMenus => gameMenus;

	private void Awake()
	{
		if (Instance == null) Instance = this;

		MapActions();
	}

	private void MapActions()
	{
		InputActions.OnExitModes += CascadeHideMenus;
		InputActions.OnRightBlockClick += ShowActionsMenu;
	}

	private void ShowActionsMenu(GridBlock block)
	{
		if (GridManager.Instance.IsPathfinding) return;

		if (block == null) gameMenus[0].ShowHideMenu(false, null);
		else gameMenus[0].ShowHideMenu(true, block);
	}

	private void CascadeHideMenus()
	{
		var firstShown = gameMenus.FirstOrDefault(x => x.IsShown);
		if (firstShown != null) firstShown.ShowHideMenu(false, null);
	}
}