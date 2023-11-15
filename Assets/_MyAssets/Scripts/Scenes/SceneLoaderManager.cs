using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderManager : MonoBehaviour
{   
    public static readonly string[] LIST_SCENES = new string[] {"StartScene", "Niveau", "EndScene"};

    private static SceneLoaderManager _instance;
    
    public static SceneLoaderManager Instance => _instance;

    private void Awake()
    {
        if (_instance != null)
        {
            Destroy(gameObject);
            return;
        }
        _instance = this;
    }




    public void LoadScene(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

}
