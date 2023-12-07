using UnityEngine;
using UnityEngine.UI;

public class UI_JeuCanvaManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject PanelMenuPrincipal;

    [SerializeField] private GameObject PanelOptions;

    [Header("Boutons pour ouvrir Panel")]
    [SerializeField] private Button btnOptions;

    [SerializeField] private Button btnQuitter;

    [Header("Boutons de retour")]
    [SerializeField] private Button btnRetourOptions;

    private GameObject[] listPanels;

    // Start is called before the first frame update
    private void Start()
    {
        listPanels = new GameObject[] { PanelMenuPrincipal, PanelOptions };

        ActivateRightPanel(0);

        btnOptions.onClick.AddListener(() => ActivateRightPanel(1, true));
        btnRetourOptions.onClick.AddListener(() => ActivateRightPanel(0, false));
        btnQuitter.onClick.AddListener(() => Quit());
    }

    // Update is called once per frame
    private void Update()
    {
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

    private void Quit()
    {
        UIAudioPlayer.PlayNegative();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}