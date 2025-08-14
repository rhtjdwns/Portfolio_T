using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TracePlayerName : MonoBehaviour
{
    private Transform target;
    private Vector3 offset;

    public void Init(Transform target, Vector3 offset)
    {
        this.target = target;
        this.offset = offset;
    }

    private void Update()
    {
        if (target != null)
        {
            transform.position = target.position + offset;
        }
    }
}
