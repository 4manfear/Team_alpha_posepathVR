using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checking_set_and_get : MonoBehaviour
{
    private int name1;

    public int Name1
    {
        get { return name1; }
        set { name1 = value; }

    }

    private void Start()
    {
        Debug.Log(name1);
    }
}
