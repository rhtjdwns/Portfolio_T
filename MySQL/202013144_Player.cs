using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    float playerSpeed = 7f;
    float jumpPower = 7f;
    Rigidbody2D rid2D;
    bool isJump = true;

    void Start()
    {
        StartCoroutine(SendMyPos());
        rid2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rid2D.AddForce(new Vector2(-playerSpeed, 0), ForceMode2D.Force);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            rid2D.AddForce(new Vector2(playerSpeed, 0), ForceMode2D.Force);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isJump)
        {
            rid2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            isJump = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Floor") || collision.gameObject.tag.Equals("Map"))
        {
            isJump = true;
        }
    }

    IEnumerator SendMyPos()
    {
        while (true)
        {
            NetworkDirector.Instance.SendPos(this.transform.position.x, this.transform.position.y);

            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
}
