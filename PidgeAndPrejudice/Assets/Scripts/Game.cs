using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour
{
    void Start()
    {
        StartGame();
    }
    
    private bool isRunning = true;

    public void StartGame()
    {
        isRunning = true;
    }
    
    public void EndGame()
    {
        isRunning = false;
    }

    public bool IsRunning()
    {
        return isRunning;
    }
}
