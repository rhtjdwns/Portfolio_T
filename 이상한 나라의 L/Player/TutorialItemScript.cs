using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialItemScript : MonoBehaviour
{
    public WeaponType type;
    public float speed;

    private void Update()
    {
        transform.position = new Vector3(0, 0, 0);

        if (this.transform.position.x < -13 || this.transform.position.x > 13 || this.transform.position.y > 7 || this.transform.position.y < -7)
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<TutorialOnceWeapon>().ItemToPlayer(type);
        GameObject.FindObjectOfType<Tutorial>().StartChat();
        ObjectPoolManager.ReturnObjectToPool(gameObject);
    }
}
