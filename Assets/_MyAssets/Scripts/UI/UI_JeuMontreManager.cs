using UnityEngine;

public class UI_JeuMontreManager : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private GameObject UI_InGame;

    public void AfficherUI()
    {
        UI_InGame.SetActive(!UI_InGame.activeSelf);
    }
}