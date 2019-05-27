using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerCollision : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 1;
    //[SerializeField] GameObject explosion;
    [SerializeField] int health = 1000;

    private Explosion explosion;

    private void Start() {
        //myExp = explosion.GetComponent<Explosion>();
        //myExp.CheckMeOut();
        SetExplosion();
    }

    private void OnCollisionEnter(Collision collision) {
        Debug.Log("Player has got collision: "+ collision.gameObject);
    }

    private void OnTriggerEnter(Collider other) {
        Debug.Log("Player has triggered: " + other.gameObject);
        /*RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit)) {
            Debug.Log("Point of contact: " + hit.point);
        }*/

        //Debug.Log(other.gameObject);
        //Die();
        Damage();
    }
    private void Damage() {
        if (health <= 0) {
            Die();
        } else {
            health--;
        }
    }


    private void SetExplosion() {
        explosion = GetComponentInChildren<Explosion>(true);
        if (!explosion) {
            Debug.Log("Ther is no explosion in player children");
        }
    }

    private void Die() {
        SendMessage("OnPlayerDeath");
        Invoke("ReloadScene", 1);
        explosion.gameObject.SetActive(true);
    }
    private void ReloadScene() {
        SceneManager.LoadScene(1);
    }
}