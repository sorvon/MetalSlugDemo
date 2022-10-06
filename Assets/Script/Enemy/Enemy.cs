using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float appearTime;
    public int life = 1;
    public float attackIntervalMin = 3;
    public float attackIntervalMax = 5;
    public GameObject bullet;
    public Sprite[] loopAnime;
    public float loopInterval = 0.2f;
    public Sprite[] attackAnime;
    public GameObject deadAnime;
    public GameObject player;
    protected float attackInterval;
    private bool isRed = false;
    // Start is called before the first frame update
    protected virtual void Start()
    {
        if(player == null) player = GameObject.FindWithTag("Player");
        attackInterval = Random.Range(attackIntervalMin, attackIntervalMax);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (player == null) player = GameObject.FindWithTag("Player");
        if (life <= 0)
        {
            if (player != null)
            {
                player.GetComponent<PlayerController>().score += 100;
            }
            Destroy(gameObject);
            return;
        }
    }
    private void OnDestroy()
    {
        if (!gameObject.scene.isLoaded) return;
        if (deadAnime != null)
            GameObject.Instantiate(deadAnime, transform.position, Quaternion.identity);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("PlayerGrenade"))
        {
            life -= collision.collider.gameObject.GetComponent<Grenade>().damage;
            if (player != null)
            {
                player.GetComponent<PlayerController>().score += 100;
            }
            Destroy(collision.collider.gameObject);
            if (life > 0 && !isRed)
            {
                setRed();
                Invoke("setUnred", 0.2f);
            }
        }
    }
    protected virtual void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            life -= other.gameObject.GetComponent<Bullet>().damage;
            if(player != null)
            {
                player.GetComponent<PlayerController>().score += 100;
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
                player.GetComponent<PlayerController>().score += 100;
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
        foreach(SpriteRenderer spriteRenderer in spriteRenderers)
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
