using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flag : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            NetworkDirector.Instance.SendWinner();
            GameDirector.Instance.OnGameEndUI(1);
        }
    }
}
