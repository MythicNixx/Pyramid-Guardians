using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutLevelManager : MonoBehaviour
{
    public static bool TutorialIsOver;

    public GameObject gameOverUI;
    public GameObject tutorialOverUI;

    public GameObject openingConvoPanel;
    public Text[] textBoxes;
    public GameObject[] openingTutPanels;

    public int numTutorialItems;
    public int tutorialItemsCompleted = 0;
    public string enemyTag = "Enemy";
    private bool waveOneDone = false;
    private bool blessingGranted= false;
    private bool blessingTwoGranted = false;
    private bool blessingThreeGranted = false;

    public GameObject FireButton;
    public GameObject WaterButton;
    public GameObject EarthButton;
    public GameObject AirButton;

    public GameObject waypoint_1;
    private int numTowersInLevel = 0;
    public GameObject tutorialUIBG;
    public Text tutorialText;

    public FavorSystem favorLevels;
    public Shop shop;
    public WaveSpawner waveSpawner;

    private void Start()
    {
        TutorialIsOver = false;
        waveSpawner.enabled = false;
        OpeningConversation();
    }

    // Update is called once per frame
    void Update()
    {
        if (TutorialIsOver)
        {
            return;
        }

        if(tutorialItemsCompleted < 1)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(waypoint_1.transform.position, enemy.transform.position);

                if (distanceToEnemy < 1f)
                {
                    tutorialItemsCompleted = 1;
                    StartCoroutine(TutorialPointOne());
                }
            }

            return;
        }

        if (!waveOneDone)
        {
            if (PlayerStats.roundsSurvived >= 2)
            {
                tutorialText.text = "Congratulations on Surviving the First wave of dumb tomb-raiders. They walk so slow that it's almost as if they want to die.";
                waveOneDone = true;
            }

            return;
        }

        Debug.Log("Tutorial-Items Completed: " + tutorialItemsCompleted);

        if (tutorialItemsCompleted < 2)
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);

            foreach (GameObject enemy in enemies)
            {
                float distanceToEnemy = Vector3.Distance(waypoint_1.transform.position, enemy.transform.position);

                if (distanceToEnemy < 1f)
                {
                    tutorialItemsCompleted = 2;

                    StartCoroutine(TutorialPointTwo());
                }
            }
        }
    }

    public void OpeningConversation()
    {
        int i = 0;

        while(openingConvoPanel.activeInHierarchy)
        {
            if(Input.GetMouseButtonDown(1))
            {
                textBoxes[i].enabled = false;
                textBoxes[i + 1].enabled = true;

                if (i >= 2)
                {
                    openingConvoPanel.SetActive(false);
                    WaveTutorial();
                }
            }

        } 
    }

    public void WaveTutorial()
    {
        openingTutPanels[0].SetActive(true);

        while (openingTutPanels[0].activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(1))
            {
                openingTutPanels[0].SetActive(false);
                FavorTutorial();
            }
        }
    }

    public void FavorTutorial()
    {
        openingTutPanels[1].SetActive(true);

        while (openingTutPanels[1].activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(1))
            {
                openingTutPanels[1].SetActive(false);
                PatienceTutorial();
            }
        }
    }

    public void PatienceTutorial()
    {
        openingTutPanels[2].SetActive(true);

        while (openingTutPanels[2].activeInHierarchy)
        {
            if(Input.GetMouseButtonDown(1))
            {
                openingTutPanels[2].SetActive(false);
                GodFavorTutorial();
            }
        }
    }

    public void GodFavorTutorial()
    {
        openingTutPanels[3].SetActive(true);

        while (openingTutPanels[3].activeInHierarchy)
        {
            if (Input.GetMouseButtonDown(1))
            {
                openingTutPanels[3].SetActive(false);
                EndOpeningTut();
            }
        }
    }

    public void EndOpeningTut()
    {
        int textBoxNumber = 2;

        textBoxes[2].enabled= true;
        openingConvoPanel.SetActive(true);

        while (openingConvoPanel.activeInHierarchy)
        {
            if(textBoxNumber >= textBoxes.Length)
            {
                tutorialUIBG.SetActive(true);
                openingConvoPanel.SetActive(false);
            }

            if (Input.GetMouseButtonDown(1))
            {
                textBoxes[textBoxNumber].enabled = false;
                textBoxes[textBoxNumber + 1].enabled = true;
                textBoxNumber++;
            }
        }
    }

    IEnumerator TutorialPointOne()
    {
        tutorialUIBG.SetActive(true);
        FireButton.SetActive(true);
        Time.timeScale = 0f;

        while(numTowersInLevel < 1)
        {
            GameObject[] numTowersPlaced = GameObject.FindGameObjectsWithTag("Tower");

            if(numTowersPlaced.Length >= 1)
            {
                Time.timeScale = 1f;
                numTowersInLevel = numTowersPlaced.Length;
                tutorialText.text = "Congratulations on placing your first tower! Or several if you got trigger-happy. None-the-less, let's see how this handles the first few tomb-raiders.";
            }

            yield return 0;
        }
    }

    IEnumerator TutorialPointTwo()
    {
        tutorialUIBG.SetActive(true);

        if (blessingGranted)
        {
            PlayerStats.Money += 1000;
        }

        if (PlayerStats.Money < 100)
        {
            MoneyLoan();
            yield return 0;
        }

        bool waterTowerPlaced = false;

        tutorialText.text = "Well it seems that I spoke too soon. These guys must be on something. They're trying to sprint into the tomb. At least they're following our nice path. FILLED WITH DEATH MACHINES! Call upon the power of Tefnut, the goddess of water, to slow these guys down.";
        FireButton.SetActive(true);
        WaterButton.SetActive(true);

        Time.timeScale = 0f;

        while (!waterTowerPlaced)
        {
            GameObject[] numTowersPlaced = GameObject.FindGameObjectsWithTag("Tower");

            foreach (GameObject tower in numTowersPlaced)
            {
                if(tower.name == "Water_Tower_LVL_1(Clone)")
                {
                    Time.timeScale = 1f;
                    waterTowerPlaced = true;
                    tutorialText.text = "Nice! The inconspicious water fountain should slow them down enough for the fireballs to take them out.";
                }
            }

            yield return 0;
        }
    }

    public void MoneyLoan()
    {
        Time.timeScale = 0f;

        if (!blessingGranted)
        {
            tutorialText.text = "It seems someone got quite carried away and spent all their favor. How am I supposed to teach you to defend the pharoah if you can't appeal to the gods? I suppose I can grant you this blessing of 1000 more favor. But don't go spending it all again!";
            blessingGranted = true;
        }

        else if(!blessingTwoGranted)
        {
            tutorialText.text = "It seems someone got carried away and spent all their favor. Again! How am I supposed to teach you to defend the pharoah if you can't listen to me? I really shouldn't have to do this, but here. Damn Ra for giving me this job ...";
            blessingTwoGranted = true;
        }

        else if (!blessingThreeGranted)
        {
            tutorialText.text = "Why? Why must you defy me and spend all this favor? It's not like it'll make this go any faster! You chose this path! Fine. Take this.";
            blessingThreeGranted = true;
        }

        else
        {
            tutorialText.text = "I give up. You're hopelessly unteachable. Take this 10000 favor. You can't possibly spend it all. So good luck.";
            PlayerStats.Money += 10000;
        }

    }

    public void TutorialComplete()
    {
        TutorialIsOver = true;
        tutorialOverUI.SetActive(true);
    }

}
