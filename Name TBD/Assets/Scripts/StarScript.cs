using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarScript : MonoBehaviour {

    float moveSpeed = 8f;

    float deathTime = 2.0f;

    private float scaleUp = 0.06f;

    // Use this for initialization
    void Start()
    {
        Physics2D.GetIgnoreLayerCollision(8, 9);
    }

    // Update is called once per frame
    void Update()
    {
        TimerToDestroy();
        MoveBullet();
        ScaleUp();
    }

    void MoveBullet()
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = transform.up * moveSpeed;// AddForce(transform.up * moveSpeed);
    }

    void TimerToDestroy()
    {
        if (deathTime > 0)
        {
            deathTime = deathTime - Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void ScaleUp()
    {
        if (transform.localScale.x < 3 && transform.localScale.y < 3)
        {
            transform.localScale += new Vector3(scaleUp, scaleUp, 0);
        }

    }
    
}
