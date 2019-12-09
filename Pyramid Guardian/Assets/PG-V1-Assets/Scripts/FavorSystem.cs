using UnityEngine;

[System.Serializable]
public class FavorSystem
{
    public int FireGodFavor;
    public int WaterGodFavor;
    public int EarthGodFavor;
    public int AirGodFavor;

    public static int levelFireFavorGain;
    public static int levelFireFavorLoss;
    public static int levelWaterFavorGain;
    public static int levelWaterFavorLoss;
    public static int levelEarthFavorGain;
    public static int levelEarthFavorLoss;
    public static int levelAirFavorGain;
    public static int levelAirFavorLoss;

    private int fireTowersUnlocked;
    private int waterTowersUnlocked;
    private int earthTowersUnlocked;
    private int airTowersUnlocked;

    public void CheckFavorStandings()
    {
        FireGodFavor = PlayerPrefs.GetInt("MaxFireFavor", 25);
        WaterGodFavor = PlayerPrefs.GetInt("MaxWaterFavor", 25);
        EarthGodFavor = PlayerPrefs.GetInt("MaxEarthFavor", 25);
        AirGodFavor = PlayerPrefs.GetInt("MaxAirFavor", 25);
    }

    public void SetTutorialFavorValues()
    {
        FireGodFavor = 100;
        WaterGodFavor = 100;
        EarthGodFavor = 100;
        AirGodFavor = 100;
    }

    #region End OF Level Favor Changes
    public void SetFireGodFavor()
    {
        FireGodFavor += levelFireFavorGain - levelFireFavorLoss;

        if (FireGodFavor > 1000)
            FireGodFavor = 1000;
        else if (FireGodFavor < -100)
            FireGodFavor = -100;

        PlayerPrefs.SetInt("MaxFireFavor", FireGodFavor);

        levelFireFavorGain = 0;
        levelFireFavorLoss = 0;
    }

    public void SetWaterGodFavor()
    {
        WaterGodFavor += levelWaterFavorGain - levelWaterFavorLoss;

        if (WaterGodFavor > 1000)
            WaterGodFavor = 1000;
        else if (WaterGodFavor < -100)
            WaterGodFavor = -100;

        PlayerPrefs.SetInt("MaxWaterFavor", WaterGodFavor);

        levelWaterFavorGain = 0;
        levelWaterFavorLoss = 0;
    }

    public void SetEarthGodFavor()
    {
        EarthGodFavor += levelEarthFavorGain - levelEarthFavorLoss;

        if (EarthGodFavor > 1000)
            EarthGodFavor = 1000;
        else if (EarthGodFavor < -100)
            EarthGodFavor = -100;

        PlayerPrefs.SetInt("MaxEarthFavor", EarthGodFavor);

        levelEarthFavorGain = 0;
        levelEarthFavorLoss = 0;
    }

    public void SetAirGodFavor()
    {
        AirGodFavor += levelAirFavorGain - levelAirFavorLoss;

        if (AirGodFavor > 1000)
            AirGodFavor = 1000;
        else if (AirGodFavor < -100)
            AirGodFavor = -100;

        PlayerPrefs.SetInt("MaxAirFavor", AirGodFavor);

        levelAirFavorGain = 0;
        levelAirFavorLoss = 0;
    }
    #endregion

    #region Check For Tower Unlocks
    public int CheckFireUnlocks()
    {
        if (FireGodFavor >= 300)
            fireTowersUnlocked = 3;

        else if (FireGodFavor >= 100)
            fireTowersUnlocked = 2;

        else if (FireGodFavor >= 1)
            fireTowersUnlocked = 1;

        else
            fireTowersUnlocked = 0;

        return fireTowersUnlocked;
    }

    public int CheckWaterUnlocks()
    {
        if (WaterGodFavor >= 300)
            waterTowersUnlocked = 3;

        else if (WaterGodFavor >= 100)
            waterTowersUnlocked = 2;

        else if (WaterGodFavor >= 1)
            waterTowersUnlocked = 1;

        else
            waterTowersUnlocked = 0;

        return waterTowersUnlocked;
    }

    public int CheckEarthUnlocks()
    {
        if (EarthGodFavor >= 300)
            earthTowersUnlocked = 3;

        else if (EarthGodFavor >= 100)
            earthTowersUnlocked = 2;

        else if (EarthGodFavor >= 1)
            earthTowersUnlocked = 1;

        else
            earthTowersUnlocked = 0;

        return earthTowersUnlocked;
    }

    public int CheckAirUnlocks()
    {
        if (AirGodFavor >= 300)
            airTowersUnlocked = 3;

        else if (AirGodFavor >= 100)
            airTowersUnlocked = 2;

        else if (AirGodFavor >= 1)
            airTowersUnlocked = 1;

        else
            airTowersUnlocked = 0;

        return airTowersUnlocked;
    }
    #endregion
}
