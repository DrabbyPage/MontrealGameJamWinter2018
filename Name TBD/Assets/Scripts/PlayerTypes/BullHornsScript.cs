using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullHornsScript : MonoBehaviour
{
    [SerializeField]
    GameObject thePlayer;
    Rigidbody2D playerRB;

    [SerializeField]
    float boopForce = 200f;

    [SerializeField]
    float addSpeed = 10.0f;
    public bool isCharging = false;

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
	void FixedUpdate ()
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
            }
        }
    }

    public void BullHornCharge()
    {
        if (!isCharging && chargeCounter < allowedCharges)
        {
            originalPos = new Vector2(thePlayer.transform.position.x, thePlayer.transform.position.y);

            isCharging = true;
            Vector3 increasedSpeed = transform.up * addSpeed;

            playerRB.velocity += new Vector2(increasedSpeed.x, increasedSpeed.y);
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    private void ApplyCharge()
    {
        if (isCharging)
        {
            currentPos = new Vector2(thePlayer.transform.position.x, thePlayer.transform.position.y);
            int currentDistanceAway = Mathf.CeilToInt(Mathf.Abs(currentPos.x - originalPos.x) + Mathf.Abs(currentPos.y - originalPos.y));
            Debug.Log(currentDistanceAway);

            if (currentDistanceAway >= rushDistance)
            {
                HaltCharge();
            }
        }
    }

    public void HaltCharge()
    {
        playerRB.velocity = new Vector2(0, 0);
        chargeCounter++;

        isCharging = false;

        transform.GetChild(0).gameObject.SetActive(false);
    }

    public bool GetCharging()
    {
        return isCharging;
    }
}
