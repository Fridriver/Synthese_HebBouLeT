using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private ParticleSystem hitMonstreEffect;
    private Gun Gun;

    void Start()
    {
        Gun = FindObjectOfType<Gun>();
        Gun.OnEnemyHit += EventGunOnEnemyHit;
    }

    private void EventGunOnEnemyHit(GameObject obj,RaycastHit hit)
    {
        hitMonstreEffect.transform.position = hit.point;
        hitMonstreEffect.transform.forward = hit.normal;
        hitMonstreEffect.Emit(100);
        hitMonstreEffect.transform.SetParent(null);
        Gun.OnEnemyHit -= EventGunOnEnemyHit;
        Destroy(obj);
        
    }
}
