using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretUI : MonoBehaviour
{
    public GameObject ui;

    public Text upgradeCost;
    public Button upgradeBtn;

    public Text sellAmount;
    

    private Node target;

    public void SetTarget(Node t)
    {
        target = t;

        transform.position = target.GetBuildPosition();

        if(!target.level2)
        {
            if(target.turretBlueprint.elementTowersUnlocked < 2)
            {
                upgradeCost.text = "Not Enough " + target.turretBlueprint.turretType + " God Favor";
                upgradeBtn.interactable = false;
                return;
            }

            upgradeCost.text = "$" + target.turretBlueprint.GetUpgradeCost("LVL_2");
            upgradeBtn.interactable = true;
        }

        else if (!target.level3)
        {
            if (target.turretBlueprint.elementTowersUnlocked < 3)
            {
                upgradeCost.text = "Not Enough " + target.turretBlueprint.turretType + " God Favor";
                upgradeBtn.interactable = false;
                return;
            }

            upgradeCost.text = "$" + target.turretBlueprint.GetUpgradeCost("LVL_3");
            upgradeBtn.interactable = true;
        }

        else
        {
            upgradeCost.text = "Max Level";
            upgradeBtn.interactable = false;
        }

        sellAmount.text = "$" + target.turretBlueprint.GetSellAmount();

        ui.SetActive(true);
    }


    public void Hide()
    {
        ui.SetActive(false);
    }

    public void Upgrade()
    {
        target.UpgradeTurret();
        BuildManager.instance.DeselectNode();
    }

    public void Sell()
    {
        target.SellTurret();
        BuildManager.instance.DeselectNode();
    }
}
