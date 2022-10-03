using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    public GameObject bombSprite;
    public float lifeTime = 3;
    public Vector3 speed = new Vector3(3, 3, 0);
    public float angularSpeed = -180;
    public int damage = 5;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = speed;
        GetComponent<Rigidbody2D>().angularVelocity = angularSpeed;
        Destroy(gameObject, lifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnDestroy()
    {
        GameObject.Instantiate(bombSprite, transform.position, Quaternion.identity);
    }
}
