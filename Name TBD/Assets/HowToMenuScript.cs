using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToMenuScript : MonoBehaviour
{
    int panel = 0;

    GameObject panel1;
    GameObject panel2;
    GameObject panel3;

	// Use this for initialization
	void Start ()
    {
        panel1 = GameObject.Find("HowToImage1");
        panel2 = GameObject.Find("HowToImage2");
        panel3 = GameObject.Find("HowToImage3");

    }

    // Update is called once per frame
    void Update ()
    {
        UpdatePanels();	
	}

    void UpdatePanels()
    {
        if(panel == 0)
        {
            panel1.SetActive(true);
            panel2.SetActive(false);
            panel3.SetActive(false);

        }
        if (panel == 1)
        {
            panel1.SetActive(false);
            panel2.SetActive(true);
            panel3.SetActive(false);

        }
        if (panel == 2)
        {
            panel1.SetActive(false);
            panel2.SetActive(false);
            panel3.SetActive(true);

        }
    }

    public void PanelLeft()
    {
        if(panel > 0)
        {
            panel = panel - 1;
        }
        else
        {
            SceneManagerScript.getInstance().LoadScene("MainMenu");
        }
    }

    public void PanelRight()
    {
        if (panel < 2)
        {
            panel = panel + 1;
        }
        else
        {
            panel = 0;
        }
    }
}
