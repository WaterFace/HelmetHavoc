using UnityEngine;
using System.Collections;

public enum PlayerState
{
    IDLE,
    WALKING,
    JUMPING,
    FALLING
}

public class PlayerMovementScript : MonoBehaviour {

    public float movementSpeed;
    public float jumpVelocity;

    private bool grounded = false;
    private int groundCollisions = 0;
    private Rigidbody2D rb;
    private PlayerState state;
    private Animator anim;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
        grounded = groundCollisions > 0;

        Vector2 newVel = rb.velocity;

        newVel.x = Input.GetAxis("Horizontal") * movementSpeed;

        anim.SetBool("Walking", grounded && Input.GetButton("Horizontal"));
        
        if (grounded && Input.GetButton("Jump"))
        {
            newVel.y = jumpVelocity;
        }

        if (!grounded && rb.velocity.y < 0)
        {
            Debug.Log(Time.deltaTime);
        }

        rb.velocity = newVel;
        anim.SetFloat("VertVel", rb.velocity.y);
	}

    void OnCollisionEnter2D(Collision2D other)
    {
        groundCollisions++;
    }

    void OnCollisionExit2D(Collision2D other)
    {
        groundCollisions--;
    }
}
