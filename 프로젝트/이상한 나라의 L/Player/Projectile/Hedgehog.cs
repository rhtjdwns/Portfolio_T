using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hedgehog : MonoBehaviour
{
    public GameObject deadAnim;
    float damage;
    float speed;
    float time = 0;

    public void Init(Player p, float damage, float projectileSpeed)
    {
        this.damage = damage;
        speed = projectileSpeed;
        time = 0;

        transform.position = new Vector3(p.transform.position.x + 0.5f, p.transform.position.y);
        p.GetComponent<Player>().SetIsUse(false);
    }

    void Update()
    {
        transform.Translate(Vector3.right * speed * Time.deltaTime);

        if (this.transform.position.x < -13 || this.transform.position.x > 13 || this.transform.position.y > 7 || this.transform.position.y < -7)
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.gameObject.GetComponent<ITakeDamage>().TakeDamage(damage);
        ObjectPoolManager.SpawnObject(deadAnim, new Vector3(transform.position.x + 3, transform.position.y, transform.position.z), transform.rotation);
        ObjectPoolManager.ReturnObjectToPool(this.gameObject);
    }
}
