using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int scoreForDestroy = 12;

    [SerializeField] GameObject explosion;
    [SerializeField] Transform xplosionParent;


    // how many hits enemy needs to die
    [SerializeField] int hit = 3;

    private Rigidbody rb;
    private Collider boxCollider;
    private ScoreBoard scoreBoard;
    private GameObject explodeGO;

    private Vector3 startPosition;
    private Quaternion startRotation;

    void Start()
    {
        AddCollider();

        scoreBoard = FindObjectOfType<ScoreBoard>();
        rb = GetComponent<Rigidbody>();

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    public void Reset() {
        rb.velocity = Vector3.zero;
        transform.position = startPosition;
        transform.rotation = startRotation;
        gameObject.SetActive(true);
    }

    private void AddCollider() {
        boxCollider = gameObject.GetComponent<BoxCollider>();

        if (!boxCollider) {
            Debug.Log("No collider... add it");
            boxCollider = gameObject.AddComponent<BoxCollider>();            
        }
        boxCollider.isTrigger = false;
    }

    private void OnParticleCollision(GameObject other) {
        TakeDamage();
    }

    private void TakeDamage() {
        hit--;
        if (hit < 1) {
            Die();
        }
    }

    private void Die() {
        rb.useGravity = true;
        rb.AddForce(Vector3.down * 200f);
        rb.AddTorque(new Vector3(0, 2000f, 0));

        //rb.AddForce(new Vector3(1000f, 1000f, 1000f));

        // Add Points;
        scoreBoard.AddScore(scoreForDestroy);

        // Explosion will be destroyed by itself when particle finished
        explodeGO = Instantiate(explosion, transform.position, Quaternion.identity);
        explodeGO.transform.parent = this.transform;
        // xplosionParent indicates where instantation should be done
        /*if (xplosionParent) {
            explodeGO.transform.parent = xplosionParent;
        }*/
        Invoke("DestroyMe", 3f);
    }
    private void DestroyMe() {
        Destroy(explodeGO);
        gameObject.SetActive(false);
        //Destroy(gameObject);
    }
}
