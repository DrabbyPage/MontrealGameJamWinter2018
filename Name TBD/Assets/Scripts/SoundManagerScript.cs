using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {
    public static SoundManagerScript instance;

    [Header("Audio clips")]
    public AudioClip ReadyClip;
    public AudioClip CoolDownClip;
    public AudioClip Moo;
    public AudioClip MenuSelect;
    public AudioClip MenuHover;

    public AudioClip Dash0Snd;
    public AudioClip Explosion;
    public AudioClip GotchaGetsBuildUp;
    public AudioClip GotchaGetsCommon;
    public AudioClip GotchaGetsRare;

    public AudioClip GotchaGetsUncommon;
    public AudioClip GotchaGetsUncommon2;
    public AudioClip HeartBeat;
    public AudioClip Hit_snd;
    public AudioClip SpearLand;
    public AudioClip SpearThrow;
    public AudioClip TreeFight;


    public AudioSource ASause;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start () {
        ASause = gameObject.GetComponent<AudioSource>();
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

    public void PlayMenuSelect()
    {
        ASause.clip = MenuSelect;
        //this actually players whatever clip is loaded into the source
        ASause.Play();
    }

    public void PlayMenuHover()
    {
        ASause.clip = MenuHover;
        //this actually players whatever clip is loaded into the source
        ASause.Play();
    }

}
