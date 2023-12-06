using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneFinUIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _nombreVague;
    private EnemiesSpawner _enemiesSpawner;

    private void Start()
    {
       _enemiesSpawner = FindAnyObjectByType<EnemiesSpawner>();
        if ((_enemiesSpawner._waveNumber - 1) < 2)
            _nombreVague.text = (_enemiesSpawner._waveNumber - 1).ToString() + " vague";
        else
            _nombreVague.text = (_enemiesSpawner._waveNumber - 1).ToString() + " vagues";
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
