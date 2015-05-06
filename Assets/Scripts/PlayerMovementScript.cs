using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementScript : MonoBehaviour {

    public float movementSpeed;
    public float jumpVelocity;
    public float groundDistance;

    private bool grounded = false;
    private Rigidbody2D rb;
    private Vector2 rayDir;
    private Vector2 hitPos;
    
	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
 	}
	
	void FixedUpdate () 
    {
        rayDir = Quaternion.AngleAxis(15f * Input.GetAxis("Horizontal"), Vector3.forward) * -Vector2.up;
        
        var hit = Physics2D.Raycast(transform.position, rayDir, groundDistance);

        if (hit) { hitPos = hit.point; }
 
        if (hit) { grounded = hit.collider.gameObject.tag == "Ground" && rb.velocity.y <= 0f; }
        else     { grounded = false; }
	}

    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            rb.velocity = new Vector2(movementSpeed * Input.GetAxis("Horizontal"),
                                      rb.velocity.y);
        }
        if (grounded && Input.GetButtonDown("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x,
                                      rb.velocity.y + jumpVelocity);
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position + (Vector3)rayDir * groundDistance, 0.1f);
        Gizmos.DrawSphere(hitPos, 0.2f);
    }
}
