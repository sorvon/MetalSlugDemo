using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyHeilcopter : Enemy
{
    private GameObject gameController;
    private float timeCountMove = 0;
    private float timeCountAttact = 0;
    private bool toRight = true;
    private bool toInit = true;
    private float moveTime = 3;
   
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        gameController = GameObject.FindWithTag("GameController");
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        timeCountMove += Time.deltaTime;
        timeCountAttact += Time.deltaTime;
        Vector3 left = Camera.main.ViewportToWorldPoint(new Vector3(0.1f, 0.9f, 10));
        Vector3 right = Camera.main.ViewportToWorldPoint(new Vector3(0.9f, 0.9f, 10));
        gameController.GetComponent<LevelControl_1>().setCameraFollow(false);
        if (toInit)
        {
            
            transform.position = Vector3.Lerp(transform.position, left, 2 * Time.deltaTime);
            if (timeCountMove > 2)
            {
                toInit = false;
                timeCountMove = 0;
            }
        }
        else if (toRight)
        {
            transform.position = Vector3.Lerp(left, right, timeCountMove / moveTime);
            if (timeCountMove > moveTime)
            {
                toRight = false;
                timeCountMove = 0;
            }
        }
        else
        {
            transform.position = Vector3.Lerp(right, left, timeCountMove / moveTime);
            if (timeCountMove > moveTime)
            {
                toRight = true;
                timeCountMove = 0;
            }
        }
        
        if(timeCountAttact > attackInterval)
        {
            shoot();
            Invoke("shoot", 0.5f);
            Invoke("shoot", 1f);
        }
    }

    protected override void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        base.OnDestroy();
        gameController.GetComponent<LevelControl_1>().setCameraFollow(true);
    }

    void shoot()
    {
        Vector3 pos = transform.Find("shootPos").position;
        timeCountAttact = 0;
        GameObject.Instantiate(bullet, pos, Quaternion.Euler(0, 0, -90));
    }
}
