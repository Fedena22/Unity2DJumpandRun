﻿using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {

	private Animator animator;

	public float maxSpeed = 4;
	public float jumbForce = 550;
	public Transform groundCheck;
	public LayerMask whatIsGround;
    public float move = 1.0F;

	private Rigidbody2D rb2d;
	private bool isGrounded = false;
	private bool jump = false;
	[HideInInspector]
	public bool lookingRight = true;
    HealthControler HealthControler;
	// Use this for initialization
	void Start () {
		animator = this.GetComponent<Animator>();
		rb2d = this.GetComponent<Rigidbody2D> ();
        HealthControler = GetComponent<HealthControler>();
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown ("Jump") && isGrounded)
			jump = true;
	}
	void FixedUpdate()
	{
		

		//float vertical = Input.GetAxis ("Vertical"); // Hoch Runter
        //float horizontal = Input.GetAxis ("Horizontal"); // Links Rechts

        animator.SetFloat("Speed", Mathf.Abs(move));
        //animator.SetFloat ("Speed", Mathf.Abs (horizontal));

        //rb2d.velocity = new Vector2 (horizontal * maxSpeed, rb2d.velocity.y);
        if (HealthControler.health >= 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(move * maxSpeed, GetComponent<Rigidbody2D>().velocity.y); //Automover
        }
        isGrounded = Physics2D.OverlapCircle (groundCheck.position, 0.15F, whatIsGround);

		animator.SetBool ("isGrounded", isGrounded);

		//if (( horizontal > 0 && !lookingRight) || (horizontal < 0 && lookingRight))
		//	Flip();
		if (jump)
			rb2d.AddForce(new Vector2(0,jumbForce));
			jump = false;
		}

	public void Flip ()
	{
		lookingRight = !lookingRight;
		Vector3 mySacle = transform.localScale;
		mySacle.x *= -1;
		transform.localScale = mySacle;
	}
}