using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    private PlayerInput player;
    private bool eliteEnemy;

    public PlayerInput Player { get => player; set => player = value; }
    public bool EliteEnemy { get => eliteEnemy; set => eliteEnemy = value; }

    public abstract void OrderAttack(PlayerInput target);
}
