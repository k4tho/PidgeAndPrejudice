using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(CountdownToSelfDestruct());
    }

    IEnumerator CountdownToSelfDestruct()
    {
        yield return new WaitForSeconds(GameParameters.bulletAliveTimer);
        Destroy(gameObject);
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "ground")
        {
            Destroy(gameObject);
        }
    }
}
