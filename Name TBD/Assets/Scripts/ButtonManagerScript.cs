using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonManagerScript : MonoBehaviour
{
    List<GameObject> buttonList;
    int currentButtonIndex = 0;
    float deadStick = 0.5f;

    string p1_LSH_Name; //= "P1_LJS_H";
    string p1_LSV_Name; //= "P1_LJS_V";
    string p1_RT_Name;//= "P1_RT";
    string p1_RSH_Name; //= "P1_RJS_H";
    string p1_RSV_Name;// = "P1_RJS_V";
    string p1_B_A_Name; // = "P1_B_A";
                       // Use this for initialization

    string p2_LSH_Name; //= "P1_LJS_H";
    string p2_LSV_Name; //= "P1_LJS_V";
    string p2_RT_Name;//= "P1_RT";
    string p2_RSH_Name; //= "P1_RJS_H";
    string p2_RSV_Name;// = "P1_RJS_V";
    string p2_B_A_Name; // = "P1_B_A";
                       // Use this for initialization

    float time;
    float selectTime = 0.3f;

    bool canSelectNewButton = true;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        time = selectTime;

        buttonList = new List<GameObject>();
        CheckButtonList();

        p1_LSH_Name = "P1"+ "_LJS_H";
        p1_LSV_Name = "P1" + "_LJS_V";
        p1_RT_Name = "P1" + "_RT";
        p1_RSH_Name = "P1" + "_RJS_H";
        p1_RSV_Name = "P1" + "_RJS_V";
        p1_B_A_Name = "P1" + "_B_A";

        p2_LSH_Name = "P2" + "_LJS_H";
        p2_LSV_Name = "P2" + "_LJS_V";
        p2_RT_Name = "P2" + "_RT";
        p2_RSH_Name = "P2" + "_RJS_H";
        p2_RSV_Name = "P2" + "_RJS_V";
        p2_B_A_Name = "P2" + "_B_A";
    }

    // Update is called once per frame
    void Update ()
    {
        CheckForButtonSelect();
        CheckForButtonInput();

        if(canSelectNewButton == false)
        {
            MakeSelectTrue();
        }
	}

    // adds teh buttons obj to the button list
    void CheckButtonList()
    {
        foreach (GameObject button in GameObject.FindGameObjectsWithTag("Button"))
        {
            //Debug.Log(button.name);
            buttonList.Add(button);
        }

        for(int i = 0; i < buttonList.Count; i++)
        {
            Debug.Log(buttonList[i].name);
        }
    }

    // checks for the stick input to select the button
    void CheckForButtonSelect()
    {
        if(canSelectNewButton)
        {
            if (Input.GetAxis(p1_LSV_Name) > deadStick)
            {
                // left stick is pressed up

                // move to up button
                if (currentButtonIndex > -1)
                {
                    buttonList[currentButtonIndex].GetComponent<ButtonScript>().UnhighlightButton();
                    currentButtonIndex = currentButtonIndex - 1;
                }
                else
                {
                    currentButtonIndex = buttonList.Count - 1;
                }

                buttonList[currentButtonIndex].GetComponent<ButtonScript>().HighlightButton();
                canSelectNewButton = false;

            }
            else if (Input.GetAxis(p1_LSV_Name) < -deadStick)
            {
                // left side on left sick

                // move to down button
                if (currentButtonIndex < buttonList.Count)
                {
                    buttonList[currentButtonIndex].GetComponent<ButtonScript>().UnhighlightButton();
                    currentButtonIndex = currentButtonIndex + 1;
                }
                else
                {
                    currentButtonIndex = 0;
                }

                buttonList[currentButtonIndex].GetComponent<ButtonScript>().HighlightButton();

                canSelectNewButton = false;

            }
        }

    }

    // checks for the button inputs to call scripts
    void CheckForButtonInput()
    {
        if(Input.GetButtonDown(p1_B_A_Name))
        {
            buttonList[currentButtonIndex].GetComponent<ButtonScript>().ButtonAct();
        }
    }

    void ReloadTime()
    {
        time = selectTime;
    }

    void MakeSelectTrue()
    {
        if(time > 0)
        {
            time = time - Time.deltaTime;
        }
        else
        {
            canSelectNewButton = true;
            time = selectTime;
        }
    }
}
