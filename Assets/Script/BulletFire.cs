using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletFire : MonoBehaviour
{
    public Sprite[] fireSprite;
    private float fireInterval = 1.0f / 20;
    private int fireIndex = 0;
    private float timeCount = 0;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        GetComponent<SpriteRenderer>().sprite = fireSprite[fireIndex];
        if (timeCount < fireInterval) return;
        timeCount = 0;
        fireIndex++;
        if (fireIndex >= fireSprite.Length) Destroy(gameObject);
    }
}
