using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDown : MonoBehaviour
{
    public SpriteRenderer PowerSpriteRenderer;
    public MouseShooter MouseShooter;

    private Coroutine changeLocationTimer;

    void Start()
    {
        SpawnPowerSpriteInNewLocation();
    }

    public void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.tag == "ground") || (col.gameObject.tag == "pigeon"))
        {
            SpawnPowerSpriteInNewLocation();
        }
    }

    IEnumerator WaitToChangeLocation()
    {
        yield return new WaitForSeconds(GameParameters.powerChangeLocationTimer);
        SpawnPowerSpriteInNewLocation();
    }

    public virtual void SpawnPowerSpriteInNewLocation()
    {
        PowerSpriteRenderer.transform.position = FindNewLocationForPower();
    }

    protected Vector3 FindNewLocationForPower()
    {
        changeLocationTimer = StartCoroutine(WaitToChangeLocation());

        float xPosition = Random.Range(-23f, 24f);
        float yPosition = Random.Range(-3f, 19f);

        return new Vector3(xPosition, yPosition, 0f);
    }

    protected void Restart()
    {
        if (changeLocationTimer != null)
            changeLocationTimer = null;
    }
}

