using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {
    public static SoundManagerScript instance;

    [Header("Audio clips")]
    public AudioClip ReadyClip;
    public AudioClip CoolDownClip;
    public AudioClip Moo;


    public AudioSource ASause;


	// Use this for initialization
	void Start () {
        //ASause.clip = ReadyClip;
        instance = this;
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
            
        }
        if (Input.GetKeyDown(KeyCode.Q))
        {
            ASause.clip = CoolDownClip;
            ASause.Play();
        }

    }

    public void EndCoolDownSound()
    {
        // basically the tape in the player
        ASause.clip = ReadyClip;
        //this actually players whatever clip is loaded into the source
        ASause.Play();
    }

    public void MooSound()
    {
        // basically the tape in the player
        ASause.clip = Moo;
        //this actually players whatever clip is loaded into the source
        ASause.Play();
    }

}
