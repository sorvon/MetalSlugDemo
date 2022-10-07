using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelControl_1 : MonoBehaviour
{
    public GameObject car;
    public GameObject player;
    public Camera mainCamera;
    public GameObject lifeUi;
    public GameObject grenadeUi;
    public GameObject scoreUi;
    public GameObject stopUi;
    public GameObject failedUi;
    public GameObject completeUi;
    public GameObject BGLoop;
    public GameObject enemy;
    public GameObject randomEnemy;
    public GameObject boss;
    public GameObject bossBomb;
    public bool skipLevel_1 = false;
    
    private PlayerController playerController;
    private float timeCount = 0;
    private bool[] jumpFlag = { true, true };
    private bool invokeFlag = true;
    private bool isStop = false;
    private int level = 1;
    
    private Vector3 playerPos;
    private bool isCameraFollow = false;
    // Start is called before the first frame update
    void Start()
    {
        Transform enemyList = enemy.GetComponentInChildren<Transform>();
        playerController = player.GetComponent<PlayerController>();
        playerController.inputFlag = false;
        playerController.life = PlayerPrefs.GetInt("life");
        playerController.grenadeNum = PlayerPrefs.GetInt("grenade");
        playerController.score = 0;
        foreach (Transform enemy in enemyList)
        {
            enemy.gameObject.SetActive(false);
            enemy.gameObject.GetComponent<Enemy>().player = player;
            if (! skipLevel_1) StartCoroutine(awakeEnemy(enemy.gameObject));
        }
        if (! skipLevel_1)
        {
            randomEnemy.GetComponent<Enemy>().player = player;
            InvokeRepeating("genRandomEnemy", 6, 5);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            isStop = !isStop;
            stopUi.SetActive(isStop);
            if (isStop) Time.timeScale = 0;
            else Time.timeScale = 1;
        }
        timeCount += Time.deltaTime;
        switch (level)
        {
            case 1: tickLevel_1(); break;
            case 2: tickLevel_2(); break;
            case 3: tickLevel_3(); break;
            default:
                Debug.LogError("level: " + level);
                break;                
        }
        
    }
    private void LateUpdate()
    {
        if(playerController.life < 0)
        {
            failedUi.SetActive(true);
            return;
        }
        grenadeUi.GetComponent<NumberSprite>().num = playerController.grenadeNum;
        lifeUi.GetComponent<NumberSprite>().num = playerController.life;
        scoreUi.GetComponent<NumberSprite>().num = playerController.score;
        if(level == 1) isCameraFollow = false;
        if (isCameraFollow)
        {
            if (Camera.main.transform.position.x < player.transform.position.x + 0.8)
            {
                Camera.main.transform.position = Vector3.Lerp(
                    Camera.main.transform.position,
                    new Vector3(
                        player.transform.position.x + 1,
                        Camera.main.transform.position.y,
                        Camera.main.transform.position.z),
                    Time.deltaTime * 30);
            }
            else if (Camera.main.transform.position.x < player.transform.position.x + 1)
            {
                Camera.main.transform.position = Vector3.Lerp(
                    Camera.main.transform.position,
                    new Vector3(
                        player.transform.position.x + 1,
                        Camera.main.transform.position.y,
                        Camera.main.transform.position.z),
                    Time.deltaTime * 1000);
            }
        }
        else
        {
            Vector3 viewPlayerPos = Camera.main.WorldToViewportPoint(player.transform.position);
            if (viewPlayerPos.x > 1.0f)
            {
                player.transform.position = Camera.main.ViewportToWorldPoint(new Vector3(1.0f, viewPlayerPos.y, viewPlayerPos.z));
            }
        }
    }

    void tickLevel_1()
    {
        
        Vector2 v = player.GetComponent<Rigidbody2D>().velocity;
        if (timeCount < 1)
        {
            player.GetComponent<Rigidbody2D>().velocity = new Vector2(3, v.y);
        }
        else if (timeCount < 2)
        {
            if(player.transform.position.x < -0.67f)
            {
                player.transform.position = new Vector3(-0.67f, player.transform.position.y, player.transform.position.z);
            }
            else if(player.transform.position.x > 0.94f)
            {
                player.transform.position = new Vector3(0.94f, player.transform.position.y, player.transform.position.z);
            }
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
            mainCamera.transform.position = Vector3.Lerp(new Vector3(0.15f, -0.8f, -10), new Vector3(0.15f, 0.75f, -10), percent);
            playerPos = player.transform.position;
        }
        else if (timeCount < 6)
        {
            playerController.inputFlag = true;
            float percent = (timeCount - 4) / 2;
            mainCamera.transform.position = Vector3.Lerp(new Vector3(0.15f, 0.75f, -10), new Vector3(8.36f, 0.75f, -10), percent);
            car.transform.position = Vector3.Lerp(new Vector3(1.46f, -0.12f, 0), new Vector3(9.67f, -0.12f, 0), percent);
            //player.transform.position = Vector3.Lerp(new Vector3(-0.2611525f, -0.2570573f, 0), new Vector3(7.9488475f, -0.2570573f, 0), percent);
            player.transform.position = Vector3.Lerp(playerPos, playerPos + new Vector3(8.21f, 0, 0), percent);
        }
        else if (enemy.GetComponentInChildren<Transform>().childCount != 0 && !skipLevel_1)
        {
            playerController.inputFlag = true;
            BGLoop.GetComponent<BackgroundLoop>().setStartLoop(true);
        }
        else
        {
            CancelInvoke("genRandomEnemy");

            boss.transform.position = Vector3.Lerp(boss.transform.position, new Vector3(-3.37f, -1.54f, 0), Time.deltaTime);
            Destroy(car, 4.5f);
            Invoke("goToLevel_2", 3);
        }

        checkPlayer();
    }

    void tickLevel_2()
    {
        
    }

    void tickLevel_3()
    {

    }

    void checkPlayer()
    {
        if(playerController.inputFlag == false) return;
        Vector3 viewPos = mainCamera.WorldToViewportPoint(player.transform.position);
        if (viewPos.x < 0 || viewPos.y < 0 || viewPos.x > 1 || viewPos.y > 1)
        {
            if (invokeFlag)
            {
                playerController.life--;
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

    IEnumerator awakeEnemy(GameObject e)
    {
        yield return new WaitForSeconds(e.GetComponent<Enemy>().appearTime);
        e.SetActive(true);
    }

    void genRandomEnemy()
    {
        int num = Random.Range(1, 5);
        for(int i = 0; i < num; i++)
        {
            float pos_x = Random.Range(0.1f, 1.2f);
            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(pos_x, 1.1f, 10));
            GameObject.Instantiate(randomEnemy, pos, Quaternion.identity);
        }
    }

    void goToLevel_2()
    {
        if(bossBomb != null)bossBomb.SetActive(true);
        BGLoop.GetComponent<BackgroundLoop>().setStartLoop(false);
        boss.transform.position = Vector3.Lerp(boss.transform.position, new Vector3(3.18f, 0.62f, 0), 2 * Time.deltaTime);
        mainCamera.transform.position = Vector3.Lerp(mainCamera.transform.position, new Vector3(mainCamera.transform.position.x, -0.8f, mainCamera.transform.position.z), 2 * Time.deltaTime);
        level = 2;
        if (Camera.main.transform.position.y < -0.79f) isCameraFollow = true;
    }
    

    public void setCameraFollow(bool value)
    {
        isCameraFollow = value;
    }
}
