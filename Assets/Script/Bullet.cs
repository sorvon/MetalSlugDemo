using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5.0f;
    public float lifeTime = 3.0f;
    public int damage = 1;
    public Sprite[] bulletSprite;
    public bool isLoop = false;
    
    private float bulletInterval = 1.0f / 20;
    private int bulletIndex = 0;
    private float timeCount = 0;

    public GameObject fireSprite;
    public GameObject endSprite;
    private GameObject fireSpriteTmp;
    // Start is called before the first frame update
    void Start()
    {
        fireSpriteTmp = GameObject.Instantiate(fireSprite, transform.position, transform.rotation);
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!fireSpriteTmp)
        {
            timeCount += Time.deltaTime;
            GetComponent<SpriteRenderer>().sprite = bulletSprite[bulletIndex];
            transform.Translate(Vector3.right * speed * Time.deltaTime);
            if (timeCount > bulletInterval)
            {
                timeCount = 0;
                if (bulletIndex < bulletSprite.Length - 1) bulletIndex++;
                else if (isLoop) bulletIndex = 0;

            }
            transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = null;
        }
            
    }


    private void OnDestroy()
    {
        GameObject.Instantiate(endSprite, transform.position, transform.rotation);
    }
}
