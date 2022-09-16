using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public float enemyMoveSpeed;
    public abstract void Die();
}
