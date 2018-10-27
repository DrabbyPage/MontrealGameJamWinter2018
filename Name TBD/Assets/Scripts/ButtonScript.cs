using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScript : MonoBehaviour
{
    [SerializeField]
    public string buttonType = "SceneButton";
	// Use this for initialization
	void Start ()
    {
	    
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void MakeTextOnButton()
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

    public void SceneButton()
    {
        GameObject.Find("SceneManager").GetComponent<SceneManagerScript>().LoadScene(gameObject.name);
    }
}
