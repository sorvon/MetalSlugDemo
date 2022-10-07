using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrigger : MonoBehaviour
{
    public GameObject[] enemyList;
    public float interval = 1f;
    private int index = 0;
    private bool triggerFlag = true;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (triggerFlag)
            {
                triggerFlag = false;
                for (int i = 0; i < enemyList.Length; i++)
                {
                    Invoke("setActiveInvoke", interval * i);
                }
            }
            
        }
        
    }
    void setActiveInvoke()
    {
        enemyList[index].SetActive(true);
        index++;
    }
}
