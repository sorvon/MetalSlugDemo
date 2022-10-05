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
            Destroy(gameObject);
            return;
        }
    }
    private void OnDestroy()
    {
        if(deadAnime != null)
            GameObject.Instantiate(deadAnime, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            life -= other.gameObject.GetComponent<Bullet>().damage;
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
