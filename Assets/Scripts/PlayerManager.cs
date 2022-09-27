using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    
    [SerializeField] private Player player;
    
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private ManaBar manaBar;
    public Player Player => player;

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            
        }
    }

    public void SpawnPlayer(GridBlock gridBlock)
    {
        player = Instantiate(GameManager.Instance.PlayerPrefab, GridManager.Instance.EntitiesParent);
        player.transform.position = new Vector3(gridBlock.WorldPosition.x, 1.5f, gridBlock.WorldPosition.z);
        player.CurrentBlock = gridBlock;
    }
}
