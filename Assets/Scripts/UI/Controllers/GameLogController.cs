using System;
using System.Collections.Generic;
using UnityEngine;

public class GameLogController : MonoBehaviour
{
    public static GameLogController Instance;
    [SerializeField] private GameLog gameLogPrefab;
    [SerializeField] private List<GameLog> logList = new();

    private void Awake()
    {
        if(Instance == null) Instance = this;
    }

    public void SpawnLogMessage(string message, Color color)
    {
        if (logList.Count > 4)
        {
            Destroy(logList[0].gameObject);
            logList.RemoveAt(0);
        }
        
        var gameLog = Instantiate(gameLogPrefab, transform);
        gameLog.LogMessage(message, color);
        logList.Add(gameLog);
    }
}

public static class ChatLog
{
    public static void LogPickup(Item item)
    {
        var message = $"Picked up {item.name}.";
        GameLogController.Instance.SpawnLogMessage(message, Color.white);
    }

    public static void LogHit(int amount, Enemy enemy)
    {
        var message = $"You hit {enemy.EnemyName} for {amount} damage.";
        GameLogController.Instance.SpawnLogMessage(message, Color.white);
    }
    
    public static void LogHeal(int amount)
    {
        var message = $"You healed {amount} health.";
        GameLogController.Instance.SpawnLogMessage(message, Color.green);
    }
    
    public static void LogDamage(int amount)
    {
        var message = $"You took {amount} damage.";
        GameLogController.Instance.SpawnLogMessage(message, Color.white);
    }
    
    public static void LogDeath()
    {
        var message = "You died.";
        GameLogController.Instance.SpawnLogMessage(message, Color.red);
    }

    public static void LogGameStart()
    {
        var message = "Your adventure begins.";
        var color = new Color(225, 123, 38,255);
        GameLogController.Instance.SpawnLogMessage(message, Color.yellow);
    }
}       