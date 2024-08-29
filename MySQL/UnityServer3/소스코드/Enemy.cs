using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    float eX, eY;

    private void Start()
    {
        eX = this.transform.position.x;
        eY = this.transform.position.y;
    }

    void Update()
    {
        transform.position = new Vector2(eX, eY);
    }

    public void SetPosition(float x, float y)
    {
        eX = x;
        eY = y;
    }
}
