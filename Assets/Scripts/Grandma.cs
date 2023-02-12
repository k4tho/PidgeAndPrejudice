using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grandma : MonoBehaviour
{
    public Pigeon Pigeon;
    public PowerUpBread PowerUpBread;
    public SpriteRenderer GrandmaSpriteRenderer;
    public Sprite AliveGrandma;
    public Sprite DeadGrandma;

    private GameObject[] grounds;
    private bool invincibleGrandma;

    void Awake()
    {
        grounds = GameObject.FindGameObjectsWithTag("ground");
    }


    void Start()
    {
        SpawnGrandmaInNewLocation();
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "bullet")
        {
            if (invincibleGrandma != true)
            {
                StartCoroutine(WaitToSpawnAfterDeath());
                Pigeon.ShotGrandma();
            }
        }
    }

    IEnumerator WaitToSpawnAfterDeath()
    {
        ChangeToDeadSprite();
        yield return new WaitForSeconds(GameParameters.grandmaRespawnAfterDeathTimer);
        SpawnGrandmaInNewLocation();
    }

    IEnumerator WaitToChangeLocation()
    {
        yield return new WaitForSeconds(GameParameters.powerChangeLocationTimer);
        SpawnGrandmaInNewLocation();
    }

    public void SpawnGrandmaInNewLocation()
    {
        ChangeToAliveSprite();
        GrandmaSpriteRenderer.transform.position = FindNewSpawnLocation();
        PowerUpBread.FollowGrandma(GrandmaSpriteRenderer.transform.position);
        StartCoroutine(WaitToChangeLocation());
    }

    public void MakeGrandmaInvincible()
    {
        invincibleGrandma = true;
    }

    public void MakeGrandmaDestructible()
    {
        invincibleGrandma = false;
    }

    private Vector3 FindNewSpawnLocation()
    {
        int randomSpawnLocation = Random.Range(0, grounds.Length);

        return GetSpawnLocation(grounds[randomSpawnLocation]);
    }

    private Vector3 GetSpawnLocation(GameObject groundLocation)
    {
        return new Vector3(groundLocation.transform.position.x, groundLocation.transform.position.y + 1f, 0f);
    }

    private void ChangeToDeadSprite()
    {
        GrandmaSpriteRenderer.sprite = DeadGrandma;
    }

    private void ChangeToAliveSprite()
    {
        GrandmaSpriteRenderer.sprite = AliveGrandma;
    }
}
