using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEnemyVfx : VfxController
{
    public override void Start()
    {
        Destroy(gameObject,0.4f);
    }
}
