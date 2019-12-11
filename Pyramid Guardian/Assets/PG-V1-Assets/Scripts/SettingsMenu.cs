using UnityEngine;

public class SettingsMenu : MonoBehaviour
{
    public GameObject mainMenuButtons;
    public GameObject settingsMenuUI;

    public GameObject resetConfirmationUI;

    public void Back()
    {
        settingsMenuUI.SetActive(false);
        mainMenuButtons.SetActive(true);
    }

    public void ResetGameProgress()
    {
        resetConfirmationUI.SetActive(true);
    }

    public void ConfirmResetProgress()
    {
        PlayerPrefs.SetInt("MaxLevelReached", 1);
        PlayerPrefs.SetInt("MaxFireFavor", 25);
        PlayerPrefs.SetInt("MaxWaterFavor", 25);
        PlayerPrefs.SetInt("MaxEarthFavor", 25);
        PlayerPrefs.SetInt("MaxAirFavor", 25);
        resetConfirmationUI.SetActive(false);
    }

    public void CloseConfirmation()
    {
        resetConfirmationUI.SetActive(false);
    }
}
