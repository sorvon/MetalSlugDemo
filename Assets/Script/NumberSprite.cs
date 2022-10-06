using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NumberSprite : MonoBehaviour
{
    public Sprite[] numSpriteList;
    public GameObject[] objList;
    public int num = 0;
    // Update is called once per frame
    void Update()
    {
        if (num >= Mathf.Pow(10, objList.Length))
        {
            for (int i = 0; i < objList.Length; i++)
            {
                objList[i].GetComponent<Image>().sprite = numSpriteList[9];
            }
        }
        else
        {
            for(int i = 0; i < objList.Length; i++)
            {
                objList[i].GetComponent<Image>().sprite = numSpriteList[(num % (int)Mathf.Pow(10, i+1)) / (int)Mathf.Pow(10, i)];
            }
        }
        
    }
}
