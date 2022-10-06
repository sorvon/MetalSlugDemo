using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyHuman : Enemy
{
    public GameObject shootPos;
    public GameObject downTrigger;
    private float timeCountAttact = 0;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        timeCountAttact += Time.deltaTime;
        Animator animator = GetComponent<Animator>();
        float distance = transform.position.x - player.transform.position.x;
        animator.SetFloat("distance", distance);
        GetComponent<BoxCollider2D>().enabled = true;
        
        GetComponent<Rigidbody2D>().velocity = new Vector3(0, 0);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("DownAttack"))
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("WalkLoop"))
        {
            if(distance > 0)
            {
                transform.localScale = new Vector3(1, 1, 1);
                GetComponent<Rigidbody2D>().velocity = new Vector3(-1, 0);
            }
            else
            {
                transform.localScale = new Vector3(-1, 1, 1);
                GetComponent<Rigidbody2D>().velocity = new Vector3(1, 0);
            }
        }
        if (timeCountAttact > attackInterval)
        {
            animator.SetTrigger("attack");
            timeCountAttact = 0;
        }
    }

    public void shoot()
    {
        GameObject.Instantiate(bullet, shootPos.transform.position, Quaternion.Euler(0, 0, transform.localScale.x == 1 ? 180 : 0));
    }
}
