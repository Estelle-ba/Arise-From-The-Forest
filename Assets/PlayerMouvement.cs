using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class PlayerMouvement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float JumpingPower = 16f;
    private bool isFacingRight = true;
    private bool isJumping = true;
    private bool isGrunded = true;

    [SerializeField]
    private float lowJumpMultiplier = 2f;
    [SerializeField]
    private float fallMultiplier;
    [SerializeField]
    private Rigidbody2D rb;
    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private LayerMask groundLayer;



    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        Jump();

        Flip();

    }


    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(horizontal * speed, rb.linearVelocity.y);

        if (rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if (rb.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            rb.linearVelocity += Vector2.up * Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }

    }

    private void Flip()
    {
        if (isFacingRight && horizontal < 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localeScale = transform.localScale;
            localeScale.x *= -1f;
            transform.localScale = localeScale;
        }
        else if (isFacingRight == false && horizontal > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localeScale = transform.localScale;
            localeScale.x *= -1f;
            transform.localScale = localeScale;
        }
    }

    private void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrunded)
        {
            isGrunded = false;
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpingPower);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrunded = true;
    }
}

