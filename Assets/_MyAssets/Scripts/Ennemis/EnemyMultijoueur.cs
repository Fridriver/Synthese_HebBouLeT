using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using Unity.Netcode;
using Unity.Services.Lobbies.Models;

public class EnemyMultijoueur: NetworkBehaviour
{

    [Header("Effet de particule")]
    [SerializeField] private ParticleSystem hitMonstreEffect;
    private Gun Gun;

    private NavMeshAgent navMeshAgent;
    private Transform target;
    private AvatarReseau[] players;
    [SerializeField] private float degatMonstres = 20f;
    private Player_VR player_VR;

    void Start()
    {
        Gun = FindObjectOfType<Gun>();
        Gun.OnEnemyHitEvent += OnEnemyHitEventClientRPC;
        navMeshAgent = GetComponent<NavMeshAgent>();
        players = FindObjectsOfType<AvatarReseau>();
        player_VR = FindObjectOfType<Player_VR>();

        ChoosingTarget();
    }

    private void Update()
    {
        if (IsServer)
        {
            
            ChoosingTarget();

        }
        else
        {
            target = null;
            navMeshAgent.enabled = false;
        }

    }

    private void ChoosingTarget()
    {
        float minDistance = float.MaxValue;
        foreach (var player in players)
        {
            float distance = Vector3.Distance(player.transform.position, transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = player.RootAvatar;
            }
        }

        navMeshAgent.SetDestination(target.position);

        foreach (var player in players)
        {
            if (Vector3.Distance(player.transform.position, transform.position) < 1.5f)
            {
                player_VR._sante -= degatMonstres;
                Destroy(gameObject);

            }

        }

    }

    [ClientRpc]
    private void OnEnemyHitEventClientRPC(GameObject obj,RaycastHit hit)
    {
        hitMonstreEffect.transform.position = hit.point;
        hitMonstreEffect.transform.forward = hit.normal;
        hitMonstreEffect.Emit(100);
        hitMonstreEffect.transform.SetParent(null);
        navMeshAgent.enabled = false;
        Destroy(obj);
    }
}
