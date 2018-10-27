using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerScript : MonoBehaviour {

    [SerializeField]
    float rotationAngle = -15.0f;
    public bool isSpinning = false;

    private Vector3 originalRot;
    private int checker;
    private int countUp = 0;

    // Use this for initialization
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

        ApplySpin();
	}

    // Spins the held item
    public void SpinHeldObject()
    {
        if (!isSpinning)
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
                transform.rotation = Quaternion.Euler(originalRot);
                isSpinning = false;
                countUp = 0;
            }
        }
    }
}
