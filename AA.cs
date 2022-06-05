using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AA : MonoBehaviour
{
    void OnDrawGizmos()
    {
        Renderer r = GetComponent<Renderer>();
        if (r)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireCube(r.bounds.center, r.bounds.size);
        }
    }
}
