using UnityEngine;
using UnityEngine.UI;

public class CanvasManager : MonoBehaviour
{
    [Header("Panneaux UI")]
    [SerializeField] private GameObject menuJeu = default;

    [SerializeField] private GameObject menuMultijoueur;
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

    //LIST_SCENES[1] = "Niveau"

    private GameObject[] listPanels;

    // Start is called before the first frame update
    private void Start()
    {
        listPanels = new GameObject[] { menuJeu, menuMultijoueur, menuOptions, menuInstructions };

        ActivateRightPanel(0);

        multijoueurButton.onClick.AddListener(() => GoToMultiplayerScene()); 
        optionsButton.onClick.AddListener(() => ActivateRightPanel(2, true));
        instructionsButton.onClick.AddListener(() => ActivateRightPanel(3, true));

        retourInstructionsMenu.onClick.AddListener(() => ActivateRightPanel(0, false));
        retourOptionsMenu.onClick.AddListener(() => ActivateRightPanel(0, false));
        retourMultijoueurMenu.onClick.AddListener(() => ActivateRightPanel(0, false));

        jouerButton.onClick.AddListener(() => Play());
        quitterButton.onClick.AddListener(() => Quit());
    }

    // Update is called once per frame
    private void Update()
    {
    }

    public void Play()
    {
        UIAudioPlayer.PlayPositive();
        SceneLoaderManager.Instance.LoadScene("Niveau");
    }

    private void ActivateRightPanel(int index)
    {
        for (int i = 0; i < listPanels.Length; i++)
        {
            listPanels[i].SetActive(i == index);
        }
    }

    private void ActivateRightPanel(int index, bool positive)
    {
        for (int i = 0; i < listPanels.Length; i++)
        {
            listPanels[i].SetActive(i == index);
            UIAudioPlayer.Play(positive);
        }
    }

    private void GoToMultiplayerScene()
    {
        SceneLoaderManager.Instance.LoadScene("StartSceneMultijoueur");
    }

    public void Quit()
    {
        UIAudioPlayer.PlayNegative();
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}