using System.Collections;
using UnityEngine;

public class Sante : MonoBehaviour
{
    [SerializeField] private float _gainSante = 20;
    [SerializeField] private float _santePerteSec = 0.004f;
    [SerializeField] private float _maxSante = 100;
    private float _sante;



    // Start is called before the first frame update
    void Start()
    {
        _sante = _maxSante;
    }

    // Update is called once per frame
    void Update()
    {
        //Maladie();
        //Debug.Log(_sante);
    }


    private void Maladie()
    {
        _sante -= _santePerteSec;
    }

    public void BlobHit()
    {
        _sante += _gainSante;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ennemi")
        {
            PlayerDead();
        }
    }

    private void PlayerDead()
    {
        StartCoroutine(DeadWait());
        SceneLoaderManager.Instance.LoadScene("EndScene");
    }

    IEnumerator DeadWait()
    {
        yield return new WaitForSeconds(5f);
    }
}
