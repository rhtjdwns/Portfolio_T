using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeTurtle : MonoBehaviour
{
    GameObject player;
    int objSize;
    int objNum;
    float circleR = 1.5f;   //반지름
    float deg = 45;         //각도
    float objSpeed = 100;   //원운동 속도

    float time = 0;

    public void Init(Player p, int turtleNum, int posType)
    {
        objNum = turtleNum;
        player = p.gameObject;

        objSize = posType;
        player.GetComponent<Player>().SetIsUse(false);

        StartCoroutine(Duration());
    }

    void Update()
    {
        deg += Time.deltaTime * objSpeed;
        if (deg < 360)
        {
            var rad = Mathf.Deg2Rad * (deg + (objNum * (360 / objSize)));
            var x = circleR * Mathf.Sin(rad);
            var y = circleR * Mathf.Cos(rad);
            transform.position = player.transform.position + new Vector3(x, y);
            transform.rotation = Quaternion.Euler(0, 0, (deg + (objNum * (360 / objSize))) * -1);
        }
        else
        {
            deg = 0;
        }
    }

    IEnumerator Duration()
    {
        while (true)
        {
            time += Time.deltaTime;
            if (time >= 10)
            {
                time = 0;
                ObjectPoolManager.ReturnObjectToPool(this.gameObject);
            }
            yield return new WaitForSeconds(0.001f);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == null || collision.gameObject.layer == 8)
            return;

        ObjectPoolManager.ReturnObjectToPool(collision.gameObject);
    }

    public void TakeDamage(float value)
    {
    }
}
