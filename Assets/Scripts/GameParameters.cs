using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameParameters
{
    //pigeon stuff
    public static float pigeonMoveAmount = 0.25f;
    public static float pigeonMaximumStamina = 20f;
    public static float pigeonStaminaRegenerationRate = .05f;
    public static float pigeonStaminaDegenerationRate = .05f;
    public static int pigeonMaximumHP = 3;
    public static float pigeonInvincibilityTimer = 10f;

    //power ups & down
    public static float fastSpeedMoveAmount = 3f;
    public static float slowSpeedMoveAmount = .25f;
    public static float slowBulletSpeed = .5f;
    public static float fastBulletSpeed = 10f;

    //power ups & down timers
    public static float powerChangeLocationTimer = 30f;
    public static float grandmaRespawnAfterDeathTimer = 1f;
    public static float fastSpeedTimer = 10f;
    public static float slowSpeedTimer = 10f;
    public static float gunUpgradeTimer = 10f;
    public static float gunDowngradeTimer = 10f;
    public static float randomFiringTimer = 5f;
    public static float locationTimer = 30f;

    //enemy stuff
    public static float enemyShootRadius = 4f;
    public static float enemyPedestrianShootRadius = 4f;
    public static float enemyAnimalPatrolShootRadius = 5f;
    public static float enemyMilitaryShootRadius = 6f;
    public static float waitToShootSeconds = 2f;
    public static float enemyMoveAmount = .05f;
    public static float enemyPedestrianMoveAmount = .025f;
    public static float enemyAnimalPatrolMoveAmount = .05f;
    public static float enemyMilitaryMoveAmount = .075f;

    //health
    public static int pedestrianHealth = 50;
    public static int animalControlHealth = 100;
    public static int militaryHealth = 150;
    public static int normalGunDamageAmount = 50;

    //waves of enemy
    public static int startNumPedestrian = 3;
    public static int startNumAnimalPatrol = 2;
    public static int startNumMilitary = 0;
    public static int addMoreEnemiesEveryXRounds = 3;
    public static int numToIncreaseAmountOfEnemyBy = 2;
    public static int maxWaitTimeBetweenWaves = 40;
    public static int minWaitTimeBetweenWaves = 0;
    public static float waitTimeBetweenEachEnemySpawn = .5f;

    //shooting stuff
    public static float bulletAliveTimer = 2f;
    public static float bulletInvinvibilityTimer = .05f;
    public static float bulletAvgSpeed = 3f;

    public static float normalGunSprayTimer = .5f;
    public static float fastGunSprayTimer = .1f;
    public static float slowGunSprayTimer = 1.25f;
    public static float beserkGunSprayTimer = .5f;

    public static int pointsForPedestrian = 10;
    public static int pointsForAnimalControl = 30;
    public static int pointsForMilitary = 50;
}