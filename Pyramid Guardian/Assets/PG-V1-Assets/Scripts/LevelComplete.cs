using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    public string menuScene = "MainMenu";

    public SceneFader sceneFader;

    public string nextLevel;
    public int nextLevelUnlock = 2;

    public void Menu()
    {
        SavePlayerPrefs();
        sceneFader.FadeTo(menuScene);
    }

    public void Continue()
    {
        SavePlayerPrefs();
        sceneFader.FadeTo(nextLevel);
    }

    public void SavePlayerPrefs()
    {
        PlayerPrefs.SetInt("MaxLevelReached", nextLevelUnlock);
    }
}
