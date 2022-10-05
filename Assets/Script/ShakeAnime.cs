using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShakeAnime : MonoBehaviour
{
    public float shakeInterval = 0.2f;
    public float shakeRangeMax = 0.03f;
    private float timeCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCount += Time.deltaTime;
        if (timeCount > shakeInterval)
        {
            timeCount = 0;
            transform.localPosition = new Vector3(Random.Range(0, shakeRangeMax), Random.Range(0, shakeRangeMax), 0);
        }
    }
}
