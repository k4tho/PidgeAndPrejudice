using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpLmg : PowerUpDown
{
    public float ApplyFastGunSpeed()
    {
        return GameParameters.fastBulletSpeed;
    }

    public float ApplyFastGunSpray()
    {
        return GameParameters.fastGunSprayTimer;
    }

}
