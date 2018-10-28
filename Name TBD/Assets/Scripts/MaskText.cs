using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class MaskText : MonoBehaviour {

    public string maskName, flavorText;
    GameObject textHolder;

    // Use this for initialization
    void Start () {
        textHolder = GameObject.FindGameObjectWithTag("FlavorTextHolder");
	}
	
	public void DisplayFlavorText()
    {
        textHolder.GetComponent<Text>().text = maskName + "\n" + flavorText;
    }
}
