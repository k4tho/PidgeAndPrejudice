using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDownChicken : PowerUpDown
{
    public Vector2 ApplySlowth(Vector2 direction)
    {
        direction.x = direction.x * GameParameters.slowSpeedMoveAmount;
        direction.y = direction.y * GameParameters.slowSpeedMoveAmount;
        return direction;
    }
}

