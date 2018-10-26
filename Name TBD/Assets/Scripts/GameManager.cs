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

    GameObject currentEdge;
    bool arenaEdgeFell;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}


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
