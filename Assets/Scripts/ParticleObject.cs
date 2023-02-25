using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleObject : MonoBehaviour
{
    [SerializeField] private ParticleSystem _Particle;

    private void Start()
    {
        Destroy(gameObject, _Particle.main.duration);
    }
}
