using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public bool run;

    [HideInInspector]
    public bool crouch;

    [HideInInspector]
    public bool canClimb;

    [HideInInspector]
    public bool aboveladder;

    [HideInInspector]
    public bool jumped;

    [HideInInspector]
    public bool doubleJump;

    [HideInInspector]
    public bool hadDash;

    [HideInInspector]
    public bool die;

    int currentLevel;



    public GameObject ghost;
    PlayerJump playerJump;
    PlayerDoubleJump playerDoubleJump;
    PlayerDash playerDash;
    GameObject Ladder;


    [HideInInspector]
    public Rigidbody2D rb;

    [HideInInspector]
    public Animator animator;



    public Image dash;
    public KillEnemyVfx killEnemyVfx;
    public ItemVfx itemVfx;



    float moveDirectionX;
    public float moveSpeed;
    public bool groundCheck;


    public int DashEnergy;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerJump = GetComponent<PlayerJump>();
        playerDoubleJump = GetComponent<PlayerDoubleJump>();
        playerDash = GetComponent<PlayerDash>();
        if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            playerDoubleJump.enabled = false;
            playerJump.enabled = false;
            playerDash.enabled = false;
        }
        else
        {
            playerJump.enabled = false;
            playerDoubleJump.enabled = true;
            playerJump.enabled = true;
            playerDash.enabled = true;
        }
        animator = GetComponent<Animator>();
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        if (dash)
        {
            dash.fillAmount = 0;
        }
    }

    private void Update()
    {
        moveDirectionX = Input.GetAxisRaw("Horizontal");
        Jump2();
        Climb();
        FillDashEnergy();
    }

    void FixedUpdate()
    {
        if (hadDash == false)
        {
            Move();
        }
    }

    public void Move()
    {

        if (rb && die == false)
        {
            Flip();
            if (moveDirectionX == 0)
            {
                animator.SetBool("run", false);
                rb.velocity = new Vector2(Vector2.zero.x, rb.velocity.y);
            }
            if ((moveDirectionX < 0 || moveDirectionX > 0) && groundCheck)
            {
                animator.SetBool("run", true);
            }
            if ((moveDirectionX < 0 || moveDirectionX > 0) && groundCheck == false)
            {
                animator.SetBool("run", false);
            }
            rb.velocity = new Vector2(Vector2.right.x * moveSpeed * moveDirectionX, rb.velocity.y);
        }
    }

    public void Flip()
    {
        if (transform.localScale.x < 0 && moveDirectionX > 0)
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

    public void Jump2()
    {
        if (rb && die == false)
        {
            if (rb.velocity.y < 0 && groundCheck == false && doubleJump == false)
            {
                animator.SetBool("jump2", true);
            }
            if (rb.velocity.y == 0 || groundCheck)
            {
                animator.SetBool("jump2", false);

            }
            if ((jumped == false && rb.velocity.y < -3) || (jumped == true && doubleJump == true && rb.velocity.y < -18))
            {
                animator.SetBool("jump2", true);
            }
        }
    }
    public void Climb()
    {
        if (Input.GetKey(KeyCode.W) && (canClimb && die == false))
        {
            animator.SetBool("climb", true);
            hadDash = false;
            transform.position += new Vector3(0, Vector3.up.y * 3 * Time.deltaTime);
        }

        if ((Input.GetKey(KeyCode.S) && (canClimb && die == false)))
        {
            animator.SetBool("climb", true);
            hadDash = false;
            transform.position += new Vector3(0, Vector3.down.y * 3 * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S) && aboveladder)
        {
            aboveladder = false;
            if (Ladder)
            {
                Ladder.GetComponent<Collider2D>().isTrigger = true;
            }
            animator.SetBool("climb", true);
            hadDash = false;
            transform.position += new Vector3(0, Vector3.down.y * 3 * Time.deltaTime);
        }
    }


    void FillDashEnergy()
    {
        if (dash)
        {
            dash.fillAmount = DashEnergy / 15f;
        }
    }












    //Collison detected:
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            groundCheck = true;
        }
        if (collision.gameObject.CompareTag("Enemy"))
        {
            rb.gravityScale = 0;
            rb.velocity = Vector2.zero;
            gameObject.GetComponent<Collider2D>().isTrigger = true;
            animator.SetBool("die", true);
            die = true;
            GameManager.Ins.gameOver = true;
        }
        if (collision.gameObject.CompareTag("Goal"))
        {

            if (SceneManager.GetActiveScene().name.Equals("Level4"))
            {
                SceneManager.LoadScene("Title");
                return;
            }
            if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
            {
                GUIManager.Ins.ShowCompleteTutorialDialog();
                PlayerPrefs.SetInt((LevelConst.LEVEl_PASSED + "Tutorial"), 1);
                return;
            }
            PlayerPrefs.SetInt((LevelConst.LEVEl_PASSED + currentLevel), 1);
            PlayerPrefs.SetInt(LevelConst.LEVEL_UNLOCKED + (currentLevel + 1), 1);
            SceneManager.LoadScene(LevelConst.LEVEL_UNLOCKED + (currentLevel + 1));

        }

        if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.transform.SetParent(collision.gameObject.transform);
            groundCheck = true;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Platform"))
        {
            gameObject.transform.SetParent(null);
            groundCheck = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        //Handle collision with the enemy
        if (collision.CompareTag("TriggerEnemy"))
        {
            if(die == false)
            {
                collision.gameObject.transform.parent.GetComponent<Enemy>().Die();
                if (killEnemyVfx)
                {
                    Instantiate(killEnemyVfx, collision.gameObject.transform.parent.transform.position, Quaternion.identity);
                }
                rb.velocity = Vector2.up * 13;
                animator.SetBool("jump", true);
            }
        }
        if (collision.CompareTag("Obstacle") )
        {
            animator.SetBool("die", true);
            die = true;
            GameManager.Ins.gameOver = true;
        }
        if (collision.CompareTag("DeadZone"))
        {
            GameManager.Ins.GameOver();
        }

        //handle collision with item
        if (collision.CompareTag("ItemCherry"))
        {
            if (DashEnergy < 15)
            {
                DashEnergy++;
            }
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
            aboveladder = true;
            collision.gameObject.transform.root.GetComponent<Collider2D>().isTrigger = true;
        }
        if (collision.CompareTag("Ladder"))
        {
            Ladder = collision.gameObject;
            rb.velocity = Vector3.zero;
            rb.gravityScale = 0;
            if (!canClimb)
            {
                canClimb = true;
            }
        }

        //Trigger dialog tutorial
        if (collision.CompareTag("TriggerJump"))
        {
            playerJump.enabled = true;
            collision.gameObject.SetActive(false);
            GUIManager.Ins.ShowHelpDialogTriggerJumpTutorial();
        }
        if (collision.CompareTag("TriggerDoubleJump"))
        {
            playerJump.enabled = false;
            playerDoubleJump.enabled = true;
            playerJump.enabled = true;
            collision.gameObject.SetActive(false);
            GUIManager.Ins.ShowHelpDialogTriggerDoubleJumpTutorial();
        }
        if (collision.CompareTag("TriggerDash"))
        {
            playerDash.enabled = true;
            collision.gameObject.SetActive(false);
            GUIManager.Ins.dashPanel.SetActive(true);
            GUIManager.Ins.ShowHelpDialogTriggerDashTutorial();
        }
        if (collision.CompareTag("TriggerEnemyHelp"))
        {
            collision.gameObject.SetActive(false);
            GUIManager.Ins.ShowHelpDialogTriggerEnemyHelpTutorial();
        }
        if (collision.CompareTag("TriggerClimb"))
        {
            collision.gameObject.SetActive(false);
            GUIManager.Ins.ShowHelpDialogTriggerClimbTutorial();
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            canClimb = false;
            collision.GetComponent<Collider2D>().isTrigger = false;
            rb.gravityScale = 4;
            animator.SetBool("climb", false);
            animator.SetBool("jump2", true);
        }
        if (collision.CompareTag("aboveLadder"))
        {
            aboveladder = false;
        }
    }
}
