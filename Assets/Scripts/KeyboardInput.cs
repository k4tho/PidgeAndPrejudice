using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInput : MonoBehaviour
{
    public Pigeon Pigeon;
    public Game Game;
    
    void Update()
    {
        if (Game.IsRunning())
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey((KeyCode.UpArrow)))
            {
                Pigeon.Move(new Vector2(0f, GameParameters.pigeonMoveAmount));
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey((KeyCode.LeftArrow)))
            {
                Pigeon.Move(new Vector2(-GameParameters.pigeonMoveAmount, 0f));
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey((KeyCode.RightArrow)))
            {
                Pigeon.Move(new Vector2(GameParameters.pigeonMoveAmount, 0f));
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey((KeyCode.DownArrow)))
            {
                Pigeon.Move(new Vector2(0f, -GameParameters.pigeonMoveAmount));
            }
            
        }
    }
}
