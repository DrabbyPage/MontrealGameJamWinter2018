using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void HighlightButton()
    {
        GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 0.0f, 0.0f, 1.0f);
    }

    public void UnhighlightButton()
    {
        GetComponent<SpriteRenderer>().color = new Vector4(1.0f, 1.0f, 1.0f, 1.0f);
    }

    public void ButtonAct()
    {
        GameObject.Find("SceneManager").GetComponent<SceneManagerScript>().LoadScene(gameObject.name);
    }
}
