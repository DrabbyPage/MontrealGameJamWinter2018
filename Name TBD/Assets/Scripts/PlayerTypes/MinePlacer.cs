using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinePlacer : MonoBehaviour
{
    GameObject mineObj;

    [SerializeField]
    float boopForce = 150f;

    [SerializeField]
    float fireTime = 0.2f;
    float time;
    [SerializeField]
    bool canPlaceMines = true;

    [SerializeField]
    int allowedMines;
    int mineCounter = 0;

    [SerializeField]
    int cooldownTime = 30;
    int cooldownCounter = 0;

    // Use this for initialization
    void Start ()
    {
        mineObj = Resources.Load("Prefabs/Mine") as GameObject;
        time = fireTime;
    }

    // Update is called once per frame
    void Update ()
    {
        CheckPlacement();
        CheckCooldown();
	}

    public void PlaceMine()
    {
        GameObject newMine;

        if (canPlaceMines && mineCounter < allowedMines)
        {
            newMine = Instantiate(mineObj) as GameObject;

            newMine.transform.position = gameObject.transform.position;

            newMine.GetComponent<MinesScript>().SetParentObj(gameObject.transform.parent.gameObject);

            newMine.GetComponent<MinesScript>().SetBoopForce(boopForce);
            
            mineCounter++;
            // change player color grey to indicate cool down
            GetComponentInParent<SpriteRenderer>().color = Color.grey;
            canPlaceMines = false;
            
        }
    }

    void CheckPlacement()
    {
        if (!canPlaceMines)
        {
            if (time > 0)
            {
                time = time - Time.deltaTime;
                
                
            }
            else
            {
                time = fireTime;
                canPlaceMines = true;
                
            }
        }
    }

    // Determines whether or not the user can currently spin
    private void CheckCooldown()
    {
        if (mineCounter == allowedMines)
        {
            cooldownCounter++;
            // change player color grey to indicate cool down
            GetComponentInParent<SpriteRenderer>().color = Color.grey;

            // If the player's cooldown is at max, allow the player to act again
            if (cooldownCounter >= cooldownTime)
            {

                cooldownCounter = 0;
                mineCounter = 0;

                //set the skin color back to normal
                GetComponentInParent<SpriteRenderer>().color = Color.white;
                //plays the ready clip from the sound manager
                if (SoundManagerScript.instance != null)
                {
                    if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P1")
                    {
                        SoundManagerScript.instance.EndCoolDownSound(true);
                    }
                    if (gameObject.GetComponentInParent<TheoryMove>().currentPlayer == "P2")
                    {
                        SoundManagerScript.instance.EndCoolDownSound(false);
                    }


                }
            }
        }
    }
}
