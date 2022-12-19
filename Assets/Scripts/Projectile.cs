using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float SecondsAlive = 2f;

    void Start()
    {
        StartCoroutine(CountdownToSelfDestruct());
    }

    IEnumerator CountdownToSelfDestruct()
    {
        yield return new WaitForSeconds(SecondsAlive);
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
