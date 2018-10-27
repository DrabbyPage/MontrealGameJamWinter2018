using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaFall : MonoBehaviour {

//    [SerializeField]
  //  Color ringColor;

    public void Reset()
    {
        if(gameObject.GetComponent<Animator>() != null)
            gameObject.GetComponent<Animator>().SetBool("shouldFlash", false);
    }

    public void Fall()
    {
        gameObject.GetComponent<Animator>().SetBool("shouldFlash", true);
    }
}
