using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMove : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        transform.position = Camera.main.transform.position + new Vector3(0,0,2);
    }
}
