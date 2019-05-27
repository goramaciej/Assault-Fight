using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnInput : MonoBehaviour
{
    [SerializeField] private Vector3 torque;
    private Rigidbody rb;

    private void Awake() {
        rb = GetComponent<Rigidbody>();
    }

    void Update() {
        if (Input.GetKey(KeyCode.R)) {
            rb.AddTorque(torque);
            Debug.Log("addtorque");
        }
    }
}