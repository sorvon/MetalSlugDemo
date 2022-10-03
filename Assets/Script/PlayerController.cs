using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpSpeed = 3.0f;
    public SpriteRenderer upRender;
    public SpriteRenderer downRender;
    public Sprite[] idleLoopSpriteUp;
    public Sprite[] idleLoopSpriteDown;
    public Sprite[] walkLoopSpriteUp;
    public Sprite[] walkLoopSpriteDown;
    public Sprite[] upLoopSpriteUp;
    public Sprite[] downLoopSpriteUp;
    public Sprite[] downLoopSpriteDown;
    public Sprite[] jumpLoopSpriteUp;
    public Sprite[] jumpLoopSpriteDown;
    
    public Sprite[] normalShootSprite;
    public Sprite[] upShootSprite;
    public Sprite[] downShootSprite;
    public Sprite[] upThrowSprite;
    public Sprite[] downThrowSprite;
    public Sprite[] upHitSprite;
    public Sprite[] downHitSprite;
    public GameObject handgunBullet;
    public GameObject grenade;
    public GameObject normalShootPos;
    public GameObject upShootPos;
    public GameObject downShootPos;
    public Camera cam;

    private bool lookUp = false;
    private bool isShoot = false;
    private bool isThrow = false;
    private int idleLoopIndex = 0;
    private int walkLoopIndex = 0;
    private int downLoopIndex = 0;
    private int jumpLoopIndex = 0;
    private int lookUpIndex = 0;
    private int shootIndex = 0;
    private int throwIndex = 0;
    private int hitIndex = 0;
    private float idleLoopInterval = 1.0f / 5;
    private float walkLoopInterval = 1.0f / 10;
    private float downLoopInterval = 1.0f / 5;
    private float jumpLoopInterval = 1.0f / 20;

    private float lookUpInterval = 1.0f / 5;
    private float shootInterval = 1.0f / 50;
    private float throwInterval = 1.0f / 25;
    private float hitInterval = 1.0f / 25;
    private bool hitFlag = false;

    private float timeCount;
    private float additionalTimeCount;


    enum States
    {
        _idle,
        _walk,
        _down,
        _down_walk,
        _jump
    };

    private States m_state  = States._idle;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        tickLogic();
        tickRender();
    }

    void tickLogic()
    {
        float h = Input.GetAxisRaw("Horizontal");
        //Debug.Log(h);
        if (h < 0)
        {
            transform.transform.localScale = new Vector3(1, 1, 1);
        }
        else if(h > 0)
        {
            transform.transform.localScale = new Vector3(-1, 1, 1);
        }
        else
        {
            walkLoopIndex = 0; 
        }
        bool key_j = Input.GetKey("j");
        bool key_k = Input.GetKey("k");
        bool key_s = Input.GetKey("s");
        bool key_w = Input.GetKey("w");
        bool key_l = Input.GetKey("l");
        lookUp = key_w;
        if(!isShoot)
        {
            isShoot = key_j;
        }
        if (!isThrow)
        {
            isThrow = key_l;
        }
        
        if(! lookUp) lookUpIndex = 0;
        Vector2 v = GetComponent<Rigidbody2D>().velocity;
        v.x = speed * h;
        if (key_k && m_state != States._jump)
        {
            v.y = jumpSpeed;
        }
        
        if (cam.transform.transform.position.x < transform.position.x)
        {
            cam.transform.transform.position = new Vector3(transform.position.x, cam.transform.transform.position.y, cam.transform.transform.position.z);
        }
        
        if (Mathf.Abs(v.y) > 0.05)
        {
            setState(States._jump);
        }
        else if(key_s)
        {
            if (Mathf.Abs(v.x) > 0.05) {
                setState(States._down_walk);
                v.x = speed / 2* h ;
            }
            else setState(States._down);
        }
        else if(Mathf.Abs(v.x) > 0.05)
        {
            setState(States._walk);
        }
        else
        {
            setState(States._idle);
        }


        GetComponent<Rigidbody2D>().velocity = v;

    }
    

    void tickRender()
    {
        timeCount += Time.deltaTime;
        
        setPositton();
        switch (m_state)
        {
            case States._idle:
                if (timeCount < idleLoopInterval) break;
                timeCount = 0;
                upRender.sprite = idleLoopSpriteUp[idleLoopIndex];
                downRender.sprite = idleLoopSpriteDown[0];
                idleLoopIndex++;
                idleLoopIndex %= idleLoopSpriteUp.Length;
                break;

            case States._walk:
                if (timeCount < walkLoopInterval) break;
                timeCount = 0;
                upRender.sprite = walkLoopSpriteUp[walkLoopIndex];
                downRender.sprite = walkLoopSpriteDown[walkLoopIndex];
                walkLoopIndex++;
                walkLoopIndex %= walkLoopSpriteUp.Length;
                break;

            case States._down:
                if (timeCount < downLoopInterval) break;
                timeCount = 0;
                upRender.sprite = downLoopSpriteUp[downLoopIndex];
                downRender.sprite = downLoopSpriteDown[0];
                downLoopIndex++;
                Debug.Log(downLoopIndex);
                downLoopIndex %= downLoopSpriteUp.Length;
                break;

            case States._down_walk:
                if (timeCount < downLoopInterval) break;
                timeCount = 0;
                upRender.sprite = downLoopSpriteUp[downLoopIndex];
                downRender.sprite = downLoopSpriteDown[downLoopIndex];
                downLoopIndex++;
                Debug.Log(downLoopIndex);
                downLoopIndex %= downLoopSpriteUp.Length;
                break;

            case States._jump:
                if (timeCount < jumpLoopInterval) break;
                timeCount = 0;
                upRender.sprite = jumpLoopSpriteUp[jumpLoopIndex];
                downRender.sprite = jumpLoopSpriteDown[jumpLoopIndex];
                if (jumpLoopIndex < jumpLoopSpriteUp.Length - 1) jumpLoopIndex++;
                break;
        }
        additionalTimeCount += Time.deltaTime;
        if (isThrow)
        {
            Vector3 pos;
            if (m_state == States._down || m_state == States._down_walk)
            {
                pos = downShootPos.transform.position;
                upRender.sprite = downThrowSprite[throwIndex];
            }
            else
            {
                pos = normalShootPos.transform.position;
                upRender.sprite = upThrowSprite[throwIndex];
            }
            if (additionalTimeCount < throwInterval) return;
            additionalTimeCount = 0;

            if (throwIndex == 1)
            {
                GameObject.Instantiate(grenade, pos, Quaternion.Euler(0, 0, 0));
            }
            throwIndex++;

            if (throwIndex >= upThrowSprite.Length)
            {
                throwIndex = 0;
                isThrow = false;
                timeCount = 1;
            }
        }
        else if(isShoot && hitFlag)
        {
            if (m_state == States._down || m_state == States._down_walk)
            {
                upRender.sprite = downHitSprite[hitIndex];
            }
            else
            {
                upRender.sprite = upHitSprite[hitIndex];
            }
            if (additionalTimeCount < hitInterval) return;
            additionalTimeCount = 0;

            if (hitIndex == 1)
            {
                //GameObject.Instantiate(handgunBullet, pos, Quaternion.Euler(0, 0, z_rotation));
            }
            hitIndex++;

            if (hitIndex >= upHitSprite.Length)
            {
                hitIndex = 0;
                isShoot = false;
                timeCount = 1;
            }
        }
        else if (isShoot)
        {
            float z_rotation = 0f;
            if(transform.localScale.x == 1)
            {
                z_rotation = 180f;
            }
            Vector3 pos;
            if (lookUp && m_state != States._down && m_state != States._down_walk)
            {
                z_rotation = 90f;
                pos = upShootPos.transform.position;
                upRender.sprite = upShootSprite[shootIndex];
            }
            else if (m_state == States._down || m_state == States._down_walk)
            {
                pos = downShootPos.transform.position;
                upRender.sprite = downShootSprite[shootIndex];
            }
            else
            {
                pos = normalShootPos.transform.position;
                upRender.sprite = normalShootSprite[shootIndex];
            }
            if (additionalTimeCount < shootInterval) return;
            additionalTimeCount = 0;
            
            if (shootIndex == 1)
            {
                GameObject.Instantiate(handgunBullet, pos, Quaternion.Euler(0, 0, z_rotation));
            }
            shootIndex++;
            
            if (shootIndex >= upShootSprite.Length) 
            {
                shootIndex = 0;
                isShoot = false;
                timeCount = 1;
            }
        }
        else if (lookUp && m_state != States._down && m_state != States._down_walk)
        {
            upRender.sprite = upLoopSpriteUp[lookUpIndex];
            if (additionalTimeCount < lookUpInterval) return;
            additionalTimeCount = 0;
            lookUpIndex++;
            if (lookUpIndex >= upLoopSpriteUp.Length) lookUpIndex = 1;
        }
    }

    void setPositton()
    {
        switch (m_state)
        {
            case States._down:
                GetComponent<BoxCollider2D>().offset = new Vector2(-0.06f, 0.11f);
                GetComponent<BoxCollider2D>().size = new Vector2(0.65f, 0.55f);
                break;
            default:
                GetComponent<BoxCollider2D>().offset = new Vector2(-0.06f, 0.2f);
                GetComponent<BoxCollider2D>().size = new Vector2(0.65f, 0.74f);
                break;
        }
    }
    void setState(States value)
    {
        if (m_state != value)
        {
            m_state = value;
            idleLoopIndex = 0;
            downLoopIndex = 0;
            walkLoopIndex = 0;
            jumpLoopIndex = 0;
            timeCount = 1;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.name == "Grenade")
        {
            hitFlag = true;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Grenade")
        {
            hitFlag = false;
        }
        
    }
}
