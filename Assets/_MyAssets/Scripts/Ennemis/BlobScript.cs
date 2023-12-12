using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobScript : MonoBehaviour
{
    private Player_VR player;
    private Gun Gun;

    [SerializeField] private ParticleSystem hitMonstreEffect;

    void Start()
    {
        player = GetComponent<Player_VR>();
        Gun = FindObjectOfType<Gun>();
        Gun.OnBlobHitEvent += OnBlobHitEvent;
    }



    void Update()
    {
        Debug.Log(player._sante);
    }

    private void OnBlobHitEvent(GameObject obj, RaycastHit hit)
    {
        hitMonstreEffect.transform.position = hit.point;
        hitMonstreEffect.transform.forward = hit.normal;
        hitMonstreEffect.Emit(100);
        hitMonstreEffect.transform.SetParent(null);
        Gun.OnEnemyHitEvent -= OnBlobHitEvent;
        player.BlobHit();
        Destroy(obj);
    }
}
