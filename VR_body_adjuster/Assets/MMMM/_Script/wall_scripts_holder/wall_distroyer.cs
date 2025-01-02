using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wall_distroyer : MonoBehaviour
{

    public int destroying_timer;

    private void Start()
    {
        StartCoroutine(destroyingthisobject());
    }

    IEnumerator destroyingthisobject()
    {
        yield return new WaitForSeconds(destroying_timer);

        Destroy(this.gameObject);

    }

}
