using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_3 : MonoBehaviour
{
    public float speed = 1.0f;
    public float invincibleTime = 3;
    public int life = 10;
    public int grenadeNum = 10;
    public int score = 0;
    public GameObject bullet;
    public GameObject grenade;
    public GameObject deadAmime;
    public GameObject shootPos;
    public GameObject grenadePos;
    public float shootInterval = 0.2f;
    public float grenadeInterval = 1f;

    private float timeCount = 0;
    private float shootTimeCount = 0;
    private float grenadeTimeCount = 0;
    private bool isTransparent = false;  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        GetComponent<Rigidbody2D>().velocity = speed * new Vector2(h, v);
        timeCount += Time.deltaTime;
        shootTimeCount += Time.deltaTime;
        grenadeTimeCount += Time.deltaTime;
        if (invincibleTime > 0)
        {
            invincibleTime -= Time.deltaTime;
            if (isTransparent) Invoke("setUntransparent", 0.2f);
            else Invoke("setTransparent", 0.2f);
        }
        else setUntransparent();

        if (Input.GetKey(KeyCode.L))
        {
            if (grenadeTimeCount > grenadeInterval)
            {
                if (grenadeNum <= 0) return;
                grenadeNum--;
                grenadeTimeCount = 0;
                GameObject.Instantiate(grenade, grenadePos.transform.position, Quaternion.identity);
            }
        }
        else if (Input.GetKey(KeyCode.J))
        {
            if(shootTimeCount > shootInterval)
            {
                shootTimeCount = 0;
                GameObject.Instantiate(bullet, shootPos.transform.position, Quaternion.identity);
            }
        }


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (GetComponent<CapsuleCollider2D>().IsTouching(collision))
        {
            if (collision.CompareTag("EnemyBullet"))
            {
                if (invincibleTime > 0) return;
                playerDead();
                Destroy(collision.gameObject);
            }
        }
    }

    public void playerDead()
    {
        if (invincibleTime > 0) return;
        life--;
        GameObject.Instantiate(deadAmime, transform.position, Quaternion.identity);
        Invoke("reborn", 2);
        gameObject.SetActive(false);
    }

    void reborn()
    {
        gameObject.SetActive(true);
        invincibleTime = 5;
        Camera cam = Camera.main;
        transform.position = cam.ViewportToWorldPoint(new Vector3(0.3f, 0.7f, 10));
    }
    void setTransparent()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = new Color(1, 1, 1, 0.5f);
        }
        isTransparent = true;
    }
    void setUntransparent()
    {
        SpriteRenderer[] spriteRenderers = GetComponentsInChildren<SpriteRenderer>();
        foreach (SpriteRenderer spriteRenderer in spriteRenderers)
        {
            spriteRenderer.color = new Color(1, 1, 1, 1f);
        }
        isTransparent = false;
    }
}
