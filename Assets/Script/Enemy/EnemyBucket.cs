using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBucket : Enemy
{
    // Start is called before the first frame update

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if(life < 3)
        {
            GetComponent<SpriteRenderer>().sprite = loopAnime[2];
        }
        else if(life < 8)
        {
            GetComponent<SpriteRenderer>().sprite = loopAnime[1];
        }
        else
        {
            GetComponent<SpriteRenderer>().sprite = loopAnime[0];
        }
    }
}
