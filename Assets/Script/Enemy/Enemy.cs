using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int life = 1;
    public float attackIntervalMin = 3;
    public float attackIntervalMax = 5;
    public GameObject bullet;
    public Sprite[] loopAnime;
    public Sprite[] attackAnime;
    public GameObject deadAnime;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDestroy()
    {
        GameObject.Instantiate(deadAnime, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            life -= other.gameObject.GetComponent<Bullet>().damage;
            Destroy(other.gameObject);
        }
        else if (other.gameObject.CompareTag("PlayerGrenade"))
        {
            life -= other.gameObject.GetComponent<Grenade>().damage;
            Destroy(other.gameObject);
        }
        if (life <= 0)
        {
            Destroy(gameObject);
        }
    }
}
