using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{

    [Header("Effet de particule")]
    [SerializeField] private ParticleSystem hitMonstreEffect;
    private Gun Gun;

    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target;
    [SerializeField] private float degatMonstres = 20f;
    private Player_VR player_VR;

    void Start()
    {
        Gun = FindObjectOfType<Gun>();
        Gun.OnEnemyHitEvent += OnEnemyHitEvent;
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<XROrigin>().transform;
        player_VR = FindObjectOfType<Player_VR>();
    }

    private void Update()
    {
        navMeshAgent.SetDestination(target.position);

        if (Vector3.Distance(target.position, transform.position) < 1.5f)
        {
            player_VR._sante -= degatMonstres;
            Destroy(gameObject);
            //player_VR.PlayerDead();
        }
    }

    private void OnEnemyHitEvent(GameObject obj,RaycastHit hit)
    {
        hitMonstreEffect.transform.position = hit.point;
        hitMonstreEffect.transform.forward = hit.normal;
        hitMonstreEffect.Emit(100);
        hitMonstreEffect.transform.SetParent(GameObject.Find("Particules").transform);
        Destroy(obj);
    }

  

}
