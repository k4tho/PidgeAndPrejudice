using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPedestrian : Enemy
{
    public override void Start()
    {
        health = GameParameters.pedestrianHealth;
        enemyMoveAmount = GameParameters.enemyPedestrianMoveAmount;
        shootRadius = GameParameters.enemyPedestrianShootRadius;
        pointsForEnemyDeath = GameParameters.pointsForPedestrian;
        base.Start();
    }

    //different weaponry
}