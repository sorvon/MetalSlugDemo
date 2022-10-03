using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySprite : MonoBehaviour
{
    public Sprite[] sprite;
    public float spriteInterval = 1.0f / 20;
    private int spriteIndex = 0;
    private float timeCount = 0;

    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        GetComponent<SpriteRenderer>().sprite = sprite[spriteIndex];
        if (timeCount < spriteInterval) return;
        timeCount = 0;
        spriteIndex++;
        if (spriteIndex >= sprite.Length) Destroy(gameObject);
    }
}
