using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlailScript : MonoBehaviour {

    public float BoopForce = 100f;

    [SerializeField]
    float fullRotation = 360.0f;
    [SerializeField]
    float rotationAngle = -15.0f;
    [SerializeField]
    float increasedSpeed = -15.0f;
    public bool spinFaster = false;

    private Vector3 originalRot;

    [SerializeField]
    int cooldownTime = 60;
    private int cooldownCounter = 0;

    [SerializeField]
    int allowedSpins = 4;
    private int spinCounter = 0;

    private int checker;
    private int countUp = 0;

    private void Start()
    {
        originalRot = transform.rotation.eulerAngles;
        checker = Mathf.RoundToInt(Mathf.Abs(fullRotation / rotationAngle));
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!spinFaster)
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
    public void SpinFaster()
    {
        if (!spinFaster && spinCounter < allowedSpins)
        {
            spinFaster = true;
            rotationAngle += increasedSpeed;
        }
    }

    // Applies the spin to the held object
    private void ApplySpin()
    {
        transform.Rotate(0, 0, rotationAngle);
        countUp++;

        if (countUp >= checker)
        {
            spinCounter++;

            //transform.rotation = Quaternion.Euler(originalRot);
            spinFaster = false;
            rotationAngle -= increasedSpeed;
            countUp = 0;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            //col.gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * BoopForce);//, ForceMode2D.Force);
            col.gameObject.GetComponent<BoopScript>().Booped(gameObject.transform.up, BoopForce);
        }

    }
}
