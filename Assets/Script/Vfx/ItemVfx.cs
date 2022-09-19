using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemVfx : VfxController
{
    // Start is called before the first frame update
    public override void Start()
    {
        Destroy(gameObject, 0.4f);
    }
}
