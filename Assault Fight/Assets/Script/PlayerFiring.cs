using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerFiring : MonoBehaviour
{

    [SerializeField] GameObject[] guns;
    // Start is called before the first frame update
    void Start()
    {
        guns[0].SetActive(true);
    }

    // Update is called once per frame
    void Update() {
        if (CrossPlatformInputManager.GetButton("Fire")) {
            Fire();
        } else {
            Fire(false);
        }
    }
    private void Fire(bool fire = true) {

        foreach (GameObject gun in guns) {
            if (fire) {
                gun.SetActive(true);
            } else {
                gun.SetActive(false);
            }
        }
    }
}
