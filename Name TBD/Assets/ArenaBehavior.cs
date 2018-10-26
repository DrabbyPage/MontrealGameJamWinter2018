using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaBehavior : MonoBehaviour {

    [SerializeField]
    GameObject[] arenaRings;
    Color[] arenaColors;
    bool[] dropList;

    Color aboutToDrop = Color.red;

    int ringToDrop;
    float timer;
    float dropTime = 5, flashTime;
    int timesToFlash = 3;

    bool shouldDrop, colorChange;

	void Start () {
        timer = 0;
        dropList = new bool[arenaRings.Length];
        ringToDrop = arenaRings.Length - 1;
	}
	
	void Update ()
    {
        if (arenaRings.Length > 1)
        {
            timer += Time.deltaTime;
            if (timer > dropTime) //timer for rings falling
            {
                timer = 0;
                dropList[ringToDrop] = true;
                --ringToDrop;
            }


            for(int i = arenaRings.Length; i > 0; i--) //begin to flash color
            {
                if(dropList[i])
                {
                    arenaColors[i] = arenaRings[i].GetComponent<SpriteRenderer>().color;
                    arenaRings[i].GetComponent<SpriteRenderer>().color = aboutToDrop;
                }
            }

            if(colorChange)
            {
                timer += Time.deltaTime;
                if(timer > flashTime)
                {
                    timesToFlash++;
                    arenaRings[i].GetComponent<SpriteRenderer>().color = aboutToDrop;

                }
            }
        }


	}
}
