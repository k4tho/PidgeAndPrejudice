using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Random = System.Random;


public class Pigeon : MonoBehaviour
{
    public SpriteRenderer PigeonSpriteRenderer;
    public Game Game;
        
    private bool isPlaying = true;

    private float pigeonCurrentStamina = GameParameters.pigeonMaximumStamina;
    
    void Update()
    {
        if (pigeonCurrentStamina < GameParameters.pigeonMaximumStamina
            && PigeonSpriteRenderer.transform.position.y <= 0)
            //pigeonCurrentStamina++;
            StartCoroutine(RechargeOrReduceStamina("Recharge"));
        print(pigeonCurrentStamina);
        if (IsStaminaEmpty())
            CheckIfPigeonIsAirborne();
        //if (HasGameJustEnded())
            //Reset();
    }

    public void StartGame()
    {
        isPlaying = true;
        pigeonCurrentStamina = GameParameters.pigeonMaximumStamina;
    }

    private void Reset()
    {
        isPlaying = false;
        ResetPosition();
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
    
    public void OnCollisionEnter2D(Collision2D col)
    {
        
    }
    
    public void Move(Vector2 direction)
    {
        if (IsFlying(direction))
        {
            PigeonSpriteRenderer.GetComponent<Rigidbody>().useGravity = false;
            //pigeonCurrentStamina--;
            StartCoroutine(RechargeOrReduceStamina("Reduce"));
            print(pigeonCurrentStamina);
        }
        else
            direction.y = 0;
        
            FaceCorrectDirection(direction);
        PigeonSpriteRenderer.transform.Translate(new 
            Vector3(direction.x * GameParameters.pigeonMoveAmount, 
                direction.y * GameParameters.pigeonMoveAmount, 
                0f));
        KeepOnScreen();
    }

    private bool IsFlying(Vector2 direction)
    {
        if (IsStaminaEmpty() || direction.y <= 0)
            return false;
        else
            return true;
    }
    
    public bool IsStaminaEmpty()
    {
        if (pigeonCurrentStamina <= 0)
            return true;
        return false;
    }
    
    IEnumerator RechargeOrReduceStamina(string RechargeOrReduce)
    {
        if (RechargeOrReduce == "Recharge")
        {
            yield return new WaitForSeconds(5);
            if (pigeonCurrentStamina < GameParameters.pigeonMaximumStamina)
                pigeonCurrentStamina++;
        }
        else if (RechargeOrReduce == "Reduce")
        {
            yield return new WaitForSeconds(1);
            if (pigeonCurrentStamina > 0)
                pigeonCurrentStamina--;
        }
    }

    public void CheckIfPigeonIsAirborne()
    {
        if (PigeonSpriteRenderer.transform.position.y > 0)
            StartCoroutine(PigeonFall());
    }
    
    IEnumerator PigeonFall()
    {
        yield return new WaitForSeconds(5);
        //PigeonSpriteRenderer.transform.Translate(new Vector3(0f, 
            //-1f * GameParameters.pigeonMoveAmount, 0f));
        PigeonSpriteRenderer.GetComponent<Rigidbody>().useGravity = true;
        KeepOnScreen();
    }
    
    private void KeepOnScreen()
    {
        PigeonSpriteRenderer.transform.position =
            SpriteTools.ConstrainToScreen(PigeonSpriteRenderer);
    }
    
    private void FaceCorrectDirection(Vector2 direction)
    {
        //if moving to the right
        if (direction.x > 0)
        {
            //face right
            PigeonSpriteRenderer.flipX = false;
        }
        else
        { 
            //if moving to the left
            //face left
            PigeonSpriteRenderer.flipX = true;
        }
    }
}