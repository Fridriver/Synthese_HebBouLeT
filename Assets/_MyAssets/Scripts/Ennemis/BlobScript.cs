using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Services.Lobbies.Models;
using UnityEngine;

public class BlobScript : MonoBehaviour
{
    private Player_VR player;
    private Gun Gun;

    [SerializeField] private ParticleSystem hitMonstreEffect;
    [SerializeField] private float _gainSante = 100f;
    //public event Action<float> OnHealthChangeEvent;

    void Start()
    {
        player = FindObjectOfType<Player_VR>();
        Gun = FindObjectOfType<Gun>();
        Gun.OnBlobHitEvent += OnBlobHitEvent;
    }

    private void OnBlobHitEvent(GameObject obj, RaycastHit hit)
    {
        hitMonstreEffect.transform.position = hit.point;
        hitMonstreEffect.transform.forward = hit.normal;
        hitMonstreEffect.Emit(100);
        hitMonstreEffect.transform.SetParent(null);
        Gun.OnEnemyHitEvent -= OnBlobHitEvent;

        player._sante += _gainSante;
        //Player_EventChangeHealth(_gainSante);
        //player.EventHealth += Player_EventChangeHealth;

        Destroy(obj);
    }

    //private void Player_EventChangeHealth(float obj)
    //{
    //    Debug.Log("COUCOU");
    //    player._sante += obj;

    //    //_sante = Mathf.Clamp(_sante, 0f, _maxSante);
    //}
}
