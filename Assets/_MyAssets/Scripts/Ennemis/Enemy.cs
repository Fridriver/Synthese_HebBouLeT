using System;
using System.Collections;
using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    [Header("Zone de détection")]
    [SerializeField] private Collider detectionZoneEnnemi;
    [Header("Effet de particule")]
    [SerializeField] private ParticleSystem hitMonstreEffect;
    private Gun Gun;

    private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform target;

    void Start()
    {
        Gun = FindObjectOfType<Gun>();
        Gun.OnEnemyHitEvent += OnEnemyHitEvent;
        navMeshAgent = GetComponent<NavMeshAgent>();
        target = FindObjectOfType<XROrigin>().transform;
    }

    private void Update()
    {
        navMeshAgent.SetDestination(target.position);

        if (Vector3.Distance(target.position, transform.position) < 1.5f)
            UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    private void OnEnemyHitEvent(GameObject obj,RaycastHit hit)
    {
        hitMonstreEffect.transform.position = hit.point;
        hitMonstreEffect.transform.forward = hit.normal;
        hitMonstreEffect.Emit(100);
        hitMonstreEffect.transform.SetParent(null);
        Gun.OnEnemyHitEvent -= OnEnemyHitEvent;
        Destroy(obj);
        
    }

  

}
