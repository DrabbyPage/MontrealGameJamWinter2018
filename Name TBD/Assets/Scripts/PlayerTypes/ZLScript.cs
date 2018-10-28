using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZLScript : MonoBehaviour
{

    [SerializeField]
    GameObject thePlayer;
    Rigidbody2D playerRB;

    [SerializeField]
    float boopForce = 200f;

    [SerializeField]
    float addSpeed = 10.0f;
    [SerializeField]
    float bigZekeTinyLuther = 0.1f;
    public bool isIncreasingSpeed = false;
    
    Vector2 originalPos;
    Vector2 currentPos;
    Vector3 increasedSpeed = new Vector3 (0, 20, 0);

    [SerializeField]
    int cooldownTime = 45;
    private int cooldownCounter = 0;

    [SerializeField]
    int allowedCharges = 1;
    private int chargeCounter = 0;

    private bool noVelocity = false;

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCooldown();

        if (!noVelocity)
        {
            playerRB.AddForce(new Vector2(increasedSpeed.x, increasedSpeed.y));
        }
    }

    public void SetPlayerValues(GameObject player, Rigidbody2D rb)
    {
        thePlayer = player;
        playerRB = rb;

        transform.GetChild(0).GetComponent<ZLColliderScript>().Initialize(boopForce);
        playerRB.velocity += new Vector2(transform.forward.x, transform.forward.y);
    }

    // Determines whether or not the user can currently charge
    private void CheckCooldown()
    {
        if (chargeCounter == allowedCharges)
        {
            cooldownCounter++;

            // If the player's cooldown is at max, allow the player to act again
            if (cooldownCounter >= cooldownTime)
            {
                cooldownCounter = 0;
                chargeCounter = 0;
                isIncreasingSpeed = false;
            }
        }
    }

    public void ZekeAndLutherCharge()
    {
        if (!isIncreasingSpeed && chargeCounter < allowedCharges)
        {
            addSpeed += bigZekeTinyLuther;

            isIncreasingSpeed = true;
            increasedSpeed = transform.up * addSpeed;

            boopForce += addSpeed;

            thePlayer.GetComponent<TheoryMove>().IncreaseSpeed(addSpeed);

            //playerRB.velocity += new Vector2(increasedSpeed.x, increasedSpeed.y);

            chargeCounter++;
        }
    }

    public void DisableVelocity ()
    {
        noVelocity = true;
    }

    public void EnableVelocity()
    {
        noVelocity = false;
    }
}
