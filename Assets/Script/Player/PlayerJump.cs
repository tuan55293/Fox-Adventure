using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJump : MonoBehaviour
{
    PlayerController c;
    private void Start()
    {
        c = GetComponent<PlayerController>();
    }

    void Update()
    {
        Jump();
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && c.die == false && c.canClimb == false )
        {
            if (c.groundCheck && c.jumped == false && c.doubleJump == false)
            {
                c.animator.SetBool("jump", true);
                c.rb.velocity = new Vector2(c.rb.velocity.x, Vector2.up.y * 12);
                c.groundCheck = false;
                c.jumped = true;
                c.doubleJump = true;
            }
        }
        if (c.rb.velocity.y == 0)
        {
            c.jumped = false;
            c.doubleJump = false;
            c.animator.SetBool("jump", false);
        }
    }
}
