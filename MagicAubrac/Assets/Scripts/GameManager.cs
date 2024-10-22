using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameManager
{
    public static ClientsManager ClientsManager { get; set; }
    public static RecipesManager RecipesManager { get; set; }
    public static MenuManager MenuManager { get; set; }
    public static WinLoseManager LoseWinManager { get; set; }
}
