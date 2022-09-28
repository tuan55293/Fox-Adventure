using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public bool run;
    public bool crouch;
    public bool climb;
    public bool jump;
    public bool doubleJump;

    int currentLevel;

    public GameObject ghost;
    bool hadGhost;

    Rigidbody2D rb;
    Animator animator;

    public KillEnemyVfx killEnemyVfx;
    public ItemVfx itemVfx;

    float moveDirectionX;
    public float moveSpeed;
    public bool groundCheck;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        moveDirectionX = Input.GetAxisRaw("Horizontal");

        DoubleJump();
        Jump();
        Jump2();
        Climb();
    }

    void FixedUpdate()
    {
        Move();
    }
    
    public void Move()
    {
        
        if (rb)
        {
            Flip();
            if (moveDirectionX == 0)
            {
                animator.SetBool("run", false);
            }
            if ((moveDirectionX < 0 || moveDirectionX > 0) && groundCheck)
            {

                if (hadGhost == false)
                {
                    StartCoroutine(GhostVfx());
                    hadGhost = true;
                }
                animator.SetBool("run", true);
            }
            if (moveDirectionX == 0) 
            {
                StopAllCoroutines();
                hadGhost = false;
                rb.velocity = new Vector2(Vector2.zero.x, rb.velocity.y);
            } 
            rb.velocity = new Vector2(Vector2.right.x * moveSpeed * moveDirectionX,rb.velocity.y);
        }
    }

    public void Flip()
    {
        if(transform.localScale.x < 0 && moveDirectionX > 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1, 
                                                transform.localScale.y, transform.localScale.z);
        }
        if (transform.localScale.x > 0 && moveDirectionX < 0)
        {
            transform.localScale = new Vector3(transform.localScale.x * -1,
                                                transform.localScale.y, transform.localScale.z);
        }
    }
    public void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (groundCheck && jump == false && doubleJump == false)
            {
                animator.SetBool("jump", true);
                rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y*13);
                groundCheck = false;
                jump = true;
                doubleJump = true;
            }
        }

        if (rb.velocity.y == 0)
        {
            jump = false;
            doubleJump = false;
            animator.SetBool("jump", false);
        }
    }
    public void DoubleJump()
    {
        if (doubleJump == true)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                animator.SetBool("jump", true);
                rb.velocity = new Vector2(rb.velocity.x, Vector2.up.y * 13);
                doubleJump = false;
            }
        }
        if (rb.velocity.y == 0)
        {
            jump = false;
            doubleJump = false;
            animator.SetBool("jump", false);
        }
    }
    public void Jump2()
    {
        if (rb)
        {
            if (rb.velocity.y < 0 && groundCheck == false && doubleJump == false)
            {
                animator.SetBool("jump2", true);
            }
            if (rb.velocity.y == 0 || groundCheck)
            {
                animator.SetBool("jump2", false);

            }
            if((jump == false && rb.velocity.y < -3) ||( jump == true && doubleJump == true && rb.velocity.y < -18))
            {
                animator.SetBool("jump2", true);
            }
        }
    }
    public void Climb()
    {
        if (climb)
        {
            if (Input.GetKey(KeyCode.W))
            {
                animator.SetBool("climb", true);
                rb.gravityScale = 0;
                transform.position += new Vector3(0,Vector3.up.y*3*Time.deltaTime);
            }

            if (Input.GetKey(KeyCode.S))
            {
                animator.SetBool("climb", true);
                rb.gravityScale = 0;
                transform.position += new Vector3(0, Vector3.down.y * 3 * Time.deltaTime);

            }
        }
    }

    void MakeGhost()
    {
        if (ghost)
        {
            Instantiate(ghost, transform.position, Quaternion.identity).transform.localScale = transform.localScale;
        }
    }

    IEnumerator GhostVfx()
    {
        while (true)
        {
            MakeGhost();
            yield return new WaitForSeconds(0.04f);
        }

        
    }











    //Collison detected:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Ladder"))
        {
            groundCheck = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Goal"))
        {
            if (SceneManager.GetActiveScene().name.Equals("Level4"))
            {
                SceneManager.LoadScene("Title");
                return;
            }
            PlayerPrefs.SetInt((LevelConst.LEVEl_PASSED + currentLevel), 1);
            PlayerPrefs.SetInt(LevelConst.LEVEL_UNLOCKED + (currentLevel + 1), 1);
            SceneManager.LoadScene((currentLevel+1));


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Handle collision with the enemy
        if (collision.CompareTag("TriggerEnemy"))
        {
            collision.gameObject.transform.root.GetComponent<Enemy>().Die();
            if (killEnemyVfx)
            {
                Instantiate(killEnemyVfx, collision.gameObject.transform.root.transform.position, Quaternion.identity);
            }
            rb.velocity = Vector2.up * 13;
            animator.SetBool("jump" , true);
        }
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);
        }

        if (collision.CompareTag("ItemCherry"))
        {
            Destroy(collision.gameObject);
            if (itemVfx)
            {
                Instantiate(itemVfx, collision.gameObject.transform.position, Quaternion.identity);
            }
        }


        ////Handle collision with the lader
        if (collision.CompareTag("checkladder"))
        {
            collision.gameObject.transform.root.GetComponent<Collider2D>().isTrigger = true;
            
        }
        if (collision.CompareTag("aboveLadder"))
        {
            climb = true;
            rb.velocity = Vector3.zero;
            collision.gameObject.transform.root.GetComponent<Collider2D>().isTrigger = true;
        }
        if (collision.CompareTag("Ladder"))
        {
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
            if (!climb)
            {
                climb = true;
            }
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            climb = false;
            collision.GetComponent<Collider2D>().isTrigger = false;
            rb.gravityScale = 5;
            animator.SetBool("climb", false);
        }
    }

}
