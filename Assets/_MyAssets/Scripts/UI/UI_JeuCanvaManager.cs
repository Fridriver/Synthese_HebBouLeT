using System.Collections;
using System.Collections.Generic;
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

    private GameObject[] listePanels;
    
   
    // Start is called before the first frame update
    void Start()
    {
        

        listePanels = new GameObject[] { PanelMenuPrincipal, PanelOptions };

        ActivateRightPanel(0);


        btnOptions.onClick.AddListener(() => ActivateRightPanel(1));
        btnRetourOptions.onClick.AddListener(() => ActivateRightPanel(0));
        btnQuitter.onClick.AddListener(()=>Quit());

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void ActivateRightPanel(int index)
    {
        for (int i = 0; i < listePanels.Length; i++)
        {
            {
                listePanels[i].SetActive(i == index);
            }
        }

    }

    private void Quit()
    {
        Application.Quit();
    }
}
