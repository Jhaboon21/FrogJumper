using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    public float speed = 4;
    public int maxHealth = 3;
    public float timeInvincible = 2.0f;
    Rigidbody2D rb;

    public Animator animator;
    bool onGround;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;
    public float jumpForce;

    float invincibleTimer;
    bool isInvincible;
    int currentHealth;

    bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;

    public bool wallJumping;
    public bool wallJumpEnd;
    public float xWallForce;
    public float yWallForce;
    public float wallJumpTime;


    public GameObject hitSound;
    public GameObject projectilePrefab;
    public GameObject jumpSound;
    public Transform shotPoint;

    public bool isFacingRight = true;


    // Start is called before the first frame update
    void Start()
    {
        invincibleTimer = -1.0f;
        currentHealth = maxHealth;
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isInvincible)
        {
            invincibleTimer -= Time.deltaTime;
            if (invincibleTimer < 0)
                isInvincible = false;
        }

        float moveHorizontal = Input.GetAxis("Horizontal");

        // Flips the character depending on the direction they are facing.    
        if (moveHorizontal > 0 && isFacingRight == false)
        {
            Flip();
        }
        else if (moveHorizontal < 0 && isFacingRight == true)
        {
            Flip();
        }

        //Movement
        rb.velocity = new Vector2(moveHorizontal * speed, rb.velocity.y);
        animator.SetFloat("Speed", Mathf.Abs(moveHorizontal));

        //Checks if player is grounded
        onGround = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        //Jump
        if (Input.GetButtonDown("Jump") && onGround == true)
        {
            rb.velocity = Vector2.up * jumpForce;

            GameObject jumpSoundInstance = Instantiate(jumpSound, transform.position, transform.rotation);

            jumpSoundInstance.GetComponent<AudioSource>().Play();

            Destroy(jumpSoundInstance, 1f);
        }

        if (onGround == false)
        {
            animator.SetBool("IsJumping", true);
        }
        else
        {
            animator.SetBool("IsJumping", false);
        }

        //Shoot
        if (Input.GetButtonDown("Fire1"))
        {
            LaunchProjectile();
        }

        //Checks if touching wall
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatIsGround);

        //if touching wall while in the air then you will slide on wall
        if (isTouchingFront == true && onGround == false && moveHorizontal != 0)
        {
            wallSliding = true;
            animator.SetBool("isWallSliding", true);

        }
        else
        {
            wallSliding = false;
            animator.SetBool("isWallSliding", false);
        }

        //wallslide
        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        //walljump
        if (Input.GetButtonDown("Jump") && wallSliding == true)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", wallJumpTime);
            GameObject jumpSoundInstance = Instantiate(jumpSound, transform.position, transform.rotation);

            jumpSoundInstance.GetComponent<AudioSource>().Play();

            Destroy(jumpSoundInstance, 1f);
        }

        if (wallJumping == true)
        {
            rb.velocity = new Vector2(xWallForce * -moveHorizontal, yWallForce);
        }
    }

    public void ChangeHealth(int amount)
    {
        if (amount < 0)
        {
            if (isInvincible)
                return;

            isInvincible = true;
            invincibleTimer = timeInvincible;

            GameControlScript.health -= 1;

            GameObject hitSoundInstance = Instantiate(hitSound, transform.position, transform.rotation);

            hitSoundInstance.GetComponent<AudioSource>().Play();

            Destroy(hitSoundInstance, 4f);
        }
    }

    void OnCollisionEnter2D(Collision2D collisionData)
    {
        if (collisionData.gameObject.CompareTag("Ground"))
        {
            // The character collides with the ground. 
            onGround = true;
        }
    }


    void LaunchProjectile()
    {
        float angle = isFacingRight ? 0f : 180f;
        GameObject projectileObject = Instantiate(projectilePrefab, shotPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));

        Physics2D.IgnoreCollision(projectileObject.GetComponent<Collider2D>(), GetComponent<Collider2D>());
    }

    //flips character
    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        isFacingRight = !isFacingRight;
    }

    //Invoked after walljumping so we don't end up jumping forever.
    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
}
