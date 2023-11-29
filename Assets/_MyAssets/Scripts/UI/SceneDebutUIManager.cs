using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDebutUIManager : MonoBehaviour
{
    [SerializeField] private GameObject menuPrincipal = default;
    [SerializeField] private GameObject menuMultijoueur;
    //LIST_SCENES[1] = "Niveau"

    
         
    // Start is called before the first frame update
    void Start()
    {
        menuMultijoueur.SetActive(false);
        menuPrincipal.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
        SceneLoaderManager.Instance.LoadScene("Niveau");
    }

    public void Multiplayer()
    {
        menuPrincipal.SetActive(false);
        menuMultijoueur.SetActive(true);
        //Initialiser la connection au serveur Unity pour le multijoueur 
    }
   public void Quit()
    {
        Application.Quit();
    }
}
