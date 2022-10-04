using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGun : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer carGunRender;
    public Sprite[] carGunSprite;
    public Sprite[] playerSprite;
    public GameObject[] shootPos;
    public GameObject bullet;
    public float rotateInterval = 0.1f;
    public float shootInterval = 0.1f;
    private int carGunIndex = 0;
    private bool isCarGun = false;
    private Vector3[] positionList =
    {
        new Vector3(-1.45f, 0.2f, 0),
        new Vector3(-1.52f, 0.2f, 0),
        new Vector3(-1.69f, 0.2f, 0),
        new Vector3(-1.78f, 0.2f, 0),
        new Vector3(-1.84f, 0.24f, 0),
        new Vector3(-1.9f, 0.26f, 0),
        new Vector3(-1.93f, 0.34f, 0),
        new Vector3(-2.04f, 0.42f, 0),
        new Vector3(-2.04f, 0.51f, 0),
    };
    private Vector3[] rotationList =
    {
        new Vector3(0, 0, 90),
        new Vector3(0, 0, 78.75f),
        new Vector3(0, 0, 67.5f),
        new Vector3(0, 0, 56.25f),
        new Vector3(0, 0, 45),
        new Vector3(0, 0, 33.75f),
        new Vector3(0, 0, 22.5f),
        new Vector3(0, 0, 11.25f),
        new Vector3(0, 0, 0),
    };
    private PlayerController playerController;
    private float timeCount = 0;
    private float shootTimeCount = 0;


    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCarGun) return;
        if (Input.GetKey("k")) 
        {
            GetComponent<SpriteRenderer>().sprite = null;
            isCarGun = false;
            player.SetActive(true);
            return;
        } 

        
        timeCount += Time.deltaTime;
        if(timeCount > rotateInterval)
        {
            timeCount = 0;
            if (Input.GetKey("d")) setCarGunIndex(carGunIndex + 1);
            else if (Input.GetKey("a")) setCarGunIndex(carGunIndex - 1);
        }
        carGunRender.sprite = carGunSprite[Mathf.Abs(carGunIndex)];
        GetComponent<SpriteRenderer>().sprite = playerSprite[Mathf.Abs(carGunIndex)];
        transform.position = positionList[Mathf.Abs(carGunIndex)];
        Vector3 pos = shootPos[Mathf.Abs(carGunIndex)].transform.position;
        Vector3 rot = rotationList[Mathf.Abs(carGunIndex)];
        if (carGunIndex < 0)
        {
            carGunRender.flipX = true;
            GetComponent<SpriteRenderer>().flipX = false;
            Vector3 vec3 = positionList[Mathf.Abs(carGunIndex)];
            transform.localPosition = new Vector3(-2.9f - vec3.x, vec3.y, vec3.z);
            
            pos.x = 2 * shootPos[0].transform.position.x - pos.x;
            rot.z = 180 - rot.z;
        }
        else
        {
            carGunRender.flipX = false;
            GetComponent<SpriteRenderer>().flipX = true;
            transform.localPosition = positionList[Mathf.Abs(carGunIndex)];
        }
        player.transform.position = transform.position;
        shootTimeCount += Time.deltaTime;
        if(shootTimeCount > shootInterval)
        {
            shootTimeCount = 0;
            if (Input.GetKey("j"))
            {
                GameObject.Instantiate(bullet, pos, Quaternion.Euler(rot));
            }
        }
        
    }

    void setCarGunIndex(int value)
    {
        if(Mathf.Abs(value) < carGunSprite.Length)
        {
            carGunIndex = value;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "CarGunRange" && player.GetComponent<Rigidbody2D>().velocity.y < -0.5)
        {
            isCarGun = true;
            player.SetActive(false);
        }
        
    }
}
