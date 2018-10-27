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
    [SerializeField]
    string currentPlayer;

    string p1_LSH_Name; //= "P1_LJS_H";
    string p1_LSV_Name; //= "P1_LJS_V";
    string p1_RT_Name;//= "P1_RT";
    string p1_RSH_Name; //= "P1_RJS_H";
    string p1_RSV_Name;// = "P1_RJS_V";

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

    GameObject currentArena;

    float currentArenaRadius;
    Vector3 center;

    GameManager managerHandle;
    Vector2 moveVector;


    bool dashButtonDown = false, doCooldown, isDashing;
    float currentDashTime;

    private void Awake()
    {
        p1_LSH_Name = currentPlayer + "_LJS_H";
        p1_LSV_Name = currentPlayer + "_LJS_V";
        p1_RT_Name = currentPlayer + "_RT";
        p1_RSH_Name = currentPlayer + "_RJS_H";
        p1_RSV_Name = currentPlayer + "_RJS_V";

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
        PerformAction();
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        MoveCharacter();
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
            case PLAYER_TYPE.BULL_RUSH:
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(1).GetChild(0).gameObject.SetActive(false);
                heldItem = transform.GetChild(1).gameObject;
                heldItem.GetComponent<BullHornsScript>().SetPlayerValues(this.gameObject, rb);
                break;
        }
    }

    // Moves the chosen character on the axis based on their speed stat
    private void MoveCharacter()
    {
        if (charType != PLAYER_TYPE.BULL_RUSH || !heldItem.GetComponent<BullHornsScript>().GetCharging())
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
    }

    // Allows the player to perform an action
    private void PerformAction()
    {
        // If the player can act and they input the action key
        if (canAct && Input.GetAxis(p1_RT_Name) == 1)
        {
         //   Debug.Log(charType);

            switch (charType)
            {
                // Makes the character spin
                case PLAYER_TYPE.SPIN:
                    heldItem.GetComponent<HammerScript>().SpinHeldObject();
                    break;
                case PLAYER_TYPE.BULL_RUSH:
                    heldItem.GetComponent<BullHornsScript>().BullHornCharge();
                    break;

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
}
