using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Pigeon Pigeon;
    public EnemySpawner EnemySpawner;
    public Grandma Grandma;
    public PowerUpDown PowerUpDown;
    public StaminaBar StaminaBar;
    public CanvasGroup StartScreen;
    public CanvasGroup EndScreen;
    public Text ScoreDisplay;
    public  Text WaveDisplay;
    public Text EndScoreText;
    public CanvasGroup Stats;

    private bool isRunning;
    void Awake()
    {
        isRunning = false;
        UI.InitializeUI(this);
        CanvasGroupDisplayer.Show(StartScreen);
        CanvasGroupDisplayer.Hide(EndScreen);
    }

    void Update()
    {
        
    }

    public void StartGame()
    {
        isRunning = true;
        Pigeon.Reset();
        Readouts.Reset();
        EnemySpawner.ResetGame();
        EnemySpawner.SpawnNextWave();
        CanvasGroupDisplayer.Show(Stats);
    }
    
    public void EndGame()
    {
        isRunning = false;
        EnemySpawner.ResetGame();
        CanvasGroupDisplayer.Show(EndScreen);
        Readouts.UpdateEndScreen();
        CanvasGroupDisplayer.Hide(Stats);
    }

    public bool IsRunning()
    {
        return isRunning;
    }

    public void OnClickEnter()
    {
        StartGame();
        CanvasGroupDisplayer.Hide(StartScreen);
        CanvasGroupDisplayer.Hide(EndScreen);
    }
}
