using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameManager : MonoBehaviour {

    static GameManager instance;

    public static GameManager getInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<GameManager>();

            if (instance == null)
            {
                GameObject tmp = new GameObject("TmpManager");
                instance = tmp.AddComponent<GameManager>();
            }
        }

        return instance;
    }

    #region StructDataHolders
    [System.Serializable]
    public struct PlayerData
    {
        public float deathDelay;
        public GameObject player1, player2;
        public Transform player1Spawn, player2Spawn;
        public bool player1Dead, player2Dead;
        public int player1Wins, player2Wins;
    };

    [System.Serializable]
    public struct MatchData
    {
        public GameObject roundPanel, matchPanel;
        public Text roundText, countDownText, matchWinText;
        public Image[] p1WinMarks, p2WinMarks;
    }

    [System.Serializable]
    public struct PlayerInfo
    {
        public List<int> player1Indexes;
        public List<Sprite> player1Sprites;

        public List<int> player2Indexes;
        public List<Sprite> player2Sprites;
    };
    #endregion

    [SerializeField]
    PlayerData playerData;
    [SerializeField]
    MatchData matchData;

    int p1DeadCounter=0, p2DeadCounter=0;
    float countDown;

    bool roundStart = true, newRound, matchOver, updateArena;
    bool checkArenaState = true;
    public bool newRoundStarted = false;
    string winText = "";

    GameObject currentEdge;
    bool arenaEdgeFell;

    const int NUM_TO_WIN = 3;

    //SceneManagerScript mSceneManager;

    bool containerFilled = false;

    [SerializeField]
    PlayerInfo infoContainer;

    private void Awake()
    {
        infoContainer.player1Indexes = new List<int>();
        infoContainer.player2Indexes = new List<int>();

        infoContainer.player1Sprites = new List<Sprite>();
        infoContainer.player2Sprites = new List<Sprite>();

       //mSceneManager = gameObject.GetComponent<SceneManagerScript>();
    }

    void Start () {
        if(playerData.player1 !=null)
        {
            //Debug.Log(playerData.player1.name);
        }
        if (playerData.player2 != null)
        {
            //Debug.Log(playerData.player2.name);
        }
        countDown = 4;
	}

    // Update is called once per frame
    void Update()
    {
        if (!containerFilled)
            SetCharValue();
        else
            SetPlayerValues();

        ArenaUpdate();
        RoundUpdate();

    }

    void SetCharValue()
    {
        infoContainer.player1Indexes = InfoHolder.getInstance().holdersHolder.player1Indexes;
        infoContainer.player2Indexes = InfoHolder.getInstance().holdersHolder.player1Indexes;

        infoContainer.player1Sprites = InfoHolder.getInstance().holdersHolder.player1Sprites;
        infoContainer.player2Sprites = InfoHolder.getInstance().holdersHolder.player2Sprites;

        float p1IndexCount = infoContainer.player1Indexes.Count;
        float p2IndexCount = infoContainer.player2Indexes.Count;
        float p1SpriteCount = infoContainer.player1Sprites.Count;
        float p2SpriteCount = infoContainer.player2Sprites.Count;

        if(p1IndexCount == 3 && p1SpriteCount == 3 && p1SpriteCount == 3 && p2SpriteCount == 3)
        {
            containerFilled = true;
        }
    }

    void SetPlayerValues()
    {
        // setting the type for the players
        //Debug.Log(infoContainer.player1Indexes.Count);
        //Debug.Log(infoContainer.player1Sprites.Count);
        if (playerData.player1 != null)
        {
            playerData.player1.GetComponent<TheoryMove>().SetPlayerType(InfoHolder.getInstance().holdersHolder.player1Indexes[p1DeadCounter]);
            playerData.player1.GetComponent<SpriteRenderer>().sprite = InfoHolder.getInstance().holdersHolder.player1Sprites[p1DeadCounter];
        }
        if (playerData.player1 != null)
        {
            playerData.player2.GetComponent<TheoryMove>().SetPlayerType(InfoHolder.getInstance().holdersHolder.player2Indexes[p2DeadCounter]);
            playerData.player2.GetComponent<SpriteRenderer>().sprite = InfoHolder.getInstance().holdersHolder.player2Sprites[p2DeadCounter];
        }
    }

    #region Round Code
    void RoundUpdate()
    {
        RoundStart();

        CheckForWinner();

        if(newRound)
        {
            DisplayRoundWinner(winText);
        }

        if (matchOver)
            DisplayMatchWinner();
    }

    void RoundStart()
    {
        if (roundStart)
        {
            newRound = false;
            playerData.player1.GetComponent<TheoryMove>().enabled = false;
            playerData.player2.GetComponent<TheoryMove>().enabled = false;

            matchData.roundPanel.SetActive(true);
            matchData.roundText.text = "ROUND START";
            matchData.countDownText.text = countDown.ToString();

            countDown -= Time.deltaTime;
            if (countDown > 0.0f)
            {
                if (Mathf.FloorToInt(countDown) == 0)
                {
                    newRoundStarted = true;
                    matchData.roundText.text = "";
                    matchData.countDownText.text = "FIGHT";
                }
                else
                    matchData.countDownText.text = Mathf.FloorToInt(countDown).ToString();
            }
            else
            {
                roundStart = false;
                checkArenaState = true;
                updateArena = true;
                matchData.roundPanel.SetActive(false);

                playerData.player1.GetComponent<TheoryMove>().enabled = true;
                playerData.player2.GetComponent<TheoryMove>().enabled = true;

            }
        }

    }

    void DisplayMatchWinner()
    {
        playerData.player1.GetComponent<TheoryMove>().enabled = false;
        playerData.player2.GetComponent<TheoryMove>().enabled = false;

        if (playerData.player2Wins > playerData.player1Wins)
            winText = "PLAYER 2 WINS";
        else
            winText = "PLAYER 1 WINS";

        matchData.roundPanel.SetActive(false);
        matchData.matchPanel.SetActive(true);

        matchData.matchWinText.text = winText;
    }

    void DisplayRoundWinner(string winText)
    {   
        playerData.player1.GetComponent<TheoryMove>().enabled = false;
        playerData.player2.GetComponent<TheoryMove>().enabled = false;

        if (playerData.player2Wins == NUM_TO_WIN) // player 2 wins match
        {
            matchOver = true;
        }
        else if (playerData.player1Wins == NUM_TO_WIN)
        {
            matchOver = true;
        }
        else
        {
            matchOver = false;

            matchData.roundPanel.SetActive(true);
            matchData.roundText.text = winText;
            matchData.countDownText.text = "";

            //Reset players
            playerData.player1.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            playerData.player2.GetComponent<Rigidbody2D>().velocity = Vector3.zero;
            playerData.player1.transform.position = playerData.player1Spawn.position;
            playerData.player2.transform.position = playerData.player2Spawn.position;

            countDown -= Time.deltaTime;
            if (countDown <= 0.0f)
            {
                countDown = 4;
                roundStart = true;
            }
        }
    }

    void CheckForWinner()
    {
        if (!checkArenaState)
        {
            if (playerData.player1Dead || playerData.player2Dead)
            {
                if (playerData.player1Dead)
                {
                    newRoundStarted = false;

                    Debug.Log("P2 win");
                    playerData.player2Wins++;
                    matchData.p2WinMarks[playerData.player2Wins - 1].color = Color.red;
                    //p1DeadCounter++;
                    winText = "PLAYER 2 WINS ROUND";
                }
                else if (playerData.player2Dead)
                {
                    newRoundStarted = false;

                    Debug.Log("P1 win");
                    playerData.player1Wins++;
                    matchData.p2WinMarks[playerData.player1Wins - 1].color = Color.red;
                    //p2DeadCounter++;
                    winText = "PLAYER 1 WINS ROUND";
                }
                else if(playerData.player1Dead && playerData.player2Dead)
                {
                    newRoundStarted = false;

                    winText = "DRAW";
                    Debug.Log("DRAW");
                }

                playerData.player1Dead = false;
                playerData.player2Dead = false;
                //p1DeadCounter = 0;
                //p2DeadCounter = 0;


                newRound = true;
                matchData.roundPanel.SetActive(false);
                countDown = 1.5f;
            }
        }
    }
    #endregion

    #region ArenaCode
    private void ArenaUpdate()
    {
        if (checkArenaState)
        {
            //update arena bounds
            if (arenaEdgeFell)
            {
                currentEdge = GetCurrentEdge();
                arenaEdgeFell = false;
            }

            //Check if player is still in arena
            if (!InsideArena(playerData.player1))
            {
                //Debug.Log("u gon di");
                playerData.player1Dead = true;
                p1DeadCounter++;
                checkArenaState = false;
       //         StartCoroutine(TurnAroundAndDie(playerData.deathDelay, playerData.player1));
            }
            if (!InsideArena(playerData.player2))
            {
                checkArenaState = false;
                //Debug.Log("u gon di2");
                checkArenaState = false;
                playerData.player2Dead = true;
                p2DeadCounter++;
               // StartCoroutine(TurnAroundAndDie(playerData.deathDelay, playerData.player2));
            }
        }
    }

    private bool InsideArena(GameObject player)
    {
        if(player != null)
        {
            Vector3 playerPos = player.transform.position;
            Vector3 newPos = new Vector3(playerPos.x, playerPos.y, currentEdge.transform.position.z);

            bool withinBounds = currentEdge.GetComponent<CircleCollider2D>().bounds.Contains(newPos);
            return withinBounds;
        }
        else
        {
            return true;// only temperary
        }

    }

    private IEnumerator TurnAroundAndDie(float waitTime, GameObject player)
    {
       yield return new WaitForSeconds(waitTime);
       // player.GetComponent<Rigidbody2D>().AddForce(-player.transform.up * 1000.0f);
    }
    #endregion

    #region Getters&Setters
    public bool GetArenaFell()
    {
        return arenaEdgeFell;
    }
    public void SetArenaFell(bool hasFallen)
    {
        arenaEdgeFell = hasFallen;
    }

    public void SetCurrentEdge(GameObject obj)
    {
        currentEdge = obj;
    }

    public GameObject GetCurrentEdge()
    {
        return currentEdge;
    }
    public bool IsNewRound()
    {
        return updateArena;
    }

    public void ArenaNeedsUpdate(bool hasUpdated)
    {
        updateArena = hasUpdated;
    }

    public void SetPlayerInfo(int playerNum, List<int> newIndexes, List<Sprite> newSprites)
    {
        if (playerNum == 1)
        {
            infoContainer.player1Indexes = newIndexes;
            infoContainer.player1Sprites = newSprites;
        }
        else
        {
            infoContainer.player2Indexes = newIndexes;
            infoContainer.player2Sprites = newSprites;
        }
    }

    public void ResetPlayerInfo()
    {
        infoContainer.player1Sprites.Clear();
        infoContainer.player2Sprites.Clear();

        infoContainer.player1Indexes.Clear();
        infoContainer.player2Indexes.Clear();

    }

    public Sprite GetPlayersSprite(int playerNum)
    {
        //Debug.Log("P1 deaths: " + p1DeadCounter);
        //Debug.Log("P2 deaths: " + p2DeadCounter);

        if (playerNum == 1)
        {
            return InfoHolder.getInstance().holdersHolder.player1Sprites[p1DeadCounter];
        }
        else if(playerNum == 2)
        {
            return InfoHolder.getInstance().holdersHolder.player2Sprites[p2DeadCounter];
        }
        else
        {
            return null;
        }
    }

    #endregion
}
