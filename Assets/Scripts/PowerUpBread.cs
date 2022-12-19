using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBread : PowerUpDown
{
    public Vector2 ApplySpeed(Vector2 direction)
    {
        direction.x = direction.x * GameParameters.fastSpeedMoveAmount;
        direction.y = direction.y * GameParameters.fastSpeedMoveAmount;
        return direction;
    }

}
