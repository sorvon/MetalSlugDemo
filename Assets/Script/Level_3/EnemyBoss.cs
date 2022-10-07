using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyBoss : MonoBehaviour
{
    public int life = 20;
    public float attackIntervalMin = 1;
    public float attackIntervalMax = 5;
    public Sprite brokenSprite1;
    public Sprite brokenSprite2;
    public GameObject bullet;
    public GameObject player;
    public GameObject shootPos;
    public UnityEvent addWinCount;
    protected float attackInterval;
    private float attackTimeCount = 0;
    private bool isBroken = false;
    private bool isRed = false;
    private int initLife;
    // Start is called before the first frame update
    void Start()
    {
        initLife = life;
        if (player == null) player = GameObject.FindWithTag("Player");
        attackInterval = Random.Range(attackIntervalMin, attackIntervalMax);
     
    }

    // Update is called once per frame
    void Update()
    {
        if (isBroken) return;
        attackTimeCount += Time.deltaTime;
        
        if (player == null) player = GameObject.FindWithTag("Player");
        if (life <= 0)
        {
            isBroken = true;
            addWinCount.Invoke();
            GetComponent<SpriteRenderer>().sprite = brokenSprite2;
            return;
        }
        else if (life <= initLife / 2)
        {
            GetComponent<SpriteRenderer>().sprite = brokenSprite1;
        }

        if(bullet != null)
        {
            if (attackTimeCount > attackInterval)
            {
                attackTimeCount = 0;
                attackInterval = Random.Range(attackIntervalMin, attackIntervalMax);
                GameObject.Instantiate(bullet, shootPos.transform.position, Quaternion.FromToRotation(Vector3.right, player.transform.position - shootPos.transform.position));
            }
        }
        
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (life <= 0) return;
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            life -= other.gameObject.GetComponent<Bullet>().damage;
            if (player != null)
            {
                player.GetComponent<PlayerController_3>().score += 100;
            }
            Destroy(other.gameObject);
            if (life > 0 && !isRed)
            {
                setRed();
                Invoke("setUnred", 0.2f);
            }
        }
        else if (other.gameObject.CompareTag("PlayerGrenade"))
        {
            life -= other.gameObject.GetComponent<Grenade>().damage;
            if (player != null)
            {
                player.GetComponent<PlayerController_3>().score += 100;
            }
            Destroy(other.gameObject);
            if (life > 0 && !isRed)
            {
                setRed();
                Invoke("setUnred", 0.2f);
            }
        }
    }

    void setRed()
    {
        isRed = true;
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = new Color(1, 0.5f, 0.5f, 1);
        }
    }

    void setUnred()
    {
        isRed = false;
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = Color.white;
        }
    }
}
