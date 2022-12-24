using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = System.Random;


public class Pigeon : MonoBehaviour
{
    public SpriteRenderer PigeonSpriteRenderer;
    public MouseShooter MouseShooter;
    public Game Game;

    public Sprite WalkingSprite;
    public Sprite FlyingSprite;
    public Sprite WalkingPowerupSprite;
    public Sprite FlyingPowerupSprite;

    public PowerUpDown PowerUpDown;
    public PowerUpBread PowerUpBread;
    public PowerDownChicken PowerDownChicken;
    public PowerUpLmg PowerUpLmg;
    public PowerDownFakeLmg PowerDownFakeLmg;

    private int currentHP = GameParameters.pigeonMaximumHP;
    private bool isPlaying;
    private bool isOnGround;
    private float pigeonCurrentStamina = GameParameters.pigeonMaximumStamina;
    private Rigidbody pigeonRigidBody;

    private bool isUsingPower;
    private bool speedPowerUp;
    private bool slowthPowerDown;
    private bool hasGunUpgrade;
    private bool hasGunDowngrade;

    void Awake()
    {
        pigeonRigidBody = gameObject.GetComponent<Rigidbody>();
        pigeonRigidBody.isKinematic = false;
        pigeonRigidBody.useGravity = true;
    }

    void Start()
    {
        //PigeonHUD.EnablePigeonHUD();
        StartGame();
    }
    void Update()
    {
        
        if (isOnGround)
        {
            RegenerateStamina();
        }
        else
        {
            DegenerateStamina();
            if (pigeonCurrentStamina > 0f)
            {
                if (pigeonRigidBody.useGravity)
                {
                    pigeonRigidBody.useGravity = false;
                }

            }
            else
            {
                if (!pigeonRigidBody.useGravity)
                {
                    pigeonRigidBody.useGravity = true;
                }
            }
        }
         
        //PigeonHUD.UpdateHUD();
        UseCorrectSprite();
        
    }

    public void StartGame()
    {
        isPlaying = true;
        pigeonCurrentStamina = GameParameters.pigeonMaximumStamina;
        Reset();
    }


    public void Reset()
    {
        isPlaying = false;
        currentHP = GameParameters.pigeonMaximumHP;
        ResetPosition();
        ReturnToNormalcy();
    }
    
    private void ResetPosition()
    {
        PigeonSpriteRenderer.transform.position = new Vector3(0f, 0f, 0f);
        PigeonSpriteRenderer.flipX = false;
    }

    private bool HasGameJustEnded()
    {
        if (!Game.IsRunning() && isPlaying)
        {
            return true;
        }

        return false;
    }
    
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag=="ground")
        {
            if (!isOnGround)
            {
                isOnGround = true;
            }

            if (!pigeonRigidBody.useGravity)
            {
                pigeonRigidBody.useGravity = true;
            }
            
        }



        if (col.gameObject.tag == "bread")
        {
            ReturnToNormalcy();
            speedPowerUp = true;
            StartCoroutine(StartPowerUpDownTimer(GameParameters.fastSpeedTimer));
        }
        else if (col.gameObject.tag == "chicken")
        {
            ReturnToNormalcy();
            slowthPowerDown = true;
            StartCoroutine(StartPowerUpDownTimer(GameParameters.slowSpeedTimer));
        }
        else if (col.gameObject.tag == "LMG")
        {
            ReturnToNormalcy();
            MouseShooter.isGunFast = true;
            hasGunUpgrade = true;
            StartCoroutine(StartPowerUpDownTimer(GameParameters.gunUpgradeTimer));

            //needs to change gun to lmg
        }
        else if (col.gameObject.tag == "FakeLMG")
        {
            ReturnToNormalcy();
            MouseShooter.isGunSlow = true;
            hasGunDowngrade = true;
            StartCoroutine(StartPowerUpDownTimer(GameParameters.gunDowngradeTimer));

            //needs to change gun to pistol
        }
        
        else if (col.gameObject.tag == "enemybullet")
        {
            if (currentHP > 0)
                currentHP--;
            else
                Game.EndGame();


            //needs to change gun to pistol
        }
    }
    
    public void Move(Vector2 direction)
    {
        Vector2 newDirection = direction;

        if (CheckForMovementPower() == true)
            newDirection = ApplyMovementChanges(direction);


        FaceCorrectDirection(newDirection);
        if (isOnGround)
        {
            if (pigeonCurrentStamina>0f)
            {
                if (newDirection.y > 0f)
                {
                    
                    Fly(newDirection);
                    isOnGround = false;
                    pigeonRigidBody.useGravity = false;

                }
                else
                {
                    Walk(newDirection);
                }
            }
            else
            {
                
                Walk(newDirection);
                
            }
            
        }
        else
        {
            if (pigeonCurrentStamina > 0f)
            {
                isOnGround = false;
                pigeonRigidBody.useGravity = false;
                Fly(newDirection);
                
            }
            else
            {
                Walk(newDirection);
            }
        }
       
    }

    
    public void Fly(Vector2 direction)
    {
        PigeonSpriteRenderer.transform.Translate(
            new Vector3(direction.x * GameParameters.pigeonMoveAmount, 
                direction.y*GameParameters.pigeonMoveAmount, 
                0f));
    }

    public void Walk(Vector2 direction)
    {
        PigeonSpriteRenderer.transform.Translate(
            new Vector3(direction.x * GameParameters.pigeonMoveAmount, 
                0f, 
                0f));
    }
    public bool IsStaminaEmpty()
    {
        if (pigeonCurrentStamina <= 0)
            return true;
        return false;

    }

    private void RegenerateStamina()
    {
        if (pigeonCurrentStamina < GameParameters.pigeonMaximumStamina)
        {
            pigeonCurrentStamina += GameParameters.pigeonStaminaRegenerationRate;
        }
        else if(pigeonCurrentStamina >= GameParameters.pigeonMaximumStamina)
        {
            pigeonCurrentStamina = GameParameters.pigeonMaximumStamina;
        }
        
    }

    private void DegenerateStamina()
    {
        if (pigeonCurrentStamina > 0f)
        {
            pigeonCurrentStamina -= GameParameters.pigeonStaminaDegenerationRate;
        }
        else if(pigeonCurrentStamina<=0f)
        {
            pigeonCurrentStamina = 0f;
        }
    }
    IEnumerator ChangeStamina(float staminaUpOrDown)
    {
        yield return new WaitForSeconds(5);
        pigeonCurrentStamina = pigeonCurrentStamina + staminaUpOrDown;
    }
    
    private void KeepOnScreen()
    {
        //PigeonSpriteRenderer.transform.position =
            //SpriteTools.ConstrainToScreen(PigeonSpriteRenderer);
    }
    
    public void FaceCorrectDirection(Vector2 direction)
    {
        //if moving to the right
        if (direction.x > 0)
        {
            //face right
            PigeonSpriteRenderer.flipX = true;
        }
        if(direction.x<0)
        { 
            //if moving to the left
            //face left
            PigeonSpriteRenderer.flipX = false;
        }
    }

    public float getStamina()
    {
        return pigeonCurrentStamina;
    }

    private void ChangeToFlyingSprite()
    {
        PigeonSpriteRenderer.sprite = FlyingSprite;
    }

    private void ChangeToWalkingSprite()
    {
        PigeonSpriteRenderer.sprite = WalkingSprite;
    }

    private void ChangeToFlyingPowerupSprite()
    {
        PigeonSpriteRenderer.sprite = FlyingPowerupSprite;
    }

    private void ChangeToWalkingPowerupSprite()
    {
        PigeonSpriteRenderer.sprite = WalkingPowerupSprite;
    }

    private void UseCorrectSprite()
    {
        if (isOnGround == true && isUsingPower == false)
        {
            ChangeToWalkingSprite();
        }

        else if (isOnGround == false && isUsingPower == false)
        {
            ChangeToFlyingSprite();
        }

        else if (isOnGround == true && isUsingPower == true)
        {
            ChangeToWalkingPowerupSprite();
        }

        else if (isOnGround == false && isUsingPower == true)
        {
            ChangeToFlyingPowerupSprite();
        }
                
    }


    /**
     * power up/down code
    */
    //public void ShotGrandma()
    //{
        //ReturnToNormalcy();
        //isUsingPower = true;
        //PowerUpDown.ApplyRandomFiring();
        //StartCoroutine(StartPowerUpDownTimer(GameParameters.randomFiringTimer));
    //}

    IEnumerator StartPowerUpDownTimer(float timer)
    {
        isUsingPower = true;
        yield return new WaitForSeconds(timer);
        ReturnToNormalcy();
    }

    private bool CheckForMovementPower()
    {
        if (speedPowerUp == true)
            return true;
        else if (slowthPowerDown == true)
            return true;
        else
            return false;
    }

    private Vector2 ApplyMovementChanges(Vector2 direction)
    {
        Vector2 newDirection = direction;

        if (speedPowerUp == true)
            newDirection = PowerUpBread.ApplySpeed(direction);
        else if (slowthPowerDown == true)
            newDirection = PowerDownChicken.ApplySlowth(direction);

        return newDirection;
    }

    private void ReturnToNormalcy()
    {
        isUsingPower = false;
        speedPowerUp = false;
        slowthPowerDown = false;
        MouseShooter.isGunSlow = false;
        MouseShooter.isGunFast = false;
        hasGunUpgrade = false;
        hasGunDowngrade = false;
    }



    /**
     * respawn pigeon code
    */
    private void RespawnPigeon()
    {
        MakeInvincible();
        StartCoroutine(WaitForInvisibilityPeriod());
        ResetPosition();
    }

    private void MakeInvincible()
    {
        gameObject.tag = "Untagged";
    }

    IEnumerator WaitForInvisibilityPeriod()
    {
        yield return new WaitForSeconds(GameParameters.pigeonInvincibilityTimer);
        MakeVulnerable();
    }

    private void MakeVulnerable()
    {
        gameObject.tag = "pigeon";
    }
}