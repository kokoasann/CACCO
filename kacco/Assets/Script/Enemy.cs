using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    GameObject Target;

    NavMeshAgent nma;
    void Start()
    {
        nma = GetComponent<NavMeshAgent>();
    }
    
    void Update()
    {
        
    }
}
