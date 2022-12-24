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
    void Awake()
    {
        UI.InitializeUI(this);
        CanvasGroupDisplayer.Show(StartScreen);
        CanvasGroupDisplayer.Hide(EndScreen);
    }
    void Start()
    {
        
        
        
    }

    void Update()
    {
        
    }
    private bool isRunning = true;

    public void StartGame()
    {
        isRunning = true;
        Pigeon.Reset();
        Readouts.Reset();
        //EnemySpawner.ResetEnemies();
        CanvasGroupDisplayer.Show(Stats);
    }
    
    public void EndGame()
    {
        isRunning = false;
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
