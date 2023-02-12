using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDownFakeLmg : PowerUpDown
{
    public float ApplySlowGunSpeed()
    {
        return GameParameters.slowBulletSpeed;
    }

    public float ApplySlowGunSpray()
    {
        return GameParameters.slowGunSprayTimer;
    }
}
