using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {
    public static SoundManagerScript instance;

    [Header("AUDIO CLIPS")]

    [Header("character sounds")]

    [Header("universal sounds")]
    public AudioClip ReadyClip;
    public AudioClip CoolDownClip;
    public AudioClip Hit_snd;

    [Header("Peashooter")]
    public AudioClip PeaShoot;

    public AudioClip MenuSelect;
    public AudioClip MenuHover;

    public AudioClip Moo;

    public AudioClip Dash0Snd;
    public AudioClip Explosion;
    
    public AudioClip GotchaGetsCommon;
    public AudioClip GotchaGetsRare;

    public AudioClip GotchaGetsUncommon;
    public AudioClip GotchaGetsUncommon2;
    public AudioClip HeartBeat;

    public AudioClip SpearLand;
    public AudioClip SpearThrow;
    

    [Header("Music Clips")]
    public AudioClip TreeFightTheme;
    public AudioClip GotchaGetsBuildUp;


    [Header("RECORD PLAYERS")]
    [Header("player 1 specific record players")]
    public AudioSource ASause; //this is player 1's source
    public AudioSource Player1Walking;
    public AudioSource Player1Hits;

    [Header("player 2 specific record players")]
    public AudioSource Player2GeneralSource; 
    public AudioSource Player2Walking;
    public AudioSource Player2Hits;

    [Header("music record players")]
    public AudioSource MusicRecordPlayer;
    /*
    [Header("projectile players")]
    public AudioSource peaShootSnd;
    public AudioSource Spear;
    */



    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        ASause = gameObject.GetComponent<AudioSource>();
        MusicRecordPlayer.clip = TreeFightTheme;
        MusicRecordPlayer.Play();
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


    //*************** UNIVERSAL SOUNDS***************

    public void EndCoolDownSound()
    {
        // basically the tape in the player
        ASause.clip = ReadyClip;
        //this actually players whatever clip is loaded into the source
        ASause.Play();
    }

    //*************** BULL SOUNDS ********************

    public void MooSound(bool isPlayer1)
    {
        if (isPlayer1)
        {
            // basically the tape in the player
            ASause.clip = Moo;
            //this actually players whatever clip is loaded into the source
            ASause.Play();
        }
        if (!isPlayer1)
        {
            Player2GeneralSource.clip = Moo;
            Player2GeneralSource.Play();
        }
            

        /*
        // basically the tape in the player
        ASause.clip = Moo;
        //this actually players whatever clip is loaded into the source
        ASause.Play();*/
    }

    //*************** PEA SHOOTER SOUNDS ********************
    public void PeaShootSoundPlay(bool isPlayer1)
    {
        if (isPlayer1)
        {
            ASause.clip = PeaShoot;
            ASause.Play();
        }
        if (!isPlayer1)
        {
            Player2GeneralSource.clip = PeaShoot;
            Player2GeneralSource.Play();
        }
    }

    // this function determins which player is being hit and then which record player plays the sound
    public void PlayHitSound(bool isPlayer1)
    {
        if (isPlayer1)
        {
            Player1Hits.clip = Hit_snd;
            Player1Hits.Play();
        }
        if (!isPlayer1)
        {
            Player2Hits.clip = Hit_snd;
            Player2Hits.Play();
        }
    }

}
