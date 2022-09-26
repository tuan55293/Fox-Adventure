using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleController : Enemy
{
    public LayerMask playerLayer;
    public bool hadFoundPlayer;
    public PlayerController playerController;
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        DetectedPlayer();
        KillPlayer();
    }
    void DetectedPlayer()
    {
        Collider2D player = Physics2D.OverlapCircle((Vector2)transform.position, 5, playerLayer);
        if (player)
        {
            playerController = player.gameObject.GetComponent<PlayerController>();
            hadFoundPlayer = true;
        }
    }
    void KillPlayer()
    {
        if (hadFoundPlayer)
        {
            if(Camera.main.WorldToViewportPoint(transform.position).x > 0.5f)
            {
                if (transform.localScale.x < 0)
                {
                    transform.localScale = new Vector3(-(transform.localScale).x, transform.localScale.y);
                }
            }
            if (Camera.main.WorldToViewportPoint(transform.position).x < 0.5f)
            {
                if (transform.localScale.x > 0)
                {
                    transform.localScale = new Vector3(-(transform.localScale).x, transform.localScale.y);
                }
            }
            if (playerController != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, playerController.transform.position, enemyMoveSpeed* Time.deltaTime);
            }
        }

    }
    public override void Die()
    {
        Destroy(gameObject);
    }
}
