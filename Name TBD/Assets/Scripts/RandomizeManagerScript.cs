using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeManagerScript : MonoBehaviour
{
    bool play1Selected = true;
    bool play2Selected = false;

    Image P1_Char_1;
    Image P1_Char_2;
    Image P1_Char_3;

    Image P2_Char_1;
    Image P2_Char_2;
    Image P2_Char_3;

    Sprite randChar1Sprite;
    Sprite randChar2Sprite;
    Sprite randChar3Sprite;
    Sprite randChar4Sprite;
    Sprite randChar5Sprite;

    Sprite randChar6Sprite;
    Sprite randChar7Sprite;
    Sprite randChar8Sprite;
    Sprite randChar9Sprite;
    Sprite randChar10Sprite;

    Sprite randChar11Sprite;
    Sprite randChar12Sprite;
    Sprite randChar13Sprite;
    Sprite randChar14Sprite;
    Sprite randChar15Sprite;

    // Use this for initialization
    void Start()
    {
        randChar1Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        randChar2Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        randChar3Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        randChar4Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        randChar5Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        randChar6Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        randChar7Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        randChar8Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        randChar9Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        randChar10Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        randChar11Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        randChar12Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        randChar13Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        //randChar14Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
        //randChar15Sprite = Resources.Load("Masks/HammerWhite") as Sprite;
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void SwitchPlayers()
    {
        if(play1Selected)
        {
            play1Selected = false;
            play2Selected = true;
        }
        else
        {
            play1Selected = true;
            play2Selected = false;
        }
    }

     
}
