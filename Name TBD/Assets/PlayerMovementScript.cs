using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementScript : MonoBehaviour
{
    public enum PLAYER_TYPE
    {
        SPIN = 0,
        BULL_RUSH = 1,
        PEASHOOTER = 2,
        SPEAR = 3
        // ETC.
    };

    [Header("Player Type")]
    public PLAYER_TYPE charType;

    [Header("Force Data")]
    public float speed;
    public bool canAct = true;
    public GameObject heldItem;

    Rigidbody2D rb;
    private bool isSpinning = false;
    private Vector3 originalRot;


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody2D>();

        CheckHeldObject();
	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        MoveCharacter();
        PerformAction();
        RotatePlayer();
	}

    private void CheckHeldObject()
    {
        switch (charType)
        {
            // Makes the character spin
            case PLAYER_TYPE.SPIN:
                transform.GetChild(0).gameObject.SetActive(true);
                heldItem = transform.GetChild(0).gameObject;
                break;
        }

        originalRot = heldItem.transform.rotation.eulerAngles;
    }

    // Moves the chosen character on the axis based on their speed stat
    private void MoveCharacter()
    {
        float moveHorizontal = Input.GetAxis("Horizontal") * speed;
        float moveVertical = Input.GetAxis("Vertical") * speed;

        rb.velocity = new Vector2(moveHorizontal, moveVertical);
    }

    // Allows the player to perform an action
    private void PerformAction()
    {
        // If the player can act and they input the action key
        if (canAct && Input.GetKey(KeyCode.Space))
        {
            Debug.Log(charType);

            switch (charType)
            {
                // Makes the character spin
                case PLAYER_TYPE.SPIN:
                    SpinHeldObject();
                    break;
            }
        }
    }

    // Spins the held item
    private void SpinHeldObject()
    {
        if (!isSpinning)
        {
            isSpinning = true;
        }
    }

    // Performs any rotations
    private void RotatePlayer()
    {
        if (isSpinning)
        {
            heldItem.transform.Rotate(0, 0, -15);
            
            if (Mathf.RoundToInt(heldItem.transform.rotation.eulerAngles.z) == Mathf.RoundToInt(originalRot.z))
            {
                transform.rotation = Quaternion.Euler(originalRot);
                isSpinning = false;
            }
        }
    }
}
