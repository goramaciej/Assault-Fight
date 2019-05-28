using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 1;
    //[SerializeField] GameObject explosion;
    [SerializeField] int health = 100;

    [SerializeField] public float collisionPosition = 0;

    private Explosion explosion;
    private Rigidbody rigidBody;
    private HealthBoard healthBoard;

    private void Start() {
        //myExp = explosion.GetComponent<Explosion>();
        //myExp.CheckMeOut();
        SetExplosion();
        rigidBody = GetComponent<Rigidbody>();
        healthBoard = FindObjectOfType<HealthBoard>();
    }
    private void FixedUpdate() {
        
    }


    private void OnCollisionEnter(Collision collision) {
        rigidBody.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezePositionZ;        
    }

    private void OnCollisionStay(Collision collision) {
        //Vector3 direction = collision.transform.localPosition;
        //collisionPosition = direction.x;
        collisionPosition = this.transform.InverseTransformPoint(collision.contacts[0].point).x;

        Damage();        
    }

    private void OnCollisionExit(Collision collision) {
        rigidBody.velocity = Vector3.zero;
        rigidBody.constraints = RigidbodyConstraints.FreezePositionZ;
        collisionPosition = 0;
    }

    private void OnTriggerEnter(Collider other) {
        //
    }
    private void Damage() {
        if (health <= 0) {
            Die();
        } else {
            health--;
        }
        healthBoard.SetHealth(health);
    }


    private void SetExplosion() {
        explosion = GetComponentInChildren<Explosion>(true);
        if (!explosion) {
            Debug.Log("Ther is no explosion in player children");
        }
    }

    private void Die() {
        SendMessage("OnPlayerDeath");
        Invoke("ReloadScene", 3);
        explosion.gameObject.SetActive(true);
    }
    private void ReloadScene() {
        SceneManager.LoadScene(1);
    }
}