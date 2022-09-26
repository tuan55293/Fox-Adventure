using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    public virtual void Show(bool show)
    {
        gameObject.SetActive(show);
    }
}
