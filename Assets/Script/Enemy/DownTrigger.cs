using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DownTrigger : MonoBehaviour
{
    public UnityEvent setDownTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBullet"))
        {
            setDownTrigger.Invoke();
        }
    }
}
