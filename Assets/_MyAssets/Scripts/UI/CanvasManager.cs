using Unity.Netcode;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{

    [Header("Menu principal")]
    [SerializeField] private GameObject menuPrincipal = default;
    [Header("Panneaux UI")]
    [SerializeField] private GameObject menuJeu = default;
    [SerializeField] private GameObject menuMultijoueur;
    [SerializeField] private GameObject menuAuthentification;
    [SerializeField] private GameObject menuOptions;
    [SerializeField] private GameObject menuInstructions;

    [Header("Boutons Menu Principal")]
    [SerializeField] private Button jouerButton;

    [SerializeField] private Button multijoueurButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button instructionsButton;
    [SerializeField] private Button quitterButton;

    [Header("Boutons de retour")]
    [SerializeField] private Button retourMultijoueurMenu;

    [SerializeField] private Button retourOptionsMenu;
    [SerializeField] private Button retourInstructionsMenu;

    [Header("Menu lobby")]
    [SerializeField] private GameObject Lobby = default;



    //LIST_SCENES[1] = "Niveau"

    private GameObject[] listPanels;

    // Start is called before the first frame update
    private void Start()
    {
        listPanels = new GameObject[] { menuJeu, menuAuthentification, menuMultijoueur, menuOptions, menuInstructions };

        ActivateRightPanel(0);

        multijoueurButton.onClick.AddListener(() => IsConnectedToUnityServices());

        optionsButton.onClick.AddListener(() => ActivateRightPanel(3));
        instructionsButton.onClick.AddListener(() => ActivateRightPanel(4));

        retourInstructionsMenu.onClick.AddListener(() => ActivateRightPanel(0));
        retourOptionsMenu.onClick.AddListener(() => ActivateRightPanel(0));
        retourMultijoueurMenu.onClick.AddListener(() => ActivateRightPanel(0));

        jouerButton.onClick.AddListener(() => Play());
        quitterButton.onClick.AddListener(() => Quit());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Play()
    {
        SceneLoaderManager.Instance.LoadScene(SceneLoaderManager.LIST_SCENES[1]);
    }

    private void ActivateRightPanel(int index)
    {
        for (int i = 0; i < listPanels.Length; i++)
        {
            listPanels[i].SetActive(i == index);
        }
    }

    private void IsConnectedToUnityServices()
    {

        Lobby.SetActive(true);
        menuPrincipal.SetActive(false);

    }

    public void Quit()
    {
        Application.Quit();
    }
}