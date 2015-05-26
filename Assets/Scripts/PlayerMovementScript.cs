using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovementScript : MonoBehaviour {

    public float movementSpeed;
    public Transform groundCheck;
    public PlayerJetpackScript jetpack;
    public float sideCastDist;
    public float jumpSpeed;
    public float speedDecay;
    public Transform sprite;

    private Rigidbody2D rb;
    private int groundCollisions;
    private Animator anim;
    private int animVel;
    private float direction;
    private bool facingLeft;
    
	// Use this for initialization
	void Start () 
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
 	}

    void Update ()
    {
        if (Input.GetButton("Horizontal"))
        {
            rb.AddForce(Vector2.right * Input.GetAxis("Horizontal") * movementSpeed * Time.deltaTime, ForceMode2D.Impulse);
            rb.velocity = new Vector2(rb.velocity.x * speedDecay, rb.velocity.y);

            if (Input.GetAxis("Horizontal") > 0f) { facingLeft = false; }
            else { facingLeft = true; }
        }

        if (jetpack.grounded)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector2.up * jumpSpeed, ForceMode2D.Impulse);
                jetpack.checkGround = false;
                jetpack.grounded = false;

                anim.SetTrigger("Fly");
                anim.SetBool("Land", false);
            }
        }

        if (Input.GetButton("Horizontal"))
        {
            animVel = (int)Mathf.Sign(Input.GetAxis("Horizontal"));
        }
        else
        {
            animVel = (int)rb.velocity.x;
        }

        //please don't touch
        direction = Mathf.Atan2(rb.velocity.y, rb.velocity.x);
        direction = ((1.5f * Mathf.PI + direction) % (2f * Mathf.PI)) / (2f * Mathf.PI);
        anim.SetFloat("Direction", 1f - direction);

        anim.SetBool("Left", facingLeft);

        anim.SetInteger("HorizontalVel", animVel);
    }
}
