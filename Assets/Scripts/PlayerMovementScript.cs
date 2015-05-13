using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementScript : MonoBehaviour {

    public float movementSpeed;
    public float maxSpeed;
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
        grounded = Physics2D.Linecast(transform.position, groundCheck.position);

        if (grounded)
        {
            rb.AddForce(Vector2.right * Input.GetAxis("Horizontal") * movementSpeed, ForceMode2D.Impulse);
            rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawRay(transform.position, groundCheck.position);
    }
}
