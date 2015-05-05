using UnityEngine;
using System.Collections;

public class PlayerMovementScript : MonoBehaviour {

    public float movementSpeed;
    public float jumpVelocity;
    public float groundDistance;

    private bool grounded = false;
    private int groundCollisions = 0;
    private Rigidbody2D rb;
    
	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate () 
    {
        var hit = Physics2D.Raycast(transform.position, -Vector2.up, groundDistance);
        grounded = hit.collider.gameObject.tag == "Ground";


	}

    void OnCollisionEnter2D(Collision2D other)
    {

    }

    void OnCollisionExit2D(Collision2D other)
    {

    }
}
