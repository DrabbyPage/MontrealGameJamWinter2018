using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : MonoBehaviour {

    public float BoopForce = 100f;

    [SerializeField]
    float fullRotation = 360.0f;
    [SerializeField]
    float rotationAngle = -15.0f;
    public bool isSpinning = false;

    [SerializeField]
    int cooldownTime = 60;
    private int cooldownCounter = 0;

    [SerializeField]
    int allowedSpins = 4;
    private int spinCounter = 0;

    private int checker;
    private int countUp = 0;

    private void Start ()
    {
        checker = Mathf.RoundToInt(Mathf.Abs(fullRotation / rotationAngle));
    }
	
	// Update is called once per frame
	private void FixedUpdate ()
    {
        CheckCooldown();
        ApplySpin();
	}

    // Determines whether or not the user can currently spin
    private void CheckCooldown()
    {
        if (spinCounter == allowedSpins)
        {
            cooldownCounter++;
            // change player color grey to indicate cool down
            GetComponentInParent<SpriteRenderer>().color = Color.grey;

            // If the player's cooldown is at max, allow the player to act again
            if (cooldownCounter >= cooldownTime)
            {
                cooldownCounter = 0;
                spinCounter = 0;

                //set the skin color back to normal
                GetComponentInParent<SpriteRenderer>().color = Color.white;
                //plays the ready clip from the sound manager
                if (SoundManagerScript.instance != null)
                    SoundManagerScript.instance.EndCoolDownSound();
            }
        }
    }

    // Spins the held item
    public void SpinHeldObject()
    {
        if (!isSpinning && spinCounter < allowedSpins)
        {
            isSpinning = true;
        }
    }

    // Applies the spin to the held object
    private void ApplySpin()
    {
        if (isSpinning)
        {
            transform.Rotate(0, 0, rotationAngle);
            countUp++;

            if (countUp >= checker)
            {
                spinCounter++;

                //transform.rotation = Quaternion.Euler(originalRot);
                isSpinning = false;
                countUp = 0;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            //col.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * BoopForce);//, ForceMode2D.Force);
            col.gameObject.GetComponent<BoopScript>().Booped(gameObject.transform.up, BoopForce);

            //get the player that's being collided and determin which sound suarce is played out of
            if (col.gameObject.GetComponent<TheoryMove>().currentPlayer == "P2")
            {
                SoundManagerScript.instance.PlayHitSound(true);
            }
            if (col.gameObject.GetComponent<TheoryMove>().currentPlayer == "P1")
            {
                SoundManagerScript.instance.PlayHitSound(false);
            }
        }

    }
}
