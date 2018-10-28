using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class SceneManagerScript : MonoBehaviour
{
    public static SceneManagerScript instance;

    public static SceneManagerScript getInstance()
    {
        if (instance == null)
        {
            instance = GameObject.FindObjectOfType<SceneManagerScript>();

            if (instance == null)
            {
                GameObject tmp = new GameObject("TmpManager");
                instance = tmp.AddComponent<SceneManagerScript>();
            }

        }

        return instance;
    }

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        if (instance != this)
        {
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        }
    }

    public string GetCurrentScene()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void CharSelect()
    {
        SceneManager.LoadScene("CharacterSelect");
    }

    public void ReturnToMain()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Debug.Log("quiting app");
        Application.Quit();
    }

    public void LoadScene(string sceneName)
    {
        if(sceneName == "Game")
        {
            if(InfoHolder.getInstance().GetFilledList())
            {
                SceneManager.LoadScene(sceneName);
            }
        }
        else if (sceneName != "QuitScene")
        {
            SceneManager.LoadScene(sceneName);
        }
        else
        {
            Application.Quit();
        }
    }
}
