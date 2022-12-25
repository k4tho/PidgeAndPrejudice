using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBread : PowerUpDown
{
    public Grandma Grandma;

    public Vector2 ApplySpeed(Vector2 direction)
    {
        direction.x = direction.x * GameParameters.fastSpeedMoveAmount;
        direction.y = direction.y * GameParameters.fastSpeedMoveAmount;
        return direction;
    }

    public override void SpawnPowerSpriteInNewLocation()
    {
        Grandma.SpawnGrandmaInNewLocation();
    }

    public void FollowGrandma(Vector3 grandmaLocation)
    {
        PowerSpriteRenderer.transform.position = new Vector3(grandmaLocation.x - 1f, grandmaLocation.y - .5f, grandmaLocation.z);
    }
}
