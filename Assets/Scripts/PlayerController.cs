using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator animator;
    public float radius;
    private Rigidbody rb;
    public float forwardSpeed = 0.08f;
    public float backSpeed = 0.01f;
    public float sideSpeed = 0.04f;
    public float speed = 0; //field made public so that you can see the intermidiate values of speed while playing 

    public LayerMask layerMask;
    public bool grounded;
    
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Grounded();
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && this.grounded)
        {
            this.rb.AddForce(Vector3.up*4, ForceMode.Impulse);
        }
    }

    private void Grounded()
    {
        if (Physics.CheckSphere(this.transform.position + Vector3.down, radius, layerMask))
        {
            this.grounded = true;
        }
        else
        {
            this.grounded = false;
        }
        this.animator.SetBool("Jump", !this.grounded);
    }

    private void Move()
    {
        float verticalAxis = Input.GetAxis("Vertical");
        float horizontalAxis = Input.GetAxis("Horizontal");

        Vector3 movement = this.transform.forward * verticalAxis + this.transform.right * horizontalAxis;
        movement.Normalize();


        //calculate speed depending on the direction (may be its possible without if else but its easier to understand for me like that)
        float angle = Math.Abs((Vector3.Angle(movement, this.transform.forward))); // abs angle between movement and forward vectors
        if (angle <= 90f) // forward
        {
            float diff = forwardSpeed - sideSpeed;
            speed = forwardSpeed - diff * angle / 90;
        }
        else // back
        {
            angle = 180 - angle;
            float diff = sideSpeed - backSpeed;
            speed = backSpeed + diff * angle / 90;
        }

        this.transform.position += movement * speed;
        this.animator.SetFloat("Vertical", verticalAxis);
        this.animator.SetFloat("Horizontal", horizontalAxis);


    }
}
