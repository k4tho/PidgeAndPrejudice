using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool invincibility;

    void Start()
    {
        StartCoroutine(CountdownToSelfDestruct());
        StartCoroutine(WaitForInvincibilityPeriod());
    }

    IEnumerator CountdownToSelfDestruct()
    {
        yield return new WaitForSeconds(GameParameters.bulletAliveTimer);
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision col)
    {
        if (invincibility == false)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator WaitForInvincibilityPeriod() 
    {
        MakeInvincible();
        yield return new WaitForSeconds(GameParameters.bulletInvinvibilityTimer);
        MakeDestructible();
    }

    private void MakeInvincible()
    {
        invincibility = true;
    }

    private void MakeDestructible()
    {
        invincibility = false;
    }
}
