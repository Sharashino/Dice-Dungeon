using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
	public static GameUIController Instance;

	[SerializeField] private List<Menu> gameMenus = new();
	[SerializeField] private GraphicRaycaster menusRaycaster;

	[Header("Controllers")]    
	[SerializeField] private InventoryController inventoryController;
	[SerializeField] private ActionsController actionsController;
	[SerializeField] private StatsController statsController;

	[SerializeField] private Button inventoryButton;
	[SerializeField] private Button statsButton;
	
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
		
		inventoryButton.onClick.AddListener(ShowHideInventoryMenu);
		statsButton.onClick.AddListener(ShowHideStatsMenu);
	}

	private void ShowHideInventoryMenu()
	{
		CascadeHideMenus();
		inventoryController.ShowHideMenu(!inventoryController.IsShown);
	}

	private void ShowHideStatsMenu()
	{
		CascadeHideMenus();
		statsController.ShowHideMenu(!statsController.IsShown);
	}

	private void ShowActionsMenu(GridBlock block)
	{
		if (block == null) actionsController.ShowHideMenu(false, null);
		else actionsController.ShowHideMenu(true, block);
	}

	private void CascadeHideMenus()
	{
		var firstShown = gameMenus.FirstOrDefault(x => x.IsShown);
		if (firstShown != null) firstShown.ShowHideMenu(false);
	}
}