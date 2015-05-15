using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementScript : MonoBehaviour {

    public float movementSpeed;
    public float maxSpeed;
    public Transform groundCheck;
    public PlayerJetpackScript jetpack;
    public float sideCastDist;
    public float jumpSpeed;

    private Rigidbody2D rb;
    private int groundCollisions;
    
	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
 	}

    void Update ()
    {
        rb.AddForce(Vector2.right * Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, ForceMode2D.Impulse);

        if (jetpack.grounded)
        {
            rb.velocity = Vector2.Lerp(rb.velocity, new Vector2(Mathf.Clamp(rb.velocity.x, -maxSpeed, maxSpeed), rb.velocity.y), Time.deltaTime);

            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                jetpack.checkGround = false;
                jetpack.grounded = false;
                Debug.Log("Jumped");
            }
        }
    }
}
