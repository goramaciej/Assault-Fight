using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int scoreForDestroy = 12;

    [SerializeField] GameObject explosion;
    [SerializeField] Transform xplosionParent;


    [SerializeField] int healthPoints = 100;

    private Rigidbody rb;
    private Collider boxCollider;
    private ScoreBoard scoreBoard;

    void Start()
    {
        AddCollider();

        scoreBoard = FindObjectOfType<ScoreBoard>();
        rb = GetComponent<Rigidbody>();
    }

    private void AddCollider() {
        boxCollider = gameObject.GetComponent<BoxCollider>();

        if (!boxCollider) {
            Debug.Log("No collider... add it");
            boxCollider = gameObject.AddComponent<BoxCollider>();            
        }
        boxCollider.isTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnParticleCollision(GameObject other) {
        Debug.Log("got particle collision");
        Die();
    }

    private void Die() {
        rb.useGravity = true;
        rb.AddForce(new Vector3(1000f, 1000f, 1000f));

        // Add Points;
        scoreBoard.AddScore(scoreForDestroy);

        // Explosion will be destroyed by itself when particle finished
        GameObject explodeGO = Instantiate(explosion, transform.position, Quaternion.identity);
        // xplosionParent indicates where instantation should be done
        if (xplosionParent) {
            explodeGO.transform.parent = xplosionParent;
        }
        Invoke("DestroyMe", 5f);
    }
    private void DestroyMe() {
        Destroy(gameObject);
    }
}
