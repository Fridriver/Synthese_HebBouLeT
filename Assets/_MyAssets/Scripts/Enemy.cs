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
        //Gun.OnEnemyHit += EventGunOnEnemyHit;
    }

    private void EventGunOnEnemyHit(GameObject obj)
    {
        hitMonstreEffect.transform.position = obj.transform.position;
        hitMonstreEffect.transform.forward = obj.transform.position.normalized;
        hitMonstreEffect.Emit(100);
        hitMonstreEffect.transform.SetParent(null);
        Destroy(obj);
    }
}
