using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    [SerializeField]
    GameObject player1, player2;
    
    GameObject currentEdge;
    bool arenaEdgeFell;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ArenaUpdate();
	}

    #region ArenaCode
    private void ArenaUpdate()
    {
        //update arena bounds
        if (arenaEdgeFell)
        {
            currentEdge = GetCurrentEdge();
            arenaEdgeFell = false;
        }

        //Check if player is still in arena
        if (!InsideArena(player1))
        {
            //Debug.Log("DEAD");
            StartCoroutine(TurnAroundAndDie(1.0f, player1));
        }
        if (!InsideArena(player2))
        {
            //Debug.Log("DEAD");
            StartCoroutine(TurnAroundAndDie(1.0f, player2));
        }
    }

    private bool InsideArena(GameObject player)
    {
        return currentEdge.GetComponent<CircleCollider2D>().bounds.Contains(new Vector3(player.transform.position.x, player.transform.position.y, currentEdge.transform.position.z));
    }

    private IEnumerator TurnAroundAndDie(float waitTime, GameObject player)
    {
        yield return new WaitForSeconds(waitTime);
        player.GetComponent<Rigidbody2D>().AddForce(-player.transform.up * 1000.0f); //go die pls
    }
    #endregion

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
}
