using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float BGSpeed = 1;//…Ë÷√±≥æ∞ÀŸ∂»
    private Material BGMaterial;
    public bool startLoop = false;
    private float timeCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        this.BGMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (startLoop) 
        {
            timeCount += Time.deltaTime;
            this.BGMaterial.mainTextureOffset = new Vector2(BGSpeed * timeCount % 1, 0);
        }
        else
        {
            timeCount = 0;
            this.BGMaterial.mainTextureOffset = Vector2.Lerp(this.BGMaterial.mainTextureOffset, new Vector2(1, 0), 2* Time.deltaTime);
        }
    }

    public void setStartLoop(bool value)
    {
        this.startLoop = value;
    }
}
