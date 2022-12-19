using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public static class PigeonHUD
{
    private static bool isEnabled;
    public static StaminaBar StaminaBar;
    private static Game Game;
    private static Pigeon Pigeon;
    
    public static void InitializePigeonHUD(Game game)
    {
        Game = game;
        Pigeon = game.Pigeon;
       
        
    }

    public static void UpdateHUD()
    {
        if (isEnabled)
        {
            displayStamina();
            displayHealth();
            displayAmmo();
        }
        else
        {
            

        }
        
    }

    public static void EnablePigeonHUD()
    {
        isEnabled = true;
    }
    
    public static void DisablePigeonHUD()
    {
        isEnabled = false;
    }
    
    private static void displayAmmo()
    {
        
    }

    private static void displayHealth()
    {
        
    }
    
    private static void displayStamina()
    {
        Game.StaminaBar.SetStamina(Pigeon.getStamina()/GameParameters.pigeonMaximumStamina);
    }
    
}
