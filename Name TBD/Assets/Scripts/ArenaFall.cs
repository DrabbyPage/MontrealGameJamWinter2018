using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaFall : MonoBehaviour {

    public void Fall()
    {
        gameObject.GetComponent<Animator>().SetBool("shouldFlash", true);
    }
}
