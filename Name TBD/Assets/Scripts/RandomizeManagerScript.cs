using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomizeManagerScript : MonoBehaviour
{
    [SerializeField]
    GameObject p1AcornTop, p1AcornBottom, p2AcornTop, p2AcornBottom;
    [SerializeField]
    Sprite commonTop, commonBottom, uncommonTop, uncommonBottom, rareTop, rareBottom, legendaryTop, legendaryBottom; 

    bool play1Selected = true;
    bool play2Selected = false;
    bool hasGiven = false;

    int numOfChar = 18;

    GameObject p1Heart;
    GameObject p2Heart;

    GameObject p1Acorn;
    GameObject p2Acorn;

    float animLength = 0.6f;
    bool heartPump = false;

    float acornOpenLength = 1.6f;
    float acornFallLength = 1.0f;

    bool acornOpen = false;
    bool acornFalling = false;

    List<int> player1Types;
    List<int> player2Types;

    List<Sprite> player1Sprites;
    List<Sprite> player2Sprites;

    #region Sprite containers
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

    Sprite randChar16Sprite;
    Sprite randChar17Sprite;
    Sprite randChar18Sprite;
    #endregion

    // Use this for initialization
    void Start()
    {
        player1Types = new List<int>();
        player2Types = new List<int>();

        player1Sprites = new List<Sprite>();
        player2Sprites = new List<Sprite>();

        p1Heart = GameObject.Find("Player1Panel").transform.GetChild(1).gameObject;
        p2Heart = GameObject.Find("Player2Panel").transform.GetChild(1).gameObject;

        p1Acorn = GameObject.Find("Player1Panel").transform.GetChild(0).gameObject;
        p2Acorn = GameObject.Find("Player2Panel").transform.GetChild(0).gameObject;

        // setting all the sprites
        {
            //Assets / Resources / Masks / HammerWhite.png for each path
            randChar1Sprite = Resources.Load("Masks/HammerWhite", typeof(Sprite)) as Sprite;
            randChar2Sprite = Resources.Load("Masks/WhiteBull", typeof(Sprite)) as Sprite;
            randChar3Sprite = Resources.Load("Masks/WhitePShooter", typeof(Sprite)) as Sprite;
            randChar4Sprite = Resources.Load("Masks/WhiteSpear", typeof(Sprite)) as Sprite;
            randChar5Sprite = Resources.Load("Masks/WhiteMines", typeof(Sprite)) as Sprite;

            randChar6Sprite = Resources.Load("Masks/WhiteTelefrag", typeof(Sprite)) as Sprite;
            randChar7Sprite = Resources.Load("Masks/WhitePlank", typeof(Sprite)) as Sprite;
            randChar8Sprite = Resources.Load("Masks/WhiteBig", typeof(Sprite)) as Sprite;
            randChar9Sprite = Resources.Load("Masks/WhiteIce", typeof(Sprite)) as Sprite;
            randChar10Sprite = Resources.Load("Masks/WhiteMagnet", typeof(Sprite)) as Sprite;

            randChar11Sprite = Resources.Load("Masks/WhiteSlow", typeof(Sprite)) as Sprite;
            randChar12Sprite = Resources.Load("Masks/WhiteMace", typeof(Sprite)) as Sprite;
            randChar13Sprite = Resources.Load("Masks/RedZekeNLuther", typeof(Sprite)) as Sprite; // zeke and luther
            randChar14Sprite = Resources.Load("Masks/WhiteHow2", typeof(Sprite)) as Sprite;
            randChar15Sprite = Resources.Load("Masks/WhiteFox", typeof(Sprite)) as Sprite;
            randChar16Sprite = Resources.Load("Masks/WhiteMoon", typeof(Sprite)) as Sprite;
            randChar17Sprite = Resources.Load("Masks/BlueJoe", typeof(Sprite)) as Sprite; // Joe Siehl
            randChar18Sprite = Resources.Load("Masks/RedJoe", typeof(Sprite)) as Sprite; // Joe Siehl
        }

        // check for null sprites
        {
            /*
               Debug.Log(randChar1Sprite);
               Debug.Log(randChar2Sprite);
               Debug.Log(randChar3Sprite);
               Debug.Log(randChar4Sprite);
               Debug.Log(randChar5Sprite);
               Debug.Log(randChar6Sprite);
               Debug.Log(randChar7Sprite);
               Debug.Log(randChar8Sprite);
               Debug.Log(randChar9Sprite);
               Debug.Log(randChar10Sprite);
               Debug.Log(randChar11Sprite);
               Debug.Log(randChar12Sprite);
               Debug.Log(randChar13Sprite);
               Debug.Log(randChar14Sprite);
               Debug.Log(randChar15Sprite);
               Debug.Log(randChar16Sprite);
               Debug.Log(randChar17Sprite);
               Debug.Log(randChar18Sprite);
               */
        }
    }

    // Update is called once per frame
    void Update ()
    {
        CheckForFullData();

        SetHeartAnimation();
    }

    void CheckForFullData()
    {
        if (player1Types.Count == 3 && player2Types.Count == 3)
        {
            if(!hasGiven)
            {
                //Debug.Log("giving player data to manager");
                InfoHolder.getInstance().SetPlayerInfo(1, player1Types, player1Sprites);
                InfoHolder.getInstance().SetPlayerInfo(2, player2Types, player2Sprites);
                hasGiven = true;
            }
        }
    }

    void SetHeartAnimation()
    {
        if(play1Selected)
        {
            p1Heart.GetComponent<Animator>().SetBool("NewCharacter", heartPump);
        }
        else
        {
            p2Heart.GetComponent<Animator>().SetBool("NewCharacter", heartPump);
        }
    }

    void SwitchPlayers()
    {
        //Debug.Log("switching players");
        if (play1Selected)
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
        int randomPool = Random.Range(1, 100);

        if (randomPool > 0 && randomPool < 50)
        {
            Debug.Log("Common Role");

            p1AcornTop.GetComponent<Image>().sprite = commonTop;
            p1AcornBottom.GetComponent<Image>().sprite = commonBottom;
            p2AcornTop.GetComponent<Image>().sprite = commonTop;
            p2AcornBottom.GetComponent<Image>().sprite = commonBottom;

            List<int> playerTypes = new List<int>();
            playerTypes.Add((int)PLAYER_TYPE.HOW_TO);
            playerTypes.Add((int)PLAYER_TYPE.SPIN);
            playerTypes.Add((int)PLAYER_TYPE.BULL_RUSH);
            playerTypes.Add((int)PLAYER_TYPE.PEASHOOTER);
            playerTypes.Add((int)PLAYER_TYPE.SMALL_KNIFE);
            playerTypes.Add((int)PLAYER_TYPE.MINER);

            int randMember = Random.Range(0, playerTypes.Count - 1);
            newRand = playerTypes[randMember];
        }
        else if (randomPool >= 50 && randomPool < 85)
        {
            Debug.Log("Uncommon Role");

            p1AcornTop.GetComponent<Image>().sprite = uncommonTop;
            p1AcornBottom.GetComponent<Image>().sprite = uncommonBottom;
            p2AcornTop.GetComponent<Image>().sprite = uncommonTop;
            p2AcornBottom.GetComponent<Image>().sprite = uncommonBottom;

            List<int> playerTypes = new List<int>();
            playerTypes.Add((int)PLAYER_TYPE.SPEAR);
            playerTypes.Add((int)PLAYER_TYPE.SLIME);
            playerTypes.Add((int)PLAYER_TYPE.TELEFRAG);
            playerTypes.Add((int)PLAYER_TYPE.MOON);
            playerTypes.Add((int)PLAYER_TYPE.ICE_DA_ICEMANE);
            playerTypes.Add((int)PLAYER_TYPE.BIG_POLE);

            int randMember = Random.Range(0, playerTypes.Count - 1);
            newRand = playerTypes[randMember];
        }
        else if(randomPool >= 85 && randomPool <= 99)
        {
            Debug.Log("Rare Role");
            p1AcornTop.GetComponent<Image>().sprite = rareTop;
            p1AcornBottom.GetComponent<Image>().sprite = rareBottom;
            p2AcornTop.GetComponent<Image>().sprite = rareTop;
            p2AcornBottom.GetComponent<Image>().sprite = rareBottom;

            p1AcornBottom.GetComponent<Animator>().enabled = true;
            p2AcornBottom.GetComponent<Animator>().enabled = true;

            List<int> playerTypes = new List<int>();
            playerTypes.Add((int)PLAYER_TYPE.MAGNET);
            playerTypes.Add((int)PLAYER_TYPE.FLAIL);
            playerTypes.Add((int)PLAYER_TYPE.ZEKE_AND_LUTHER);

            int randMember = Random.Range(0, playerTypes.Count - 1);
            newRand = playerTypes[randMember];
        }
        else 
        {
            Debug.Log("Legendary Role");
            p1AcornTop.GetComponent<Image>().sprite = legendaryTop;
            p1AcornBottom.GetComponent<Image>().sprite = legendaryBottom;
            p2AcornTop.GetComponent<Image>().sprite = legendaryTop;
            p2AcornBottom.GetComponent<Image>().sprite = legendaryBottom;

            List<int> playerTypes = new List<int>();
            playerTypes.Add((int)PLAYER_TYPE.JOE_SIEHL);
            newRand = playerTypes[0];
        }

        if (heartPump == false && acornOpen == false)
        {
            if (play1Selected && player1Types.Count < 3)
            {
                heartPump = true;
                acornOpen = true;
                player1Types.Add(newRand);

                // will do heart anim then acorn anim then it sets teh player
                StartCoroutine(WaitForAnimation(newRand));
                
            }
            else if (play2Selected && player2Types.Count < 3)
            {

                heartPump = true;
                acornOpen = true;
                player2Types.Add(newRand);

                // will do heart anim then acorn anim then it sets teh player
                StartCoroutine(WaitForAnimation(newRand));
            }
        }
    }

    void MakeImageChar(int currentImage, int charVal)
    {
        string player;
        string playerCharacter;
        GameObject charImage;

        if (play1Selected)
        {
            player = "P1";
        }
        else
        {
            player = "P2";
        }

        playerCharacter = "Char" + currentImage;

        charImage = GameObject.Find(player + playerCharacter);

        // this is setting the charImage and adding the sprite to the list
        {
            if (charVal == 1)
            {
                charImage.GetComponent<Image>().sprite = randChar1Sprite;
                AddSprite(randChar1Sprite);
            }
            else if (charVal == 2)
            {
                charImage.GetComponent<Image>().sprite = randChar2Sprite;
                AddSprite(randChar2Sprite);
            }
            else if (charVal == 3)
            {
                charImage.GetComponent<Image>().sprite = randChar3Sprite;
                AddSprite(randChar3Sprite);
            }
            else if (charVal == 4)
            {
                charImage.GetComponent<Image>().sprite = randChar4Sprite;
                AddSprite(randChar4Sprite);
            }
            else if (charVal == 5)
            {
                charImage.GetComponent<Image>().sprite = randChar5Sprite;
                AddSprite(randChar5Sprite);
            }
            else if (charVal == 6)
            {
                charImage.GetComponent<Image>().sprite = randChar6Sprite;
                AddSprite(randChar6Sprite);
            }
            else if (charVal == 7)
            {
                charImage.GetComponent<Image>().sprite = randChar7Sprite;
                AddSprite(randChar7Sprite);
            }
            else if (charVal == 8)
            {
                charImage.GetComponent<Image>().sprite = randChar8Sprite;
                AddSprite(randChar8Sprite);
            }
            else if (charVal == 9)
            {
                charImage.GetComponent<Image>().sprite = randChar9Sprite;
                AddSprite(randChar9Sprite);
            }
            else if (charVal == 10)
            {
                charImage.GetComponent<Image>().sprite = randChar10Sprite;
                AddSprite(randChar10Sprite);
            }
            else if (charVal == 11)
            {
                charImage.GetComponent<Image>().sprite = randChar11Sprite;
                AddSprite(randChar13Sprite);
            }
            else if (charVal == 12)
            {
                charImage.GetComponent<Image>().sprite = randChar12Sprite;
                AddSprite(randChar12Sprite);
            }
            else if (charVal == 13)
            {
                charImage.GetComponent<Image>().sprite = randChar13Sprite;
                AddSprite(randChar13Sprite);
            }
            else if (charVal == 14)
            {
                charImage.GetComponent<Image>().sprite = randChar14Sprite;
                AddSprite(randChar14Sprite);
            }
            else if (charVal == 15)
            {
                charImage.GetComponent<Image>().sprite = randChar15Sprite;
                AddSprite(randChar15Sprite);
            }
            else if (charVal == 16)
            {
                charImage.GetComponent<Image>().sprite = randChar16Sprite;
                AddSprite(randChar16Sprite);
            }
            else if (charVal == 17)
            {
                charImage.GetComponent<Image>().sprite = randChar17Sprite;
                AddSprite(randChar17Sprite);
            }
            else if (charVal == 18)
            {
                charImage.GetComponent<Image>().sprite = randChar18Sprite;
                AddSprite(randChar18Sprite);
            }
        }

        //Debug.Log(charImage.name + " value: " + charVal);

        //Debug.Log(charImage.name + " sprite: " + charImage.GetComponent<Image>().sprite);
    }

    void AddSprite(Sprite newSprite)
    {
        if (play1Selected)
        {
            player1Sprites.Add(newSprite);
        }
        else
        {
            player2Sprites.Add(newSprite);
        }

    }

    IEnumerator WaitForAnimation(int randNum)
    {
        yield return new WaitForSeconds(animLength);

        heartPump = false;

        if(play1Selected)
        {
            p1Acorn.GetComponent<Animator>().SetBool("IsFalling", true);
            p1Acorn.GetComponent<Animator>().SetBool("IsOpening", true);
        }
        else
        {
            p2Acorn.GetComponent<Animator>().SetBool("IsFalling", true);
            p2Acorn.GetComponent<Animator>().SetBool("IsOpening", true);
        }

        StartCoroutine(WaitForAcorn(randNum));
    }

    IEnumerator WaitForAcorn(int randNum)
    {
        acornFalling = true;

        yield return new WaitForSeconds(acornFallLength);
        
        if (play1Selected)
        {
            p1Acorn.GetComponent<Animator>().SetBool("IsFalling", false);
        }
        else
        {
            p2Acorn.GetComponent<Animator>().SetBool("IsFalling", false);
        }

        acornFalling = false;
        acornOpen = true;

        yield return new WaitForSeconds(acornOpenLength);

        Debug.Log("acorn is done");

        acornOpen = false;


        if (play1Selected)
        {
            p1Acorn.GetComponent<Animator>().SetBool("IsOpening", false);
        }
        else
        {
            p1Acorn.GetComponent<Animator>().SetBool("IsOpening", false);
        }

        if (play1Selected)// && player1Types.Count < 3)
        {
            MakeImageChar(player1Types.Count, randNum);
            SwitchPlayers();
        }
        else if (play2Selected)// && player2Types.Count < 3)
        {
            MakeImageChar(player2Types.Count, randNum);
            SwitchPlayers();
        }
    }
}
