using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeManagerScript : MonoBehaviour
{
    bool play1Selected = true;
    bool play2Selected = false;

    float numRandomizes = 0;

    int numOfChar = 15;

    List<int> player1Types;
    List<int> player2Types;

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
        player1Types = new List<int>();
        player2Types = new List<int>();

        //Assets / Resources / Masks / HammerWhite.png for each path
        randChar1Sprite = Resources.Load("Masks/HammerWhite", typeof(Sprite)) as Sprite;
        randChar2Sprite = Resources.Load("Masks/WhiteBull", typeof(Sprite)) as Sprite;
        randChar3Sprite = Resources.Load("Masks/WhitePShooter", typeof(Sprite)) as Sprite;
        randChar4Sprite = Resources.Load("Masks/WhiteSpear", typeof(Sprite)) as Sprite;
        randChar5Sprite = Resources.Load("Masks/WhiteMiner", typeof(Sprite)) as Sprite;

        randChar6Sprite = Resources.Load("Masks/WhiteTelefrag", typeof(Sprite)) as Sprite;
        randChar7Sprite = Resources.Load("Masks/WhitePlank", typeof(Sprite)) as Sprite;
        randChar8Sprite = Resources.Load("Masks/WhiteBig", typeof(Sprite)) as Sprite;
        randChar9Sprite = Resources.Load("Masks/WhiteIce", typeof(Sprite)) as Sprite;
        randChar10Sprite = Resources.Load("Masks/WhiteMagnet", typeof(Sprite)) as Sprite;

        randChar11Sprite = Resources.Load("Masks/WhiteSlow", typeof(Sprite)) as Sprite;
        randChar12Sprite = Resources.Load("Masks/WhiteMace", typeof(Sprite)) as Sprite;
        randChar13Sprite = Resources.Load("Masks/HammerWhite", typeof(Sprite)) as Sprite;
        randChar14Sprite = Resources.Load("Masks/HammerWhite", typeof(Sprite)) as Sprite;
        randChar15Sprite = Resources.Load("Masks/HammerWhite", typeof(Sprite)) as Sprite;

        Debug.Log(randChar1Sprite);
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

    public void RandomizeForPlayer()
    {
        int newRand;
        newRand = Random.Range(1, 15);

        if(play1Selected && player1Types.Count < 3)
        {
            player1Types.Add(newRand);
            MakeImageChar(player1Types.Count, newRand);
            SwitchPlayers();
        }
        else if(play2Selected && player2Types.Count < 3)
        {
            player2Types.Add(newRand);
            MakeImageChar(player2Types.Count, newRand);
            SwitchPlayers();
        }
    }

    void MakeImageChar(int currentImage, int charVal)
    {
        string player;
        string playerCharacter;

        if (play1Selected)
        {
            player = "P1";
        }
        else
        {
            player = "P2";
        }

        playerCharacter = "Char" + currentImage;

        GameObject charImage = GameObject.Find(player + playerCharacter);

        if (charVal == 1)
        {
            charImage.GetComponent<Image>().sprite = randChar1Sprite;
        }
        else if(charVal == 2)
        {
            charImage.GetComponent<Image>().sprite = randChar2Sprite;
        }
        else if (charVal == 3)
        {
            charImage.GetComponent<Image>().sprite = randChar3Sprite;
        }
        else if (charVal == 4)
        {
            charImage.GetComponent<Image>().sprite = randChar4Sprite;
        }
        else if (charVal == 5)
        {
            charImage.GetComponent<Image>().sprite = randChar5Sprite;
        }
        else if (charVal == 6)
        {
            charImage.GetComponent<Image>().sprite = randChar6Sprite;
        }
        else if (charVal == 7)
        {
            charImage.GetComponent<Image>().sprite = randChar7Sprite;
        }
        else if (charVal == 8)
        {
            charImage.GetComponent<Image>().sprite = randChar8Sprite;
        }
        else if (charVal == 9)
        {
            charImage.GetComponent<Image>().sprite = randChar9Sprite;
        }
        else if (charVal == 10)
        {
            charImage.GetComponent<Image>().sprite = randChar10Sprite;
        }
        else if (charVal == 11)
        {
            charImage.GetComponent<Image>().sprite = randChar11Sprite;
        }
        else if (charVal == 12)
        {
            charImage.GetComponent<Image>().sprite = randChar12Sprite;
        }
        else if (charVal == 13)
        {
            charImage.GetComponent<Image>().sprite = randChar13Sprite;
        }
        else if (charVal == 14)
        {
            charImage.GetComponent<Image>().sprite = randChar14Sprite;
        }
        else if(charVal == 15)
        {
            charImage.GetComponent<Image>().sprite = randChar15Sprite;
        }
        else
        {
            charImage.GetComponent<Image>().sprite = randChar1Sprite;
        }
        Debug.Log(charImage.name + " value: " + charVal);

        Debug.Log(charImage.name + " sprite: " + charImage.GetComponent<Image>().sprite);
    }
}
