using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {

    public AudioClip ReadyClip;
    public AudioClip CoolDownClip;

    public AudioSource ASause;


	// Use this for initialization
	void Start () {
        //ASause.clip = ReadyClip;
	}
	
	// Update is called once per frame
	void Update () {
        TestFromBull();
	}

    public void TestFromBull()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //load audio clip into the source
            // basically the tape in the player
            ASause.clip = ReadyClip;
            //this actually players whatever clip is loaded into the source
            ASause.Play();
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ASause.clip = CoolDownClip;
            ASause.Play();
        }

    }
}
