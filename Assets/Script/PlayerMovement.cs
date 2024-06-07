using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 1f;
    public float jumpForce = 3f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private float horizontalInput;
    private bool jumpRequest;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void SpriteFlip(float horizontalInput)
    {
        if (horizontalInput < 0) 
        {
            spriteRenderer.flipX = true;
        }  
        else if (horizontalInput > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    void Update()
    {
        // Get input for horizontal movement and jump
        horizontalInput = Input.GetAxis("Horizontal");

        // Handle jump input
        if (Input.GetKeyDown(KeyCode.Space) && Mathf.Abs(rb.velocity.y) < 0.001f)
        {
            jumpRequest = true;
        }
    }

    void FixedUpdate()
    {
        // Handle movement in FixedUpdate for consistent physics updates
        if (horizontalInput != 0)
        {
            transform.Translate(new Vector3(horizontalInput * speed * Time.deltaTime, 0f, 0f));
            SpriteFlip(horizontalInput);
            PlayWalk();
        }
        else
        {
            StopWalk();
        }

        // Handle jump request
        if (jumpRequest)
        {
            rb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            PlayJump();
            jumpRequest = false;
        }
    }

    #region AnimationHandler
    private void PlayWalk()
    {
        animator.SetBool("isWalking", true);
    }

    private void StopWalk()
    {
        animator.SetBool("isWalking", false);
    }

    private void PlayJump()
    {
        animator.SetTrigger("goJump");
    }
    #endregion
}
