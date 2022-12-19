using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLmg : PowerUpDown
{
    public float ApplyFastGunSpeed(float speed)
    {
        return speed * GameParameters.fastGunSpeed;
    }

}
