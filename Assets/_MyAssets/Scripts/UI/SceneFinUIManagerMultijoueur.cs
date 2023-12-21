using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using Unity.Netcode;
using Unity.Services.Relay;

public class SceneFinUIManagerMultijoueur : NetworkBehaviour
{
    [SerializeField] private TMP_Text _nombreVague;
    [SerializeField] private GameObject panelLobby;
    [SerializeField] private GameObject panelFinDeJeu;


    private void Start()
    {
        
       int waveCount = GameCoreMultijoueur.Instance.waveCount;

        if ((waveCount - 1) < 2)
            _nombreVague.text = (waveCount - 1).ToString() + " vague";
        else
            _nombreVague.text = (waveCount - 1).ToString() + " vagues";
    }

    public void Rejouer()
    {
        LobbyManager.Instance.LeaveLobbyAsync();
        panelLobby.SetActive(true);
        panelFinDeJeu.SetActive(false);
        //SceneLoaderManager.Instance.LoadScene(1);

    }

    public void Retour()
    {
        LobbyManager.Instance.LeaveLobbyAsync();
        AuthentificationManager.Instance.Logout();
        SceneLoaderManager.Instance.LoadScene("StartScene");
    }
}
