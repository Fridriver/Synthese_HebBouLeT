using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class PathFinder : MonoBehaviour
{
    private Transform _target;
    private NavMeshAgent _agent;
    
    void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player").transform;
        _agent = GetComponent<NavMeshAgent>();
    }


}
