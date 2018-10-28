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
    bool invisibleCharge = false;

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

    int frames = 4;
    int frameCounter = 0;
    bool countingFrames = false;

    SpriteRenderer sr;

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckCooldown();
        ApplyCharge();

        CountingTelefragFrames();
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
            // change player color grey to indicate cool down
            GetComponentInParent<SpriteRenderer>().color = Color.grey;


            // If the player's cooldown is at max, allow the player to act again
            if (cooldownCounter >= cooldownTime)
            {
                cooldownCounter = 0;
                chargeCounter = 0;

                //set the skin color back to normal
                GetComponentInParent<SpriteRenderer>().color = Color.white;

                //plays the ready clip from the sound manager
                if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P1")
                {
                    SoundManagerScript.instance.EndCoolDownSound(true);
                }

                if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P2")
                {
                    SoundManagerScript.instance.EndCoolDownSound(false);
                }
            }
        }
    }

    public void CountingTelefragFrames()
    {
        if (countingFrames)
        {
            frameCounter++;

            Debug.Log("GO");

            if (frameCounter >= frames)
            {
                transform.GetChild(0).gameObject.SetActive(false);

                countingFrames = false;
                frameCounter = 0;
            }
        }
    }

    public void BullHornCharge()
    {
        if (!isCharging && chargeCounter < allowedCharges)
        {
            //moos
            if (SoundManagerScript.instance != null)
            {
                if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P1")
                {
                    SoundManagerScript.instance.MooSound(true);
                }
                if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P2")
                {
                    SoundManagerScript.instance.MooSound(false);
                }
            }


            if (invisibleCharge)
            {
                thePlayer.GetComponent<SpriteRenderer>().enabled = false;
                thePlayer.GetComponent<CircleCollider2D>().enabled = false;
            }

            else
            {
                gameObject.transform.GetChild(0).GetChild(0).GetComponent<SpriteRenderer>().enabled = true;
                transform.GetChild(0).gameObject.SetActive(true);
            }

            originalPos = new Vector2(thePlayer.transform.position.x, thePlayer.transform.position.y);

            isCharging = true;
            Vector3 increasedSpeed = transform.up * addSpeed;

            playerRB.velocity += new Vector2(increasedSpeed.x, increasedSpeed.y);
        }
    }

    private void ApplyCharge()
    {
        if (isCharging)
        {
            currentPos = new Vector2(thePlayer.transform.position.x, thePlayer.transform.position.y);
            int currentDistanceAway = Mathf.CeilToInt(Mathf.Abs(currentPos.x - originalPos.x) + Mathf.Abs(currentPos.y - originalPos.y) + 0.5f);
            //Debug.Log(currentDistanceAway);

            if (currentDistanceAway >= rushDistance)
            {
                HaltCharge();
            }
        }
    }

    public void HaltCharge()
    {
        if (invisibleCharge)
        {
            Debug.Log("HERE");

            transform.GetChild(0).gameObject.SetActive(true);

            Debug.Log(transform.GetChild(0).gameObject.activeSelf);

            thePlayer.GetComponent<SpriteRenderer>().enabled = true;
            thePlayer.GetComponent<CircleCollider2D>().enabled = true;
            countingFrames = true;
        }

        else
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }

        playerRB.velocity = new Vector2(0, 0);
        chargeCounter++;

        isCharging = false;
    }

    public bool GetCharging()
    {
        return isCharging;
    }
}
