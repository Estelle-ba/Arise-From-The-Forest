using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using static UnityEngine.ParticleSystem;

public class PlayerMouvement : MonoBehaviour
{
    private float horizontal;
    private float speed = 8f;
    private float JumpingPower = 16f;
    private bool isFacingRight = true;
    private bool isJumping = true;
    private bool isGrunded = true;

    private bool doubleJump = false;

    private bool canDash = true;
    private bool isDashing;
    private float dashingPower = 24f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;

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
    [SerializeField]
    private ParticleSystem particles;



    void Update()
    {
        if (isDashing)
        {
            return;
        }

        horizontal = Input.GetAxisRaw("Horizontal");

        Jump();

        if (Input.GetKeyDown(KeyCode.R) && canDash)
        {
            StartCoroutine(Dash());
        }

        Flip();

       
    }


    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (isJumping)
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
        if (!isGrunded && !Input.GetButton("Jump"))
        {
            doubleJump = false;
        }

        if (Input.GetButtonDown("Jump") && isGrunded)
        {
            if (isGrunded)
            {
                isGrunded = false;
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpingPower);

                doubleJump = !doubleJump;
            }
        }
        if (!isGrunded && doubleJump && Input.GetButtonDown("Jump"))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, JumpingPower);
            isGrunded = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        isGrunded = true;
    }

    private IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;
        rb.linearVelocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        particles.Play();
        yield return new WaitForSeconds(dashingTime);
        particles.Stop();
        rb.gravityScale = originalGravity;
        isDashing = false; 
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}

