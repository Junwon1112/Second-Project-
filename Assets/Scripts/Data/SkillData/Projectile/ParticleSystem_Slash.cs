using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem_Slash : MonoBehaviour
{
    public float lifetime = 0.2f;

    void Start()
    {
        Destroy(gameObject, lifetime);
    }

}
