using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPlane : MonoBehaviour
{
    public float appearTime;
    public int life = 1;
    public float attackIntervalMin = 1;
    public float attackIntervalMax = 5;
    public GameObject bullet;
    public GameObject player;
    public GameObject shootPos;
    public float moveInterval = 3f;
    protected float attackInterval;
    private float moveTimeCount = 0;
    private float attackTimeCount = 0;
    private Vector3 aimPos = new Vector3(1, 1, 0);    
    // Start is called before the first frame update
    void Start()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
        attackInterval = Random.Range(attackIntervalMin, attackIntervalMax);
        aimPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.3f, 0.9f), Random.Range(0.1f, 0.9f), 10));
    }

    // Update is called once per frame
    void Update()
    {
        moveTimeCount += Time.deltaTime;
        attackTimeCount += Time.deltaTime;
        
        if (player == null) player = GameObject.FindWithTag("Player");
        if (life <= 0)
        {
            if (player != null)
            {
                player.GetComponent<PlayerController_3>().score += 100;
            }
            Destroy(gameObject);
            return;
        }
        if(moveTimeCount > moveInterval)
        {
            moveTimeCount = 0;
            aimPos = Camera.main.ViewportToWorldPoint(new Vector3(Random.Range(0.3f, 0.9f), Random.Range(0.1f, 0.9f), 10));
        }
        transform.position = Vector3.Lerp(transform.position, aimPos, Time.deltaTime);
        if(attackTimeCount > attackInterval)
        {
            attackTimeCount = 0;
            attackInterval = Random.Range(attackIntervalMin, attackIntervalMax);
            GameObject.Instantiate(bullet, shootPos.transform.position, Quaternion.FromToRotation(Vector3.right, player.transform.position - shootPos.transform.position));
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            life -= other.gameObject.GetComponent<Bullet>().damage;
            if (player != null)
            {
                player.GetComponent<PlayerController_3>().score += 100;
            }
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("PlayerGrenade"))
        {
            life -= other.gameObject.GetComponent<Grenade>().damage;
            if (player != null)
            {
                player.GetComponent<PlayerController_3>().score += 100;
            }
            Destroy(other.gameObject);
        }
    }
}
