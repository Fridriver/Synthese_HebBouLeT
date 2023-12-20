using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneFinUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _nombreVague;

    private void Start()
    {
       int waveCount = GameCore.Instance.waveCount;

        if ((waveCount - 1) < 2)
            _nombreVague.text = (waveCount - 1).ToString() + " vague";
        else
            _nombreVague.text = (waveCount - 1).ToString() + " vagues";
    }

    public void Rejouer()
    {
        SceneManager.LoadScene(2);
    }

    public void Retour()
    {
        SceneManager.LoadScene(0);
    }
}
