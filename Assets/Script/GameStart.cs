using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public void handleGameStart(int diff)
    {
        switch (diff)
        {
            case 0:
                PlayerPrefs.SetInt("life", 9);
                PlayerPrefs.SetInt("grenade", 50);
                break;
            case 1:
                PlayerPrefs.SetInt("life", 3);
                PlayerPrefs.SetInt("grenade", 10);
                break ;
            case 99:
                if (Input.GetKey(KeyCode.D))
                {
                    PlayerPrefs.SetInt("life", 99);
                    PlayerPrefs.SetInt("grenade", 99);
                    break ;
                }
                return;
        }

        SceneManager.LoadScene("01-Level");
    }
    public void handleGameRestart()
    {
        SceneManager.LoadScene("00-Begin");
    }
}
