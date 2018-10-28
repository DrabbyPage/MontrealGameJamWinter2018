using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetScript : MonoBehaviour
{
    [SerializeField]
    float farBoop = 150.0f;

    [SerializeField]
    float midBoop = 75.0f;

    [SerializeField]
    float closeBoop = 75.0f;

    [SerializeField]
    int allowedMagnet;
    int magnetCounter = 0;

    [SerializeField]
    int cooldownTime = 15;
    int cooldownCounter = 0;

    // Use this for initialization
    void Start ()
    {
        transform.GetChild(0).GetComponent<MagnetAreaScript>().SetMagnetAreaData(gameObject.transform.parent.gameObject, farBoop);
        transform.GetChild(1).GetComponent<MagnetAreaScript>().SetMagnetAreaData(gameObject.transform.parent.gameObject, midBoop);
        transform.GetChild(2).GetComponent<MagnetAreaScript>().SetMagnetAreaData(gameObject.transform.parent.gameObject, closeBoop);
    }

    // Update is called once per frame
    void Update ()
    {
		
	}

    public void ReversePolarity()
    {
        if (allowedMagnet < magnetCounter)
        {
            for (int i = 0; i < 3; i++)
            {
                transform.GetChild(i).GetComponent<MagnetAreaScript>().ReversePolarity();
            }

            magnetCounter++;
        }
    }

    // Determines whether or not the user can currently reverse polarity
    private void CheckCooldown()
    {
        if (magnetCounter == allowedMagnet)
        {
            cooldownCounter++;

            // If the player's cooldown is at max, allow the player to act again
            if (cooldownCounter >= cooldownTime)
            {
                cooldownCounter = 0;
                magnetCounter = 0;
            }
        }
    }
}
