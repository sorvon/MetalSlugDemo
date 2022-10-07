using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelThreeTrigger : MonoBehaviour
{
    public GameObject player;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerController playerController = player.GetComponent<PlayerController>();
            PlayerPrefs.SetInt("life", playerController.life);
            PlayerPrefs.SetInt("grenade", playerController.grenadeNum);
            PlayerPrefs.SetInt("score", playerController.score);
            SceneManager.LoadScene("03-Level");
        }
    }
}
