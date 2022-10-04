using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyParaboy : Enemy
{
    private float timeCount = 0;
    private float attackInterval;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        attackInterval = Random.Range(attackIntervalMin, attackIntervalMax);
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        

        if(player.transform.position.x > transform.position.x)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        float deltaCos = Vector3.Dot(Vector3.Normalize(player.transform.position - transform.position), Vector3.down);
        Vector3 pos = transform.Find("shootPos1").position;
        if (deltaCos > Mathf.Cos(Mathf.PI / 16))
        {
            pos = transform.Find("shootPos1").position;
            GetComponent<SpriteRenderer>().sprite = loopAnime[0];
        }
        else if (deltaCos > Mathf.Cos(Mathf.PI / 8))
        {
            pos = transform.Find("shootPos2").position;
            GetComponent<SpriteRenderer>().sprite = loopAnime[1];
        }
        else if (deltaCos > Mathf.Cos(Mathf.PI *3 / 8))
        {
            pos = transform.Find("shootPos3").position;
            GetComponent<SpriteRenderer>().sprite = loopAnime[2];
        }
        else
        {
            pos = transform.Find("shootPos4").position;
            GetComponent<SpriteRenderer>().sprite = loopAnime[3];
        }

        if (timeCount > attackInterval)
        {
            timeCount = 0;
            attackInterval = Random.Range(attackIntervalMin, attackIntervalMax);
            GameObject.Instantiate(bullet, pos, Quaternion.FromToRotation(Vector3.right, player.transform.position - pos));
        }
    }
}
