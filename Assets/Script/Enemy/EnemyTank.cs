using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTank : Enemy
{

    private float timeCount = 0;
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        Vector3 pos = transform.Find("shootPos").position;
        timeCount += Time.deltaTime;
        if (timeCount > attackInterval)
        {
            timeCount = 0;
            attackInterval = Random.Range(attackIntervalMin, attackIntervalMax);
            if (Vector3.Distance(player.transform.position, pos) < 4)
            {
                GameObject.Instantiate(bullet, pos, Quaternion.identity);
            }
        }
    }
}
