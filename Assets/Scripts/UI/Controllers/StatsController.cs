using UnityEngine;
using TMPro;

public class StatsController : Menu
{
    [SerializeField] private TMP_Text healthAmount;
    [SerializeField] private TMP_Text manaAmount;
    [SerializeField] private TMP_Text armorAmount;
    [SerializeField] private TMP_Text strengthAmount;
    [SerializeField] private TMP_Text intelligenceAmount;
    [SerializeField] private TMP_Text agilityAmount;
    [SerializeField] private TMP_Text luckAmount;
    private Player player;
    
    protected override void Awake()
    {
        base.Awake();
        ShowHideMenu(false);
    }

    public override void ShowHideMenu(bool state)
    {
        base.ShowHideMenu(state);
        
        if(player == null) player = Player.Instance;
        
        if (state)
        {
            healthAmount.text = $"{player.Health.BaseValue}";
            manaAmount.text = $"{player.Mana.BaseValue}";
            armorAmount.text = $"{player.Armor.BaseValue}";
            strengthAmount.text = $"{player.Strength.BaseValue}";
            intelligenceAmount.text = $"{player.Intelligence.BaseValue}";
            agilityAmount.text = $"{player.Agility.BaseValue}";
            luckAmount.text = $"{player.Luck.BaseValue}";
        }
    }
}
