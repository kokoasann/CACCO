﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectDeth : MonoBehaviour
{
    ParticleSystem effect;
    // Start is called before the first frame update
    void Start()
    {
        effect = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!effect.isPlaying)
        {
            Destroy(gameObject);
        }
    }
}
