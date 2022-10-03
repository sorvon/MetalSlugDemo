using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnd : MonoBehaviour
{
    public Sprite[] endSprite;
    private float endInterval = 1.0f / 20;
    private int endIndex = 0;
    private float timeCount = 0;

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        GetComponent<SpriteRenderer>().sprite = endSprite[endIndex];
        if (timeCount < endInterval) return;
        timeCount = 0;
        endIndex++;
        if (endIndex >= endSprite.Length) Destroy(gameObject);
    }
}
