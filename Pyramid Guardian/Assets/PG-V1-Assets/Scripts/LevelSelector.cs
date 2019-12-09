using UnityEngine;
using UnityEngine.UI;

public class LevelSelector : MonoBehaviour
{
    public SceneFader fader;

    public Button[] levelBtns;

    private void Start()
    {
        int levelReached = PlayerPrefs.GetInt("MaxLevelReached", 1);

        for (int i = 0; i < levelBtns.Length; i++)
        {
            if(i+1 > levelReached)
                levelBtns[i].interactable = false;
        }
    }

    public void SelectLevel(string level)
    {
        fader.FadeTo(level);
    }
}
