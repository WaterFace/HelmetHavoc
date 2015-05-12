using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementScript : MonoBehaviour {

    public float movementSpeed;
    public Transform groundCheck;
    public PlayerJetpackScript jetpack;
    public float sideCastDist;

    private bool grounded = false;
    private Rigidbody2D rb;
    private int groundCollisions;
    
	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
 	}

    void Update ()
    {
        if (Physics2D.Linecast(transform.position, transform.position + Vector2.right * Mathf.Sign(Input.GetAxis("Horizontal")) * sideCastDist))
        {
            Debug.Log("test");
        }
        else
        {
            Debug.Log("test2");
            transform.position += Vector3.right * Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            groundCollisions++;
        }
    }

    void OnCollisionLeave2D(Collision2D other)
    {
        if (other.gameObject.tag == "Ground")
        {
            groundCollisions--;
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + Vector3.right * Mathf.Sign(Input.GetAxis("Horizontal")) * sideCastDist, 0.05f);
    }
}
