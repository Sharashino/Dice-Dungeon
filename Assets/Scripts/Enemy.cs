using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private string enemyName;
    [SerializeField] private float enemySpeed;
    [SerializeField] private Stat health;
    [SerializeField] private Stat mana;
    [SerializeField] private Stat armor;
    [SerializeField] private Stat strength;
    [SerializeField] private Stat intelligence;
    [SerializeField] private Stat agility;
    [SerializeField] private Stat luck;


    public string EnemyName => enemyName;
}
