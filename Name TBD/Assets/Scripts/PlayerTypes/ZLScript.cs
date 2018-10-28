using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZLScript : MonoBehaviour {

    [SerializeField]
    GameObject thePlayer;
    Rigidbody2D playerRB;

    [SerializeField]
    float boopForce = 200f;

    [SerializeField]
    bool invisibleCharge = false;

    [SerializeField]
    float addSpeed = 10.0f;
    [SerializeField]
    float bigZekeTinyLuther = 0.1f;
    public bool isIncreasingSpeed = false;

    [SerializeField]
    int rushDistance = 4;
    Vector2 originalPos;
    Vector2 currentPos;

    [SerializeField]
    int cooldownTime = 45;
    private int cooldownCounter = 0;

    [SerializeField]
    int allowedCharges = 2;
    private int chargeCounter = 0;

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCooldown();
        ApplyCharge();

    }

    public void SetPlayerValues(GameObject player, Rigidbody2D rb)
    {
        thePlayer = player;
        playerRB = rb;

        transform.GetChild(0).GetComponent<BullHornsColliderScript>().Initialize(gameObject, boopForce);
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
            Vector3 increasedSpeed = transform.up * addSpeed;

            playerRB.velocity += new Vector2(increasedSpeed.x, increasedSpeed.y);

            chargeCounter++;
        }
    }
}
