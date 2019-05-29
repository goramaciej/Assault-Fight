using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerFiring : MonoBehaviour
{

    [SerializeField] GameObject[] guns;

    private void Start() {
        Fire(false);
    }
    void Update() {
        //Fire(CrossPlatformInputManager.GetButton("Fire"));
    }
    private void Fire(bool fire) {
        foreach (GameObject gun in guns) {
            ParticleSystem.EmissionModule em = gun.GetComponent<ParticleSystem>().emission;
            em.enabled = fire;
        }
    }
    public void FireButtonDown() {
        Fire(true);
    }
    public void FireButtonUp() {
        Fire(false);
    }
}
