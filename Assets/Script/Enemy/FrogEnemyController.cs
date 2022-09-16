using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FrogEnemyController : Enemy
{
    public Vector2 jumpForce;
    bool jumped;
    public bool hadFoundPlayer;
    bool hadAuto;
    Rigidbody2D rb;
    public LayerMask playerLayer;
    public PlayerController playerController;
    bool groundCheck;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        DetectedPlayer();
        if (hadAuto == false && hadFoundPlayer && jumped == false && groundCheck)
        {
            StartCoroutine(KillPlayer());
            hadAuto = true;
        }
        if (rb.velocity.y < 0 && jumped)
        {
            Jump2Animation(true);
        }
        if (rb.velocity == Vector2.zero || groundCheck)
        {
            JumpAnimation(false);
            Jump2Animation(false);
        }
    }

    public void DetectedPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle((Vector2)transform.position, 5, playerLayer);
        if (player)
        {
            playerController = player.gameObject.GetComponent<PlayerController>();
            hadFoundPlayer = true;
        }
    }

    public void FollowPlayer()
    {
        if (hadFoundPlayer)
        {
            if (Vector2.Distance(transform.position, playerController.transform.position) < 10)
            {
                if (Camera.main.WorldToViewportPoint(transform.position).x > 0.5f)
                {
                    if (transform.localScale.x < 0)
                    {
                        transform.localScale = new Vector3(-(transform.localScale).x, transform.localScale.y);
                    }
                    rb.AddForce(jumpForce);
                    rb.velocity = new Vector2(-(enemyMoveSpeed), rb.velocity.y);
                    JumpAnimation(true);
                    jumped = true;
                    groundCheck = false;
                }
                if (Camera.main.WorldToViewportPoint(transform.position).x < 0.5f)
                {
                    if (transform.localScale.x > 0)
                    {
                        transform.localScale = new Vector3(-(transform.localScale).x, transform.localScale.y);
                    }
                    rb.AddForce(jumpForce);
                    rb.velocity = new Vector2(enemyMoveSpeed, rb.velocity.y);
                    JumpAnimation(true);
                    jumped = true;
                    groundCheck = false;
                }
            }
            else
            {
                StopCoroutine(KillPlayer());
                hadAuto = false;
                hadFoundPlayer = false;
            }

        }
    }

    public void JumpAnimation(bool state)
    {
        animator.SetBool("FrogJump", state);
    }
    public void Jump2Animation(bool state)
    {
        animator.SetBool("FrogJump2", state);
    }
    public override void Die()
    {
        Destroy(gameObject);
    }

    IEnumerator KillPlayer()
    {
        while (hadFoundPlayer)
        {
            FollowPlayer();
            yield return new WaitForSeconds(3);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = true;
            JumpAnimation(false);
            jumped = false;
        }
    }

}
