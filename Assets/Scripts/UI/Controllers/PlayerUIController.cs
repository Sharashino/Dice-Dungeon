using UnityEngine.UI;
using UnityEngine;

public class PlayerUIController : Menu
{
    [SerializeField] private Button[] quickSlotButtons;
    [SerializeField] private Button inventoryButton;
    [SerializeField] private Button statsButton;
    
    [Header("Controllers")]    
    [SerializeField] private InventoryController inventoryController;
    [SerializeField] private StatsController statsController;

    protected override void Awake()
    {
        base.Awake();
        
    }
}


