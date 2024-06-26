using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : BaseWeapon
{
    public GameObject bulletSpawner;
    bool isAttack = false;
    bool attackTime = true;
    public float time;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Update()
    {
        time++;
        if (time > 100f)
            time = 0;
        Random.InitState((int)time);
        if (Time.timeScale != 0)
            CreateProjectile();
    }

    public IEnumerator OnAttack()
    {
        yield return new WaitForSecondsRealtime(AttackSpeed);
    
        if (!attackTime) attackTime = true;
    }

    void CreateProjectile()
    {
        if (isAttack && attackTime)
        {
            Fireball fire = ObjectPoolManager.SpawnObject(ProjectileObject, 
                new Vector2(bulletSpawner.transform.position.x + 0.4f, bulletSpawner.transform.position.y),
                bulletSpawner.transform.rotation).GetComponent<Fireball>();
            fire.Init(ProjectileSpeed, AttackPoint, MaxAttack);
            StartCoroutine(OnAttack());
            attackTime = false;
        }
    }

    public void OnShot(bool isAtk)
    {
        isAttack = isAtk;
    }

    public bool GetIsAttack()
    {
        return isAttack;
    }
}
