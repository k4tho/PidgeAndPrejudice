using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Pigeon Pigeon;
    
    public SpriteRenderer EnemySpriteRenderer;
    public GameObject EnemyProjectilePrefab;

    protected int score;

    protected int health;
    protected float enemyMoveAmount;
    protected float shootRadius;

    protected bool isDead;
    protected bool isShooting;

    protected bool isPrepping = false;
        
    protected bool isMovingToRight;
    protected int numberOfMoves;
    protected int moveAmount;
    private float speed = 5f;

    public virtual void Start()
    {
        isDead = false;
        isShooting = false;
        isMovingToRight = false;
        numberOfMoves = 0;
        moveAmount = 0;
    }

    void Update()
    {
        if (FindPigeon() != null)
        {
            FacePigeon();
            if (GetDistanceFromPigeon() < shootRadius)
            {
                ReadyToShoot();
            }
            else
                ChasePigeon();
        }
        else
        {
            if (numberOfMoves == 0)
            {
                moveAmount = Random.Range(50, 200);
            }
            MoveRandomly(moveAmount);
        }
    }

    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "bullet")
        {
            TakesDamage();
        }

        CheckIfDead();
    }

    protected void ChasePigeon()
    {
        if (GetPigeonXCoordinate() < GetEnemyXCoordinate())
        {
            MoveLeft();
        }
        else
        {
            MoveRight();
        }
    }

    protected void MoveRandomly(int moveAmount)
    {
        numberOfMoves++;


        if (isMovingToRight == true)
        {
            MoveLeft();

            if (numberOfMoves == moveAmount)
            {
                isMovingToRight = false;
                numberOfMoves = 0;
            }
        }
        else
        {
            MoveRight();

            if (numberOfMoves == moveAmount)
            {
                isMovingToRight = true;
                numberOfMoves = 0;
            }
        }
    }

    protected GameObject FindPigeon()
    {
        return GameObject.FindGameObjectWithTag("pigeon");
    }

    protected void ReadyToShoot()
    {
        if (isPrepping == false)
            StartCoroutine(WaitToShoot());
        else
            return;
    }

    IEnumerator WaitToShoot()
    {
        if (isPrepping == true)
        {
            yield return new WaitForSeconds(GameParameters.waitToShootSeconds);
            isPrepping = false;
        }
        else
        {
            isPrepping = true;
            yield return new WaitForSeconds(GameParameters.waitToShootSeconds);
            ShootPigeon();
            isPrepping = false;
        }
    }

    protected void ShootPigeon()
    {
        GameObject projectileObject = Instantiate(EnemyProjectilePrefab, transform.position, Quaternion.identity);
        
        Vector3 projectileDirection3D = new Vector3(GetPigeonXCoordinate(), GetPigeonYCoordinate(), 
            Camera.main.transform.position.z * -1) - transform.position;
        
        projectileObject.GetComponent<Rigidbody>().velocity += speed  * projectileDirection3D;
    }

    protected void FacePigeon()
    {
        if (GetPigeonXCoordinate() - GetEnemyXCoordinate() > 0)
            EnemySpriteRenderer.flipX = true;
        else
            EnemySpriteRenderer.flipX = false;
    }

    protected float GetDistanceFromPigeon()
    {
        float totalDistance;
        float xDistance;
        float yDistance;

        xDistance = GetPigeonXCoordinate() - GetEnemyXCoordinate();
        yDistance = GetPigeonYCoordinate() - GetEnemyYCoordinate();
        totalDistance = Mathf.Sqrt((xDistance * xDistance) + (yDistance * yDistance));

        return totalDistance;
    }

    protected void TakesDamage()
    {
        health = health - GameParameters.normalGunDamageAmount;
    }

    protected void CheckIfDead()
    {
        if (health < 0)
            KillOffEnemy();
    }

    protected void KillOffEnemy()
    {
        //Readouts.UpdateScore();
        Destroy(gameObject);
    }

    protected void MoveRight()
    {
        EnemySpriteRenderer.flipX = true;
        EnemySpriteRenderer.transform.Translate(1 * enemyMoveAmount, 0, 0);
    }

    protected void MoveLeft()
    {
        EnemySpriteRenderer.flipX = false;
        EnemySpriteRenderer.transform.Translate(-1 * enemyMoveAmount, 0, 0);
    }

    protected float GetPigeonXCoordinate()
    {
        return FindPigeon().transform.position.x;
    }

    protected float GetPigeonYCoordinate()
    {
        return FindPigeon().transform.position.y;
    }

    protected float GetEnemyXCoordinate()
    {
        return EnemySpriteRenderer.transform.position.x;
    }

    protected float GetEnemyYCoordinate()
    {
        return EnemySpriteRenderer.transform.position.y;
    }
}
