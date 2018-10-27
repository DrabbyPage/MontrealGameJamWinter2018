using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : MonoBehaviour {

    public float BoopForce;

    [SerializeField]
    float rotationAngle = -15.0f;
    public bool isSpinning = false;

    private Vector3 originalRot;

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
        originalRot = transform.rotation.eulerAngles;
        checker = Mathf.RoundToInt(Mathf.Abs(360 / rotationAngle));
    }
	
	// Update is called once per frame
	private void FixedUpdate ()
    {
        if (!isSpinning)
        {
            originalRot = transform.rotation.eulerAngles;
        }

        CheckCooldown();
        ApplySpin();
	}

    // Determines whether or not the user can currently spin
    private void CheckCooldown()
    {
        if (spinCounter == allowedSpins)
        {
            cooldownCounter++;

            // If the player's cooldown is at max, allow the player to act again
            if (cooldownCounter >= cooldownTime)
            {
                cooldownCounter = 0;
                spinCounter = 0;
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

                transform.rotation = Quaternion.Euler(originalRot);
                isSpinning = false;
                countUp = 0;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("BLOOP");
        col.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * BoopForce, ForceMode2D.Force);
    }
}
