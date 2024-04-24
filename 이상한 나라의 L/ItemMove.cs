using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    public WeaponType type;
    public float speed;
    float moveDir;
    int randNum;

    private void OnEnable()
    {
        randNum = Random.Range(0, 3);

        switch (randNum)
        {
            case 0:
            default:
                moveDir = 0;
                break;
            case 1:
                moveDir = 1;
                break;
            case 2:
                moveDir = -1;
                break;
        }
    }

    private void Start()
    {
        randNum = Random.Range(0, 3);

        switch (randNum)
        {
            case 0:
            default:
                moveDir = 0;
                break;
            case 1:
                moveDir = 1;
                break;
            case 2:
                moveDir = -1;
                break;
        }

    }

    private void Update()
    {
        if (Time.timeScale != 0)
            transform.Translate(new Vector3(-1, moveDir, 0) * speed * Time.unscaledDeltaTime);
        else if (Time.timeScale == 0)
            transform.Translate(new Vector3(-1, moveDir, 0) * speed * Time.deltaTime);

        if (this.transform.position.x < -13 || this.transform.position.x > 13 || this.transform.position.y > 7 || this.transform.position.y < -7)
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);

        CameraWorldSpace();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<OnceWeapon>().ItemToPlayer(type);
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }

    void CameraWorldSpace()
    {
        Vector3 worldpos = Camera.main.WorldToViewportPoint(this.transform.position);
        if (worldpos.y < 0.1f)
        {
            worldpos.y = 0.1f;
            moveDir = -moveDir;
        }
        if (worldpos.y > 0.90f)
        {
            worldpos.y = 0.90f;
            moveDir = -moveDir;
        }
        this.transform.position = Camera.main.ViewportToWorldPoint(worldpos);
    }
}
