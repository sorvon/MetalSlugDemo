using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHostage : Enemy
{
    public GameObject rewardDrop;
    private bool isReward = false;
    private bool isLeft = true;
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
        Animator animator = GetComponent<Animator>();
        animator.SetInteger("life", life);
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunLoopanim"))
        {
            if (GetComponent<Rigidbody2D>().velocity.x == 0)
                GetComponent<Rigidbody2D>().velocity = new Vector2(-0.5f, 0);
            if (isLeft) Invoke("toRightInvoke", 1f);
            else Invoke("toLeftInvoke", 1f);
        }
        else if(animator.GetCurrentAnimatorStateInfo(0).IsName("Disappear"))
        {
            transform.localScale = new Vector3(1, 1, 1);
            GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
            Destroy(gameObject, 10);
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Drop"))
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);

        if (other.gameObject.CompareTag("Player"))
        {
            Animator animator = GetComponent<Animator>();
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("RunLoopanim"))
            {
                if (!isReward)
                {
                    isReward = true;
                    GameObject.Instantiate(rewardDrop, transform.position, Quaternion.identity);
                }
                animator.SetTrigger("PlayerTrigger");
            } 
        }
    }

    private void toLeftInvoke()
    {
        isLeft = true;
        transform.localScale = new Vector3(1, 1, 1);
        GetComponent<Rigidbody2D>().velocity = new Vector2(-1, 0);
    }
    private void toRightInvoke()
    {
        isLeft = false;
        transform.localScale = new Vector3(-1, 1, 1); 
        GetComponent<Rigidbody2D>().velocity = new Vector2(1, 0);
    }
}
