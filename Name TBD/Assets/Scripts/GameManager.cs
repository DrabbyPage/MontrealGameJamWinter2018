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
    #endregion

    [SerializeField]
    PlayerData playerData;
    [SerializeField]
    MatchData matchData;

    int p1DeadCounter, p2DeadCounter;
    float countDown;

    bool roundOver = false, roundStart = true, newRound, matchOver, updateArena;
    bool checkArenaState = true;
    string winText = "";

    GameObject currentEdge;
    bool arenaEdgeFell;

    const int NUM_TO_WIN = 3;

    SceneManagerScript mSceneManager;

    private void Awake()
    {
       mSceneManager = gameObject.GetComponent<SceneManagerScript>();
    }

    void Start () {
        countDown = 4;
	}

    // Update is called once per frame
    void Update()
    {
            RoundUpdate();
            ArenaUpdate();
    }

    #region Round Code
    void RoundUpdate()
    {
        RoundStart();

        if (playerData.player1Dead)
        {
            p1DeadCounter++;
        }
        if (playerData.player2Dead)
        {
            p2DeadCounter++;
        }

        CheckForWinner();

        if(newRound)
            DisplayRoundWinner(winText);

        if (matchOver)
            DisplayMatchWinner();
    }

    void RoundStart()
    {
        if (roundStart)
        {
            newRound = false;
            playerData.player1.GetComponent<PlayerMovementScript>().enabled = false;
            playerData.player2.GetComponent<PlayerMovementScript>().enabled = false;

            matchData.roundPanel.SetActive(true);
            matchData.roundText.text = "ROUND START";
            matchData.countDownText.text = countDown.ToString();

            countDown -= Time.deltaTime;
            if (countDown > 0.0f)
            {
                if (Mathf.FloorToInt(countDown) == 0)
                {
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

                playerData.player1.GetComponent<PlayerMovementScript>().enabled = true;
                playerData.player2.GetComponent<PlayerMovementScript>().enabled = true;

            }
        }

    }

    void DisplayMatchWinner()
    {
        playerData.player1.GetComponent<PlayerMovementScript>().enabled = false;
        playerData.player2.GetComponent<PlayerMovementScript>().enabled = false;

        winText = "PLAYER 2 WINS";
        matchData.roundPanel.SetActive(false);
        matchData.matchPanel.SetActive(true);

        matchData.matchWinText.text = winText;
    }

    void DisplayRoundWinner(string winText)
    {
        Debug.Log("Display mid round text");
        
        playerData.player1.GetComponent<PlayerMovementScript>().enabled = false;
        playerData.player2.GetComponent<PlayerMovementScript>().enabled = false;

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
                if (p1DeadCounter < p2DeadCounter)
                {
                    Debug.Log("P2 win");
                    playerData.player2Wins++;
                    matchData.p2WinMarks[playerData.player2Wins - 1].color = Color.red;
                    winText = "PLAYER 2 WINS ROUND";
                }
                else if (p1DeadCounter > p2DeadCounter)
                {
                    Debug.Log("P1 win");
                    playerData.player1Wins++;
                    matchData.p2WinMarks[playerData.player1Wins - 1].color = Color.red;
                    winText = "PLAYER 1 WINS ROUND";
                }
                else
                {
                    winText = "DRAW";
                    Debug.Log("DRAW");
                }

                playerData.player1Dead = false;
                playerData.player2Dead = false;
                p1DeadCounter = 0;
                p2DeadCounter = 0;


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
                Debug.Log("u gon di");
                playerData.player2Dead = true;
                checkArenaState = false;
       //         StartCoroutine(TurnAroundAndDie(playerData.deathDelay, playerData.player1));
            }
            if (!InsideArena(playerData.player2))
            {
                checkArenaState = false;
                Debug.Log("u gon di2");
                checkArenaState = false;
                playerData.player1Dead = true;
               // StartCoroutine(TurnAroundAndDie(playerData.deathDelay, playerData.player2));
            }
        }
    }

    private bool InsideArena(GameObject player)
    {
        return currentEdge.GetComponent<CircleCollider2D>().bounds.Contains(new Vector3(player.transform.position.x, player.transform.position.y, currentEdge.transform.position.z));
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
    #endregion
}
