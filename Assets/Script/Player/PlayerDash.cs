using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDash : MonoBehaviour
{
    PlayerController c;
    void Start()
    {
        c = GetComponent<PlayerController>();
    }

    void Update()
    {
        Dash();
    }

    void Dash()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift) && c.DashEnergy >= 5 && c.hadDash == false && c.die == false && c.canClimb == false)
        {
            c.DashEnergy -= 5;
            if (c.dash)
            {
                c.dash.fillAmount = c.DashEnergy / 15;
            }

            c.hadDash = true;
            c.animator.SetBool("run", false);
            c.animator.SetBool("dash", true);
            StartCoroutine("DashTime");
            StartCoroutine("GhostVfx");
        }
    }

    IEnumerator DashTime()
    {
        c.rb.gravityScale = 0;
        c.rb.velocity = transform.localScale.x * Vector2.right * 5f;
        yield return new WaitForSeconds(0.2f);
        c.rb.velocity = Vector2.zero;
        c.rb.gravityScale = 4;
        c.hadDash = false;
        c.animator.SetBool("dash", false);
        StopCoroutine("GhostVfx");
        StopCoroutine("DashTime");

    }

    void MakeGhost()
    {
        if (c.ghost)
        {
            Instantiate(c.ghost, transform.position, Quaternion.identity).transform.localScale = transform.localScale;
        }
    }


    IEnumerator GhostVfx()
    {
        while (true)
        {
            MakeGhost();
            yield return new WaitForSeconds(0.03f);
        }
    }
}
