using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float BGSpeed;//…Ë÷√±≥æ∞ÀŸ∂»
    private Material BGMaterial;
    private bool startLoop = false;
    // Start is called before the first frame update
    void Start()
    {
        this.BGMaterial = GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        this.BGMaterial.mainTextureOffset = new Vector2(BGSpeed * Time.time, 0);
    }

    public void setStartLoop(bool value)
    {
        this.startLoop = value;
    }
}
