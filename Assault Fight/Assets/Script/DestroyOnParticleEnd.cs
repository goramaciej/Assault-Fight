using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnParticleEnd : MonoBehaviour
{
    private ParticleSystem particle;

    void Start()
    {
        particle = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (particle) {
            if (!particle.IsAlive()) {
                Debug.Log("Destroy particle");
                Destroy(gameObject);
            }
        }
    }
}
