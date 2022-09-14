using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogEnemyController : MonoBehaviour
{
    [SerializeField] Vector2 DistanceEnemyMove;
    public Vector2 leftLimit;
    public Vector2 rightLimit;
    public bool canleft;
    public bool canright;
    [SerializeField] float enemyMoveSpeed;
    Vector2 originalPos;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        originalPos = transform.position;
        leftLimit = originalPos - DistanceEnemyMove;
        rightLimit = originalPos + DistanceEnemyMove;
        rb.velocity = Vector2.right * enemyMoveSpeed;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
        Physics2D.IgnoreLayerCollision(8,8,true);

    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x > rightLimit.x && canleft == false)
        {
            canleft = true;
            if (canleft)
            {
                canright = false;
                rb.velocity = Vector2.zero;
                if(rb.velocity == Vector2.zero)
                {
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                }
                rb.velocity = Vector2.left * enemyMoveSpeed;     
            }
        }

        if (transform.position.x < leftLimit.x && canright == false)
        {
            canright = true;

            if (canright)
            {
                canleft = false;
                rb.velocity = Vector2.zero;
                if (rb.velocity == Vector2.zero)
                {
                    transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
                }
                rb.velocity = Vector2.right * enemyMoveSpeed;   
            }
        }

    }
}
