using UnityEngine;
using UnityEngine.EventSystems;

using System.Collections;

public class Node : MonoBehaviour
{
    BuildManager buildManager;

    public Color hoverColor;
    public Color notEnoughMoneyColor;
    public Vector3 posOffset;

    public int upgradeCostIncrement = 50;

    [HideInInspector]
    public GameObject turret;
    public int turretLevel = 0;

    [HideInInspector]
    public TurretBlueprint turretBlueprint;

    [HideInInspector]
    public bool level2 = false;
    public bool level3 = false;

    private Renderer nodeRenderer;
    private Color startColor;

    private void Start()
    {
        buildManager = BuildManager.instance;
        nodeRenderer = GetComponent<Renderer>();
        startColor = nodeRenderer.material.color;
    }

    void BuildTurret(TurretBlueprint blueprint)
    {
        if (PlayerStats.Money < blueprint.GetTurretCost())
        {
            Debug.Log("You're too poor");
            return;
        }

        PlayerStats.Money -= blueprint.GetTurretCost();

        GameObject tempTurret = (GameObject)Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
        turret = tempTurret;

        turretBlueprint = blueprint;

        GameObject buildEffect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(buildEffect, 5f);

        UpdateTempFavorValues("Building");

        turretLevel++;
    }

    public void UpgradeTurret()
    {

        #region Upgrade to Level 2
        if(!level2 && turretBlueprint.elementTowersUnlocked >= 2)
        {
            if (PlayerStats.Money < turretBlueprint.GetUpgradeCost("LVL_2"))
            {
                Debug.Log("You're too poor to Upgrade that");
                return;
            }

            PlayerStats.Money -= turretBlueprint.GetUpgradeCost("LVL_2");

            // ==== Delete Old Turret ====\\
            Destroy(turret);

            // ==== Build New Turret ====\\
            GameObject tempTurret = (GameObject)Instantiate(turretBlueprint.level_2_Prefab, GetBuildPosition(), Quaternion.identity);
            turret = tempTurret;

            GameObject builtEffect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(builtEffect, 5f);

            turretLevel++;

            level2 = true;

            // ===== Add/Subtract favor for Gods ===== \\
            UpdateTempFavorValues("Upgrading");

            return;
        }
        #endregion

        #region Upgrade to Level 3
        if (!level3 && turretBlueprint.elementTowersUnlocked >= 3)
        {
            if (PlayerStats.Money < turretBlueprint.GetUpgradeCost("LVL_3"))
            {
                Debug.Log("You're too poor to Upgrade that");
                return;
            }

            PlayerStats.Money -= turretBlueprint.GetUpgradeCost("LVL_3");

            // ==== Delete Old Turret ====\\
            Destroy(turret);

            // ==== Build New Turret ====\\
            GameObject tempTurret = (GameObject)Instantiate(turretBlueprint.level_3_Prefab, GetBuildPosition(), Quaternion.identity);
            turret = tempTurret;

            GameObject builtEffect = (GameObject)Instantiate(buildManager.buildEffect, GetBuildPosition(), Quaternion.identity);
            Destroy(builtEffect, 5f);

            turretLevel++;
            level3 = true;

            // ===== Add/Subtract favor for Gods ===== \\
            UpdateTempFavorValues("Upgrading");
        }
        #endregion
    }

    public void SellTurret()
    {
        PlayerStats.Money += turretBlueprint.GetSellAmount();

        GameObject sellEffect = (GameObject)Instantiate(buildManager.sellEffect, GetBuildPosition(), Quaternion.identity);
        Destroy(sellEffect, 5f);

        UpdateTempFavorValues("Selling");

        turretLevel = 0;

        Destroy(turret);
        turretBlueprint = null;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        

        if(turret != null)
        {
            buildManager.SelectNode(this);
            return;
        }

        if (!buildManager.CanBuild)
            return;

        BuildTurret(buildManager.GetTurretToBuild());

    }

    public Vector3 GetBuildPosition()
    {
        return transform.position + posOffset;
    }

    void OnMouseEnter()
    {
        if (EventSystem.current.IsPointerOverGameObject())
            return;

        if (!buildManager.CanBuild)
            return;

        if(buildManager.HasMoney)
        {
            nodeRenderer.material.color = hoverColor;
        }

        else
            nodeRenderer.material.color = notEnoughMoneyColor;

    }

    void OnMouseExit()
    {
        nodeRenderer.material.color = startColor;
    }

    public void UpdateTempFavorValues(string action)
    {
        #region Fire Favor Updates
        if(turretBlueprint.turretType == "Fire")
        {
            if(action == "Selling")
            {
                FavorSystem.levelFireFavorLoss += (2 * turretLevel);
                FavorSystem.levelWaterFavorGain += turretLevel;
            }

            if(action =="Upgrading")
            {
                FavorSystem.levelFireFavorGain += 1;
                FavorSystem.levelWaterFavorLoss += 1;
            }

            if(action == "Building")
            {
                FavorSystem.levelFireFavorGain += 2;
                FavorSystem.levelAirFavorGain += 1;
                FavorSystem.levelEarthFavorGain += 1;
                FavorSystem.levelWaterFavorLoss += 2;
            }
        }
        #endregion

        #region Water Favor Updates
        if (turretBlueprint.turretType == "Water")
        {
            if (action == "Selling")
            {
                FavorSystem.levelFireFavorGain += turretLevel;
                FavorSystem.levelWaterFavorLoss += (2* turretLevel);
            }

            if (action == "Upgrading")
            {
                FavorSystem.levelFireFavorLoss += 1;
                FavorSystem.levelWaterFavorGain += 1;
            }

            if (action == "Building")
            {
                FavorSystem.levelFireFavorLoss += 2;
                FavorSystem.levelAirFavorGain += 1;
                FavorSystem.levelEarthFavorGain += 1;
                FavorSystem.levelWaterFavorGain += 2;
            }
        }
        #endregion

        #region Earth Favor Updates
        if (turretBlueprint.turretType == "Earth")
        {
            if (action == "Selling")
            {
                FavorSystem.levelEarthFavorLoss += (2 * turretLevel);
                FavorSystem.levelAirFavorGain += turretLevel;
            }

            if (action == "Upgrading")
            {
                FavorSystem.levelAirFavorLoss += 1;
                FavorSystem.levelEarthFavorGain += 1;
            }

            if (action == "Building")
            {
                FavorSystem.levelFireFavorGain += 1;
                FavorSystem.levelAirFavorLoss += 2;
                FavorSystem.levelEarthFavorGain += 2;
                FavorSystem.levelWaterFavorGain += 1;
            }
        }
        #endregion

        #region Air Favor Updates
        if (turretBlueprint.turretType == "Air")
        {
            if (action == "Selling")
            {
                FavorSystem.levelEarthFavorGain += turretLevel;
                FavorSystem.levelAirFavorLoss += (2 * turretLevel);
            }

            if (action == "Upgrading")
            {
                FavorSystem.levelAirFavorGain += 1;
                FavorSystem.levelEarthFavorLoss += 1;
            }

            if (action == "Building")
            {
                FavorSystem.levelFireFavorGain += 1;
                FavorSystem.levelAirFavorGain += 2;
                FavorSystem.levelEarthFavorLoss += 2;
                FavorSystem.levelWaterFavorGain += 1;
            }
        }
        #endregion
    }
}
