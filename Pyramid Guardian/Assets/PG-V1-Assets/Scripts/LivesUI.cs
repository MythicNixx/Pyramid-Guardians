using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LivesUI : MonoBehaviour
{
    public Text livesText;
    private bool zeroLives = false;

    private void Update()
    {
        if(PlayerStats.Lives == 1)
        {
            livesText.text = PlayerStats.Lives.ToString() + " Life";
        }

        else if(PlayerStats.Lives <= 0)
        {
            if(!zeroLives)
            {
                livesText.fontSize = (livesText.fontSize / 3) * 2;
                zeroLives = true;
            }
            
            livesText.text = "No Lives Left";
        }

        else
        {
            livesText.text = PlayerStats.Lives.ToString() + " Lives";
        }
        
    }
}
