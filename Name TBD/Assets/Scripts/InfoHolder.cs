using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoHolder : MonoBehaviour
{
    [System.Serializable]
    public struct PlayerInfo
    {
        public List<int> player1Indexes;
        public List<Sprite> player1Sprites;

        public List<int> player2Indexes;
        public List<Sprite> player2Sprites;
    };

    static InfoHolder instance;

    public static InfoHolder getInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<InfoHolder>();

            if (instance == null)
            {
                GameObject tmp = new GameObject("TmpManager");
                instance = tmp.AddComponent<InfoHolder>();
            }
        }

        return instance;
    }

    public PlayerInfo holdersHolder;

    private void Awake()
    {
        holdersHolder.player1Indexes = new List<int>();
        holdersHolder.player2Indexes = new List<int>();

        holdersHolder.player1Sprites = new List<Sprite>();
        holdersHolder.player2Sprites = new List<Sprite>();

        DontDestroyOnLoad(gameObject);
        //mSceneManager = gameObject.GetComponent<SceneManagerScript>();
    }

    // Use this for initialization
    void Start ()
    {

	}
	
	// Update is called once per frame
	void Update ()
    {
        GameObject GameMan = GameObject.Find("GameManager");

        if(GameMan != null)
        {
            GameManager.getInstance().SetPlayerInfo(1, holdersHolder.player1Indexes, holdersHolder.player1Sprites);
            GameManager.getInstance().SetPlayerInfo(2, holdersHolder.player2Indexes, holdersHolder.player2Sprites);
        }
    }

    public void SetPlayerInfo(int playerNum, List<int> newIndexes, List<Sprite> newSprites)
    {
        if (playerNum == 1)
        {
            holdersHolder.player1Indexes = newIndexes;
            holdersHolder.player1Sprites = newSprites;
        }
        else
        {
            holdersHolder.player2Indexes = newIndexes;
            holdersHolder.player2Sprites = newSprites;
        }
    }

    public void ResetPlayerInfo()
    {
        holdersHolder.player1Sprites.Clear();
        holdersHolder.player2Sprites.Clear();

        holdersHolder.player1Indexes.Clear();
        holdersHolder.player2Indexes.Clear();

    }

    public bool GetFilledList()
    {
        if (holdersHolder.player1Indexes.Count == 3 && holdersHolder.player2Indexes.Count == 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
