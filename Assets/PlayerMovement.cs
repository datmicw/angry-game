using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    private Rigidbody2D rb;
    private Animator animator;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        Move();
        Jump();
        Attack();
        UpdateAnimationStates();
    }

    void Move()
    {
        float moveInput = Input.GetAxis("Horizontal");
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);

        if (moveInput > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (moveInput < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    void Jump()
    {
        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
            animator.SetBool("isJumping", true);
        }
    }

    void UpdateAnimationStates()
    {
        float moveInput = Input.GetAxis("Horizontal");
        bool isRunning = Mathf.Abs(moveInput) > 0.1f;

        animator.SetBool("isRunning", isRunning);

        // Nếu nhân vật đã tiếp đất sau khi nhảy
        if (isGrounded)
        {
            animator.SetBool("isJumping", false);
        }
    }
    // Nhấp chuột trái hiện skill 
    void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("attackTrigger");

        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Mặt đất có Tag: Ground
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
