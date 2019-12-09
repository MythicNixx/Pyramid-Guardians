using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour
{
    public static bool gameIsOver;
    public string sceneName;

    public GameObject gameOverUI;
    public GameObject levelWonUI;

    public FavorSystem favorLevels;
    public Shop shop;

    public int fireTowersUnlocked;
    public int waterTowersUnlocked;
    public int earthTowersUnlocked;
    public int airTowersUnlocked;

    private void Start()
    {
        gameIsOver = false;

        if (sceneName == "TutorialLevel")
        {
            favorLevels.SetTutorialFavorValues();
        }

        else
        {
            favorLevels.CheckFavorStandings();
        }

        CheckTowerUnlocks();
        shop.SetupShop();
    }

    // Update is called once per frame
    void Update()
    {
        if(gameIsOver)
        {
            return;
        }

        if(PlayerStats.Lives <= 0)
        {
            EndGame();
        }
    }

    public void CheckTowerUnlocks()
    {
        airTowersUnlocked = favorLevels.CheckAirUnlocks();
        earthTowersUnlocked = favorLevels.CheckEarthUnlocks();
        fireTowersUnlocked = favorLevels.CheckFireUnlocks();
        waterTowersUnlocked = favorLevels.CheckWaterUnlocks();
    }

    void EndGame()
    {
        gameIsOver = true;
        gameOverUI.SetActive(true);
    }

    public void LevelWin()
    {
        gameIsOver = true;
        levelWonUI.SetActive(true);

        favorLevels.SetFireGodFavor();
        favorLevels.SetWaterGodFavor();
        favorLevels.SetEarthGodFavor();
        favorLevels.SetAirGodFavor();
    }

}
