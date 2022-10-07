using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class LevelControl_3 : MonoBehaviour
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
    public GameObject randomEnemy;
    public GameObject boss;
    public bool skipNormal = false;
    private int winCount = 0;
    private PlayerController_3 playerController;
    private bool invokeFlag = true;
    private bool isStop = false;
    private float oldScore;
    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController_3>();
        playerController.life = PlayerPrefs.GetInt("life");
        playerController.grenadeNum = PlayerPrefs.GetInt("grenade");
        playerController.score = PlayerPrefs.GetInt("score");
        oldScore = PlayerPrefs.GetInt("score");
        if (! skipNormal)
        {
            randomEnemy.GetComponent<EnemyPlane>().player = player;
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
        if(playerController.score - oldScore >= 5000 || skipNormal)
        {
            goToBoss();
        }
        if(winCount >= 6)
        {
            completeUi.SetActive(true);
        }
        if (boss.activeSelf)
        {
            boss.transform.position = Vector3.Lerp(boss.transform.position, Camera.main.ViewportToWorldPoint(new Vector3(0.7f, 0.6f, 10)), Time.deltaTime);
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
        int num = Random.Range(3, 8);
        for(int i = 0; i < num; i++)
        {         
            if (GameObject.FindObjectsOfType<EnemyPlane>().Length > 15) return;
            float pos_x = Random.Range(0.1f, 1.2f);
            Vector3 pos = Camera.main.ViewportToWorldPoint(new Vector3(pos_x, 1.1f, 10));
            GameObject.Instantiate(randomEnemy, pos, Quaternion.identity);
        }
    }
    void goToBoss()
    {
        CancelInvoke("genRandomEnemy");
        boss.SetActive(true);
    }

    public void addWinCount()
    {
        winCount++;
    }
}
