using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public static class Readouts
{
    
    public static int score;
    private static int wave;
    public static int PointsForKill = 10;
    private static Game Game;


    // Start is called before the first frame update
    //void Start()
    //{
        //Reset();
        //ShowScore(score);
        //ShowWave(0);
    //}

    public static void ShowScore(int score)
    {
        if (score < 0)
            score = 0;
        Game.ScoreDisplay.text = "SCORE: " + score;
    }

    public static void ShowWave(int wave)
    {
        if (wave < 0)
            wave = 0;
        Game.WaveDisplay.text = "WAVE " + wave;
    }

    public static void UpdateScore()
    {
        score = score + PointsForKill;
        ShowScore(score);
    }

    public static void UpdateWave()
    {
        wave = wave + 1;
        ShowWave(wave);
       
    }

    public static void Reset()
    {
        score = 0;
        wave = 0;
        ShowScore(score);
        ShowWave(wave);
    }

    public static void UpdateEndScreen()
    {
        Game.EndScoreText.text = "FINAL SCORE: " + score;
    }

    public static void InitializeReadouts(Game game)
    {
        Game = game;
    }
}
