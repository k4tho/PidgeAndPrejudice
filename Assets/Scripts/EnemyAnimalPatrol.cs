using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimalPatrol : Enemy
{
    public override void Start()
    {
        health = GameParameters.animalControlHealth;
        enemyMoveAmount = GameParameters.enemyAnimalPatrolMoveAmount;
        shootRadius = GameParameters.enemyAnimalPatrolShootRadius;
        base.Start();
    }

    //different weaponry
}
