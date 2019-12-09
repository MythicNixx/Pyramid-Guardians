using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuMain : MonoBehaviour
{
    public string levelToLoad = "Level_01";

    public GameObject settingsUI;
    public GameObject menuOptions;

    public SceneFader sceneFader;

    public void PlayGame()
    {
        sceneFader.FadeTo(levelToLoad);
    }

    public void LoadTutorial()
    {
        levelToLoad = "TutorialLevel";
        sceneFader.FadeTo(levelToLoad);
    }

    public void Settings()
    {
        menuOptions.SetActive(false);
        settingsUI.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("Exiting.");
        Application.Quit();
    }
}
