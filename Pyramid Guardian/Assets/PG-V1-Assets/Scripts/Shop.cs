using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    BuildManager buildManager;

    public GameMaster gameMaster;

    public TurretBlueprint fireTurret;
    public TurretBlueprint waterTurret;
    public TurretBlueprint earthTurret;
    public TurretBlueprint airTurret;

    public GameObject notEnoughFavorPopup;
    public AnimationCurve fadeCurve;
    public Image popupBackImg;
    public Text popupText;
    private float favorPopupOffset = 80f;
    public float fadeTimeModifier = 1f;
    private bool favorErrorStarted = false;

    private GameObject targetTurretButton;

    private void Start()
    {
        buildManager = BuildManager.instance;
    }

    public void SetupShop()
    {
        fireTurret.UpdateTurretCosts("MaxFireFavor");
        waterTurret.UpdateTurretCosts("MaxWaterFavor");
        earthTurret.UpdateTurretCosts("MaxEarthFavor");
        airTurret.UpdateTurretCosts("MaxAirFavor");

        fireTurret.elementTowersUnlocked = gameMaster.fireTowersUnlocked;
        waterTurret.elementTowersUnlocked = gameMaster.waterTowersUnlocked;
        earthTurret.elementTowersUnlocked = gameMaster.earthTowersUnlocked;
        airTurret.elementTowersUnlocked = gameMaster.airTowersUnlocked;
    }

    public void SelectFireTurret(GameObject fireShopBtn)
    {
        Debug.Log("Fire Turret Selected");

        if(gameMaster.fireTowersUnlocked == 0)
        {
            Debug.Log("Display Error - Not enough Favor");
            if(!favorErrorStarted)
            {
                favorErrorStarted = true;
                StartCoroutine(NotEnoughFavorFadeIn(fireShopBtn));
            }
                
            return;
        }

        buildManager.SelectTurretToBuild(fireTurret);

    }

    public void SelectWaterTurret()
    {
        Debug.Log("Water Launcher Selected");

        if (gameMaster.waterTowersUnlocked == 0)
        {
            Debug.Log("Display Error - Not enough Favor");
            return;
        }

        buildManager.SelectTurretToBuild(waterTurret);
    }

    public void SelectAirTurret()
    {
        Debug.Log("Air Turret Selected");

        if (gameMaster.airTowersUnlocked == 0)
        {
            Debug.Log("Display Error - Not enough Favor");
            return;
        }

        buildManager.SelectTurretToBuild(airTurret);

    }

    public void SelectEarthTurret()
    {
        Debug.Log("Earth Launcher Selected");

        if (gameMaster.earthTowersUnlocked == 0)
        {
            Debug.Log("Display Error - Not enough Favor");
            return;
        }

        buildManager.SelectTurretToBuild(earthTurret);
    }

    IEnumerator NotEnoughFavorFadeIn(GameObject uiButton)
    {
        notEnoughFavorPopup.transform.position = uiButton.transform.position + new Vector3(0, favorPopupOffset, 0);
        float t = 0f;

        while (t < 1f)
        {
            Debug.Log("Running Fade in");
            t += Time.deltaTime * fadeTimeModifier;
            float a = fadeCurve.Evaluate(t);
            popupText.color = new Color(0f, 0f, 0f, a);
            popupBackImg.color = new Color(255f, 146f, 112f, a);
            yield return 0;
        }

        Debug.Log("Fade In completed. t value is: " + t);

        while (t > 0f)
        {
            t -= Time.deltaTime * fadeTimeModifier; ;
            float a = fadeCurve.Evaluate(t);
            popupText.color = new Color(0f, 0f, 0f, a);
            popupBackImg.color = new Color(255f, 146f, 112f, a);

            if(t <= 0)
            {
                favorErrorStarted = false;
            }

            yield return 0;
        }

        Debug.Log("Fade out completed");
    }
}
