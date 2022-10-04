using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelControl_1 : MonoBehaviour
{
    public GameObject car;
    public GameObject player;
    public Camera mainCamera;
    public GameObject BGLoop;
    public int life = 10;

    private PlayerController playerController;
    private float timeCount = 0;
    private bool[] jumpFlag = { true, true };
    private bool invokeFlag = true;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
        playerController.inputFlag = false;
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        
        Vector2 v = player.GetComponent<Rigidbody2D>().velocity;
        if(timeCount < 1)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(3, v.y);
        }
        else if(timeCount < 2)
        {
            if (jumpFlag[0])
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
                jumpFlag[0] = false;
            }
        }
        else if (timeCount < 3)
        {
            if (jumpFlag[1])
            {
                player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 5);
                jumpFlag[1] = false;
            }
        }
        else if (timeCount < 4)
        {
            float percent = timeCount - 3;
            mainCamera.transform.position = Vector3.Lerp(new Vector3(0.15f, -0.53f, -10), new Vector3(0.15f, 0.75f, -10), percent);
        }
        else if (timeCount < 6)
        {
            float percent = (timeCount - 4) / 2;
            mainCamera.transform.position = Vector3.Lerp(new Vector3(0.15f, 0.75f, -10), new Vector3(8.36f, 0.75f, -10), percent);
            car.transform.position = Vector3.Lerp(new Vector3(1.46f, -0.12f, 0), new Vector3(9.67f, -0.12f, 0), percent);
            player.transform.position = Vector3.Lerp(new Vector3(-0.2611525f, -0.2570573f, 0), new Vector3(7.8488475f, -0.2570573f, 0), percent);
        }
        else if (timeCount < 100)
        {
            playerController.inputFlag = true;
            BGLoop.GetComponent<BackgroundLoop>().setStartLoop(true);
        }

        checkPlayer();
    }

    void checkPlayer()
    {
        if(playerController.inputFlag == false) return;
        Vector3 viewPos = mainCamera.WorldToViewportPoint(player.transform.position);
        if (viewPos.x < 0 || viewPos.y < 0 || viewPos.x > 1 || viewPos.y > 1)
        {
            if (invokeFlag)
            {
                life--;
                invokeFlag = false;
                Invoke("resetPlayer", 1);
            }
            
        }
    }

    void resetPlayer()
    {
        invokeFlag = true;
        player.transform.position = new Vector3(7.8488475f, 1f, 0);
        player.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
    }
}
