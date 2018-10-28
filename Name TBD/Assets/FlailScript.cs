﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlailScript : MonoBehaviour {

    public float BoopForce = 100f;

    [SerializeField]
    float fullRotation = 360.0f;
    [SerializeField]
    float rotationAngle = -15.0f;
    float originalRotationAngle;
    [SerializeField]
    float increasedSpeed = -15.0f;
    public bool spinFaster = false;

    private Vector3 originalRot;

    [SerializeField]
    int cooldownTime = 60;
    private int cooldownCounter = 0;
    private bool beginCooldown = false;

    [SerializeField]
    int timeSpinning = 60;
    private int spinDownCounter = 0;

    [SerializeField]
    int allowedSpinFasters = 4;
    private int spinFasterCounter = 0;

    private void Start()
    {
        originalRotationAngle = rotationAngle;
        originalRot = transform.rotation.eulerAngles;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (!spinFaster)
        {
            originalRot = transform.rotation.eulerAngles;
        }

        ActivateFastSpin();
        CheckCooldown();
        ApplySpin();
    }

    // Determines whether or not the user can currently spin
    private void ActivateFastSpin()
    {
        if (spinFasterCounter == allowedSpinFasters && !beginCooldown)
        {
            spinDownCounter++;

            // If the player's cooldown is at max, allow the player to act again
            if (spinDownCounter >= timeSpinning)
            {
                spinDownCounter = 0;
                spinFasterCounter = 0;
                rotationAngle = originalRotationAngle;
                beginCooldown = true;

                for (int i = 0; i < gameObject.transform.childCount; i++)
                {
                    gameObject.transform.GetChild(i).transform.position -= (gameObject.transform.GetChild(i).transform.up / 2);
                }
            }
        }
    }

    // Determines whether or not the user can currently spin
    private void CheckCooldown()
    {
        if (beginCooldown)
        {
            cooldownCounter++;

            // If the player's cooldown is at max, allow the player to act again
            if (cooldownCounter >= cooldownTime)
            {
                cooldownCounter = 0;
                beginCooldown = false;
                spinFaster = false;
            }
        }
    }

    // Makes the flail spin faster
    public void SpinFaster()
    {
        if (!spinFaster && spinFasterCounter < allowedSpinFasters)
        {
            spinFaster = true;
            spinFasterCounter++;
            rotationAngle += increasedSpeed;

            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                gameObject.transform.GetChild(i).transform.position += (gameObject.transform.GetChild(i).transform.up / 2);
            }
        }
    }

    // Applies the spin to the held object
    private void ApplySpin()
    {
        transform.Rotate(0, 0, rotationAngle);
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