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

public class TheoryMove : MonoBehaviour
{
    [SerializeField]
    string currentPlayer;

    string p1_LSH_Name; //= "P1_LJS_H";
    string p1_LSV_Name; //= "P1_LJS_V";
    string p1_RT_Name;//= "P1_RT";
    string p1_RSH_Name; //= "P1_RJS_H";
    string p1_RSV_Name;// = "P1_RJS_V";
                       // Use this for initialization

    public enum PLAYER_TYPE
    {
        SPIN = 0,
        BULL_RUSH = 1,
        PEASHOOTER = 2,
        SPEAR = 3,
        MINER = 4,
        TELEFRAG = 5,
        BIG_POLE = 6
        // ETC.
    };

    [SerializeField]
    InputData inputData;
    [SerializeField]
    DashData dashData;

    [Header("Player Type")]
    public PLAYER_TYPE charType;

    float deadStick = 0.5f;

    [SerializeField]
    float moveSpeed = 120.0f;

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
	
	// Update is called once per frame
	void Update ()
    {
        PerformAction();
	}

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckForInput();
        RotatePlayer();
    }

    // checking for movement input
    void CheckForInput()
    {
        if(Input.GetAxis(p1_LSH_Name) > deadStick)
        {
            // right side on left stick
            //gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * moveSpeed);
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.right * moveSpeed);
        }
        if (Input.GetAxis(p1_LSH_Name) < -deadStick)
        {
            // left side on left sick
            //gameObject.GetComponent<Rigidbody2D>().AddForce(-gameObject.transform.right * moveSpeed);
            gameObject.GetComponent<Rigidbody2D>().AddForce(-Vector3.right * moveSpeed);
        }

        if (Input.GetAxis(p1_LSV_Name) > deadStick)
        {
            // right side on left stick
            //gameObject.GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * moveSpeed);
            gameObject.GetComponent<Rigidbody2D>().AddForce(Vector3.up * moveSpeed);
        }
        if (Input.GetAxis(p1_LSV_Name) < -deadStick)
        {
            // left side on left sick
            //gameObject.GetComponent<Rigidbody2D>().AddForce(-gameObject.transform.up * moveSpeed);
            gameObject.GetComponent<Rigidbody2D>().AddForce(-Vector3.up * moveSpeed);
        }
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
            case PLAYER_TYPE.PEASHOOTER:
                transform.GetChild(2).gameObject.SetActive(true); 
                heldItem = transform.GetChild(2).gameObject;
                break;
            case PLAYER_TYPE.SPEAR:
                transform.GetChild(3).gameObject.SetActive(true);
                heldItem = transform.GetChild(3).gameObject;
                break;
            case PLAYER_TYPE.MINER:
                transform.GetChild(4).gameObject.SetActive(true);
                heldItem = transform.GetChild(4).gameObject;
                break;
            case PLAYER_TYPE.TELEFRAG:
                transform.GetChild(5).gameObject.SetActive(true);
                transform.GetChild(5).GetChild(0).gameObject.SetActive(false);
                heldItem = transform.GetChild(5).gameObject;
                heldItem.GetComponent<BullHornsScript>().SetPlayerValues(this.gameObject, rb);
                break;
            case PLAYER_TYPE.BIG_POLE:
                moveSpeed /= 2;
                transform.GetChild(6).gameObject.SetActive(true);
                heldItem = transform.GetChild(6).gameObject;
                break;
        }
    }

    // Allows the player to perform an action
    private void PerformAction()
    {
        // If the player can act and they input the action key
        if (canAct && Input.GetAxis(p1_RT_Name) > 0.2f)
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
                case PLAYER_TYPE.PEASHOOTER:
                    heldItem.GetComponent<PeaShooterScript>().Shoot();
                    break;
                case PLAYER_TYPE.SPEAR:
                    heldItem.GetComponent<SpearHolderScript>().ThrowWeapon();
                    break;
                case PLAYER_TYPE.MINER:
                    heldItem.GetComponent<MinePlacer>().PlaceMine();
                    break;
                case PLAYER_TYPE.TELEFRAG:
                    heldItem.GetComponent<BullHornsScript>().BullHornCharge();
                    break;
                case PLAYER_TYPE.BIG_POLE:
                    heldItem.GetComponent<HammerScript>().SpinHeldObject();
                    break;

            }
        }
    }

    // Performs any rotations
    private void RotatePlayer()
    {
        if (charType != PLAYER_TYPE.BIG_POLE && charType != PLAYER_TYPE.SPIN)
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
}
