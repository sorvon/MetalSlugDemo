using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStart : MonoBehaviour
{
    public string scene;
    private int difficult;
    public void handleGameStart(int diff)
    {
        difficult = diff;
        SceneManager.LoadScene(scene);
    }
}
