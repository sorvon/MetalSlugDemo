using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarGun : MonoBehaviour
{
    public GameObject player;
    public SpriteRenderer carGunRender;
    public Sprite[] carGunSprite;
    public Sprite[] playerSprite;
    public float rotateInterval = 0.1f;
    
    private int carGunIndex = 0;
    private Vector3[] offsetList =
    {
        new Vector3(-0.04f, 1.22f, 0),
        new Vector3(-0.04f, 1.22f, 0),
        new Vector3(-0.04f, 1.22f, 0),
        new Vector3(-0.04f, 1.22f, 0),
        new Vector3(-0.04f, 1.22f, 0),
        new Vector3(-0.04f, 1.22f, 0),
        new Vector3(-0.04f, 1.22f, 0),
        new Vector3(-0.04f, 1.22f, 0),
        new Vector3(-0.04f, 1.22f, 0),
    };
    private PlayerController playerController;
    private float timeCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerController = player.GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("k")) player.SetActive(true);
        if (player.activeSelf) return;
        timeCount += Time.deltaTime;
        if(timeCount > rotateInterval)
        {
            timeCount = 0;
            if (Input.GetKey("d")) setCarGunIndex(carGunIndex + 1);
            else if (Input.GetKey("a")) setCarGunIndex(carGunIndex - 1);
        }
        carGunRender.sprite = carGunSprite[Mathf.Abs(carGunIndex)];
        if(carGunIndex < 0)
        {
            carGunRender.flipX = true;
        }
        else
        {
            carGunRender.flipX = false;
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
            player.SetActive(false);
            Debug.Log("trigger!");
        }
        
    }
}
