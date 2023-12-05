using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerLobby : MonoBehaviour
{
    [Header("Panels UI")]
    [SerializeField] private GameObject _authentification = default;

    [SerializeField] private GameObject _menuLobby = default;
    [SerializeField] private GameObject _creerLobby = default;
    [SerializeField] private GameObject _rejoindreLobby = default;
    [SerializeField] private GameObject _salleAttente = default;
    [SerializeField] private GameObject _chargement = default;

    [Header("Boutons")]
    [SerializeField] private Button _partieRapideButton = default;

    [SerializeField] private Button _creerLobbyButton = default;
    [SerializeField] private Button _rejoindreLobbyButton = default;

    [Header("Boutons de Retour")]
    [SerializeField] private Button _quitterLobbyButton = default;

    [SerializeField] private Button _retourJoindreButton = default;
    [SerializeField] private Button _retourCreerButton = default;
    [SerializeField] private Button _retourSalleAttente = default;

    private void Start()
    {
        // Affiche le panneau d'authentification au départ
        //ActiverUI(3);

        // Appeler l'évènement SignIn de l'authentification
        AuthentificationManager.Instance.SignIn.AddListener(() => ActiverUI(0));

        // Rejoint le premier Lobby Actif
        _partieRapideButton.onClick.AddListener(() => LobbyManager.Instance.PartieRapideLobby());

        _creerLobbyButton.onClick.AddListener(() => ActiverUI(1, true));
        _rejoindreLobbyButton.onClick.AddListener(() => ActiverUI(2, true));

        _retourCreerButton.onClick.AddListener(() => ActiverUI(0, false));
        _retourJoindreButton.onClick.AddListener(() => ActiverUI(0, false));
        _retourSalleAttente.onClick.AddListener(() => ActiverUI(0, false));

        _quitterLobbyButton.onClick.AddListener(() => RetourMenuPrincipal());

        // Quand un joueur se connecte on appelle la méthode OnClientConnected
        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;

        // Si un chargement s'effectue sur le Lobby j'active le panneau de chargement
        LobbyManager.Instance.OnStartJoindreLobby.AddListener(() => ActiverUI(5));
        // Si une erreur de connexion se produit sur le lobby je retourne au menu initial
        LobbyManager.Instance.OnFailedJoindreLobby.AddListener(() => ActiverUI(0));
    }

    private void OnDestroy()
    {
        NetworkManager.Singleton.OnClientConnectedCallback -= OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback -= OnClientDisconnected;
    }

    // Cette méthode est appeler par tous les clients et l'host lors de la déconnexion
    private void OnClientDisconnected(ulong obj)
    {
        if (!NetworkManager.Singleton.IsServer)
        {
            QuitterLobbyUI();
        }
    }

    public void QuitterLobbyUI()
    {
        ActiverUI(0, false);
        LobbyManager.Instance.LeaveLobbyAsync();
    }

    private void OnClientConnected(ulong obj)
    {
        // Vérifie que le joueur qui vient de se connecter est bien le joueur local
        // si oui active le panneau de la salle d'attente
        if (obj == NetworkManager.Singleton.LocalClientId)
        {
            ActiverUI(4);
        }
    }

    // Permet d'activer le gameObject correspondant à l'index
    // et de déastiver les autres
    public void ActiverUI(int index)
    {
        GameObject[] uiElements = new GameObject[] { _menuLobby, _creerLobby, _rejoindreLobby, _authentification, _salleAttente, _chargement };
        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].SetActive(i == index);
        }
    }

    public void ActiverUI(int index, bool positive)
    {
        GameObject[] uiElements = new GameObject[] { _menuLobby, _creerLobby, _rejoindreLobby, _authentification, _salleAttente, _chargement };
        for (int i = 0; i < uiElements.Length; i++)
        {
            uiElements[i].SetActive(i == index);
            UIAudioPlayer.Play(positive);
        }
    }

    private void RetourMenuPrincipal()
    {
        UIAudioPlayer.PlayNegative();
        LobbyManager.Instance.LeaveLobbyAsync();
        AuthentificationManager.Instance.Logout();
        SceneLoaderManager.Instance.LoadScene("StartScene");
    }
}