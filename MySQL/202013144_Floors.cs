using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Floors : MonoBehaviour
{
    DateTime startTime;
    DateTime endTime;
    TimeSpan ts;

    void Start()
    {
        startTime = DateTime.Now;
    }

    void Update()
    {
        endTime = DateTime.Now;
        ts = endTime - startTime;

        if (ts.TotalSeconds > 10f)
        {
            Destroy(this.gameObject);
        }
    }
}
