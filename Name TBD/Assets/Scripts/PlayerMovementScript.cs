using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct InputData
{
    public float xLeftAxisDeadzone, yLeftAxisDeadzone,
                 xRightAxisDeadzone, yRightAxisDeadzone;
};

[System.Serializable]

public struct DashData
{
    public float dashSpeed, maxDashTime, dashStopSpeed, dashDistance, dashCoolDownTime;
};

public class PlayerMovementScript : MonoBehaviour
{
    const string p1_LSH_Name = "P1_LJS_H";
    const string p1_LSV_Name = "P1_LJS_V";
    const string p1_RT_Name = "P1_RT";
    const string p1_RSH_Name = "P1_RJS_H";
    const string p1_RSV_Name = "P1_RJS_V";

    public enum PLAYER_TYPE
    {
        SPIN = 0,
        BULL_RUSH = 1,
        PEASHOOTER = 2,
        SPEAR = 3
        // ETC.
    };

    [SerializeField]
    InputData inputData;
    [SerializeField]
    DashData dashData;

    [Header("Player Type")]
    public PLAYER_TYPE charType;

    [Header("Force Data")]
    [SerializeField]
    float speed;
    [SerializeField]
    float rotationSpeed;

    public bool canAct = true;
    public GameObject heldItem;

    Rigidbody2D rb;
    private bool isSpinning = false;
    private Vector3 originalRot;

    GameObject currentArena;

    float currentArenaRadius;
    Vector3 center;

    GameManager managerHandle;
    Vector2 moveVector;


    bool dashButtonDown = false, doCooldown, isDashing;
    float currentDashTime;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        CheckHeldObject();
    }

    void Start ()
    {
        managerHandle = GameManager.getInstance();
        currentArena = managerHandle.GetCurrentEdge();
        currentArenaRadius = currentArena.GetComponent<CircleCollider2D>().radius;
        center = currentArena.GetComponent<Renderer>().bounds.center;
    }
	
    void Update()
    {
        ArenaUpdate();
        PerformAction();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        MoveCharacter();
        RotatePlayer();

        ApplySpin();

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
            case PLAYER_TYPE.BULL_RUSH:
                transform.GetChild(1).gameObject.SetActive(true);
                heldItem = transform.GetChild(1).gameObject;
                break;
        }

        originalRot = heldItem.transform.rotation.eulerAngles;
    }

    // Moves the chosen character on the axis based on their speed stat
    private void MoveCharacter()
    {
        moveVector = Vector2.zero;

        float moveHorizontal = Input.GetAxis(p1_LSH_Name);
        float moveVertical = Input.GetAxis(p1_LSV_Name);

        if (moveHorizontal > inputData.xLeftAxisDeadzone || moveHorizontal < -inputData.xLeftAxisDeadzone)
            moveVector.x = moveHorizontal * speed;
        else
            moveVector.x = 0;

        if (moveVertical > inputData.yLeftAxisDeadzone || moveVertical < -inputData.yLeftAxisDeadzone)
            moveVector.y = moveVertical * speed;
        else
            moveVector.y = 0;

        rb.velocity = moveVector;

    }

    // Allows the player to perform an action
    private void PerformAction()
    {
        // If the player can act and they input the action key
        if (canAct && Input.GetAxis(p1_RT_Name) == 1)
        {
            Debug.Log(charType);

            switch (charType)
            {
                // Makes the character spin
                case PLAYER_TYPE.SPIN:
                    SpinHeldObject();
                    break;
                case PLAYER_TYPE.BULL_RUSH:
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

    private void ApplySpin()
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

    // Performs any rotations
    private void RotatePlayer()
    {
        float xRotate = Input.GetAxis(p1_RSH_Name);
        float yRotate = Input.GetAxis(p1_RSV_Name);

        //Twin stick rotation
        if ((xRotate > inputData.xRightAxisDeadzone || xRotate < -inputData.xRightAxisDeadzone) ||
            (yRotate > inputData.yRightAxisDeadzone || yRotate < -inputData.yRightAxisDeadzone))
        {

            //help from KingKong320 @ https://bit.ly/2MMKvb5
            Vector2 dir = new Vector2(xRotate, yRotate);
            float rotation = Mathf.Atan2(dir.x, dir.y) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(new Vector3(0, 0, -rotation)), Time.deltaTime * rotationSpeed);

        }
    }

    private void ArenaUpdate()
    {
        //update arena bounds
        if (managerHandle.GetArenaFell())
        {
            currentArena = managerHandle.GetCurrentEdge();
            managerHandle.SetArenaFell(false);
        }

        //Check if player is still in arena
        if (!InsideArena())
        {
            //Debug.Log("DEAD");
            rb.velocity = Vector3.zero;
            StartCoroutine(TurnAroundAndDie(1.0f));
        }
    }

    private bool InsideArena()
    {
        return currentArena.GetComponent<CircleCollider2D>().bounds.Contains(new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, currentArena.transform.position.z));
    }

    private IEnumerator TurnAroundAndDie(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        rb.AddForce(-gameObject.transform.up * 1000.0f); //go die pls
    }

}
