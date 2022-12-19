using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMilitary : Enemy
{
    public override void Start()
    {
        health = GameParameters.militaryHealth;
        enemyMoveAmount = GameParameters.enemyMilitaryMoveAmount;
        shootRadius = GameParameters.enemyMilitaryShootRadius;
        base.Start();
    }

    //different weaponry
}
