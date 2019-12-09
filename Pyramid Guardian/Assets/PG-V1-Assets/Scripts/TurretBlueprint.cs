using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TurretBlueprint
{
    public GameObject prefab;
    public string turretType;
    private int costOfTurret;
    public int initialCostofTurret = 100;

    // ===== Used to calculate the sell value and upgrade cost ===== \\
    private int totalTurretValue; 

    public GameObject level_2_Prefab;
    public GameObject level_3_Prefab;
    private int levelTwoUpgradeCost;
    private int levelThreeUpgradeCost;
    public int initialUpgradeCost = 60;

    public int elementTowersUnlocked;

    private float favorDiscount;

    public int GetSellAmount()
    {
        return totalTurretValue / 2;
    }

    public void SetUpgradeCost()
    {
        levelTwoUpgradeCost = (int)(initialUpgradeCost * (1 - favorDiscount));
        levelThreeUpgradeCost = levelTwoUpgradeCost + (int)(50 * (1 - favorDiscount));
    }

    public void SetCostOfTurret()
    {
        costOfTurret = (int)(initialCostofTurret * (1 - favorDiscount));
    }

    public void UpdateTurretCosts(string favorType)
    {
        // ===== Every 50 favor is a 5% discount up to a max of 50% ===== \\
        switch (PlayerPrefs.GetInt(favorType))
        {
            case int n when n < 50:
                favorDiscount = 0;
                break;

            case int n when (n >= 50 && n < 100):
                favorDiscount = 0.05f;
                break;

            case int n when (n >= 100 && n < 150):
                favorDiscount = 0.1f;
                break;

            case int n when (n >= 150 && n < 200):
                favorDiscount = 0.15f;
                break;

            case int n when (n >= 200 && n < 250):
                favorDiscount = 0.2f;
                break;

            case int n when (n >= 250 && n < 300):
                favorDiscount = 0.25f;
                break;

            case int n when (n >= 300 && n < 350):
                favorDiscount = 0.3f;
                break;

            case int n when (n >= 350 && n < 400):
                favorDiscount = 0.35f;
                break;

            case int n when (n >= 400 && n < 450):
                favorDiscount = 0.4f;
                break;

            case int n when (n >= 450 && n < 500):
                favorDiscount = 0.45f;
                break;

            default:
                favorDiscount = 0.5f;
                break;
        }

        SetCostOfTurret();
        SetUpgradeCost();
    }

    public int GetUpgradeCost(string upgradeLevel)
    {
        if (upgradeLevel == "LVL_2")
            return levelTwoUpgradeCost;

        else if (upgradeLevel == "LVL_3")
            return levelThreeUpgradeCost;

        else
            return levelThreeUpgradeCost;
    }

    public int GetTurretCost()
    {
        return costOfTurret;
    }

    public int GetTotalTurretValue()
    {
        return totalTurretValue;
    }

    public void SetTotalTurretValue(int addedValue)
    {
        totalTurretValue += addedValue;
    }
}
