using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDoubleJump : MonoBehaviour
{
    PlayerController c;
    void Start()
    {
        c = GetComponent<PlayerController>();
    }
    void Update()
    {
        DoubleJump();
    }

    public void DoubleJump()
    {
        if (c.doubleJump == true && c.die == false && c.canClimb == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                c.animator.SetBool("jump", true);
                c.rb.velocity = new Vector2(c.rb.velocity.x, Vector2.up.y * 12);
                c.doubleJump = false;
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
