using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerCheckScript : MonoBehaviour
{
    public string currPlayer;

    float joySense = 0.4f;
    string p1_LSH_Name = "P1_LJS_H";
    string p1_LSV_Name = "P1_LJS_V";
    string p1_RSH_Name = "P1_RJS_H";
    string p1_RSV_Name = "P1_RJS_V";
    string p1_RT_Name = "P1_RT";
    string p1_LT_Name = "P1_LT";
    string p1_A_Name = "P1_B_A";
    string p1_B_Name = "P1_B_B";
    string p1_X_Name = "P1_B_X";
    string p1_Y_Name = "P1_B_Y";
    string p1_RB_Name = "P1_B_RB";
    string p1_LB_Name = "P1_B_LB";

    string p2_LSH_Name = "P2_LJS_H";
    string p2_LSV_Name = "P2_LJS_V";
    string p2_RSH_Name = "P2_RJS_H";
    string p2_RSV_Name = "P2_RJS_V";
    string p2_RT_Name = "P2_RT";
    string p2_LT_Name = "P2_LT";
    string p2_A_Name = "P2_B_A";
    string p2_B_Name = "P2_B_B";
    string p2_X_Name = "P2_B_X";
    string p2_Y_Name = "P2_B_Y";
    string p2_RB_Name = "P2_B_RB";
    string p2_LB_Name = "P2_B_LB";

    // Use this for initialization
    void Start ()
    {
      //  string p1_LSH_Name = "P1_LJS_H";

    }

    // Update is called once per frame
    void Update ()
    {
        CheckP1ControlInput();
        CheckP2ControlInput();

    }

    void CheckP1ControlInput()
    {
        if(Input.GetAxis(p1_LSH_Name) > joySense)
        {
            Debug.Log("left stick right");
        }
        else if (Input.GetAxis(p1_LSH_Name) < -joySense)
        {
            Debug.Log("left stick left");
        }

        if (Input.GetAxis(p1_RSH_Name) > joySense)
        {
            Debug.Log("right stick right");
        }
        else if (Input.GetAxis(p1_RSH_Name) < -joySense)
        {
            Debug.Log("right stick left");
        }

        if (Input.GetButton(p1_A_Name))
        {
            Debug.Log("a pressed");
        }
        if (Input.GetButton(p1_B_Name))
        {
            Debug.Log("b pressed");
        }
        if (Input.GetButton(p1_X_Name))
        {
            Debug.Log("x pressed");
        }
        if (Input.GetButton(p1_Y_Name))
        {
            Debug.Log("y pressed");
        }
        if (Input.GetButton(p1_RB_Name))
        {
            Debug.Log("rb pressed");
        }
        if (Input.GetButton(p1_LB_Name))
        {
            Debug.Log("lb pressed");
        }

        if (Input.GetAxis(p1_RT_Name) > joySense)
        {
            Debug.Log("right trigger down");
        }

        if (Input.GetAxis(p1_LT_Name) > joySense)
        {
            Debug.Log("left trigger down");
        }
    }

    void CheckP2ControlInput()
    {
        if (Input.GetAxis(p2_LSH_Name) > joySense)
        {
            Debug.Log("2 left stick right");
        }
        else if (Input.GetAxis(p2_LSH_Name) < -joySense)
        {
            Debug.Log("2 left stick left");
        }

        if (Input.GetAxis(p2_RSH_Name) > joySense)
        {
            Debug.Log("2 right stick right");
        }
        else if (Input.GetAxis(p2_RSH_Name) < -joySense)
        {
            Debug.Log("2 right stick left");
        }

        if (Input.GetButton(p2_A_Name))
        {
            Debug.Log("a 2 pressed");
        }
        if (Input.GetButton(p2_B_Name))
        {
            Debug.Log("b 2 pressed");
        }
        if (Input.GetButton(p2_X_Name))
        {
            Debug.Log("x 2 pressed");
        }
        if (Input.GetButton(p2_Y_Name))
        {
            Debug.Log("y 2 pressed");
        }
        if (Input.GetButton(p2_RB_Name))
        {
            Debug.Log("rb 2 pressed");
        }
        if (Input.GetButton(p2_LB_Name))
        {
            Debug.Log("lb 2 pressed");
        }

        if (Input.GetAxis(p2_RT_Name) > joySense)
        {
            Debug.Log("2 right trigger down");
        }

        if (Input.GetAxis(p2_LT_Name) > joySense)
        {
            Debug.Log("2 left trigger down");
        }
    }

}
