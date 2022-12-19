using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class UI
{

    private static Game Game;
   

    public static void InitializeUI(Game game)
    {
        Game = game;
        PigeonHUD.InitializePigeonHUD(game);
        Readouts.InitializeReadouts(game);
    }
    
    
}
