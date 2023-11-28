using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_JeuMontreManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject UI_InGame;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AfficherUI()
    {
        UI_InGame.SetActive(!UI_InGame.activeSelf);
    }
}
