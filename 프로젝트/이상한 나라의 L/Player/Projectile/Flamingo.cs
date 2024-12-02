using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamingo : MonoBehaviour
{
    public AnimationCurve SwishCurve;
    Vector3 initPosition;
    Transform playerPos;
    float time = 0;
    float damage = 0;
    Vector3 fb;
    public float speed;

    public void Init(float dmg, Player p)
    {
        playerPos = p.transform;
        transform.position = new Vector3(playerPos.position.x + 1.74f, playerPos.position.y + 2f);
        initPosition = new Vector3(playerPos.position.x + 0.5f, playerPos.position.y);
        damage = dmg;
    }

    void Update()
    {
        SwishFlamingo();
    }

    void SwishFlamingo()
    {
        time += Time.deltaTime;

        if (time <= 0.12f)
            fb = Vector3.forward;
        else if (time > 0.12f)
            fb = Vector3.back;

        transform.RotateAround(initPosition, fb, SwishCurve.Evaluate(time) * Time.unscaledDeltaTime * speed);
        if (SwishCurve.Evaluate(time) == 0)
        {
            playerPos.GetComponent<Animator>().SetBool("IsFlamingo", false);
            playerPos.GetComponent<Player>().SetIsMove(true);
            playerPos.GetComponent<Player>().SetIsUse(false);
            time = 0;
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.GetComponent<ITakeDamage>().TakeDamage(damage);
    }
}
