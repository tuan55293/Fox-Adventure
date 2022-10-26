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
        if (c.canDoubleJump == true && c.die == false && c.canClimb == false && c.groundCheck == false)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                AudioController.Ins.PlaySound(AudioController.Ins.jump);
                c.groundCheck = false;
                c.animator.SetBool("jump2", false);
                c.animator.SetBool("jump", true);
                c.rb.velocity = new Vector2(c.rb.velocity.x, Vector2.up.y * 11);
                c.canDoubleJump = false;
            }
        }
        if (c.rb.velocity.y == 0 || c.groundCheck)
        {
            c.jumped = false;
            c.canDoubleJump = false;
            c.animator.SetBool("jump", false);
        }
    }
}
