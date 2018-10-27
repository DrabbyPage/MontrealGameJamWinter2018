using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManagerScript : MonoBehaviour
{

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    // Use this for initialization
    void Start ()
    {

    }

    // Update is called once per frame
    void Update ()
    {

    }


    public void LoadScene(string sceneName)
    {
        if(sceneName != "QuitScene")
        {
            SceneManager.LoadScene(sceneName);
            GameObject.Find("ButtonManager").GetComponent<ButtonManagerScript>().CheckButtonList();
        }
        else
        {
            Application.Quit();
        }
    }
}
