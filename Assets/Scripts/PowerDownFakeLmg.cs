using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDownFakeLmg : PowerUpDown
{
    public float ApplySlowGunSpeed(float speed)
    {
        return speed * GameParameters.slowGunSpeed;
    }
}
