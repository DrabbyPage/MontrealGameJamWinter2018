using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArenaFall : MonoBehaviour {

    Sprite originalSprite;
    SpriteRenderer rend;

    private void Awake()
    {
        rend = gameObject.GetComponent<SpriteRenderer>();
        originalSprite = rend.sprite;
    }

    public void Reset()
    {
        if (gameObject.GetComponent<Animator>() != null)
        {
            gameObject.GetComponent<Animator>().SetBool("shouldFlash", false);
            rend.sprite = originalSprite;

        }
    }

    public void Fall()
    {
        gameObject.GetComponent<Animator>().SetBool("shouldFlash", true);
    }
}
