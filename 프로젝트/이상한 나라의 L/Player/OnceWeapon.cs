using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType
{
    Jabberwocky,
    Hedgehog,
    Flamingo,
    Faketurtle,
    Heart,
    Watch,
    Key,
}

public class OnceWeapon : BaseWeapon
{
    [Header("Projectile Option")]
    public GameObject flamingoPrefab;
    public GameObject turtlePrefab;
    public GameObject hedgehogPrefab;
    public GameObject jabberwockyPrefab;
    public GameObject watchPrefab;

    Player player;
    public float time = 0;


    Dictionary<WeaponType, int> itemInfo;

    private void Start()
    {
        player = GetComponentInParent<Player>();
        itemInfo = new Dictionary<WeaponType, int>();

        for (int i = 0; i < MaxOnceItem; ++i)
        {
            itemInfo.Add((WeaponType)i, 1);
        }
    }

    // Jabberwocky
    public void DamageBeam()
    {
        AttackPoint = 0.15f;
        Duration = 5;
        int level = GetLevel((int)WeaponType.Jabberwocky);

        switch (level)
        {
            case 1:
                ObjectPoolManager.SpawnObject(jabberwockyPrefab, new Vector3(0, 30), new Quaternion()).
                    GetComponent<Jabberwocky>().Init(player, Duration, AttackPoint, 1, 1);
                break;
            case 2:
                ObjectPoolManager.SpawnObject(jabberwockyPrefab, new Vector3(0, 30), new Quaternion()).
                    GetComponent<Jabberwocky>().Init(player, Duration, AttackPoint, 2, 1);
                ObjectPoolManager.SpawnObject(jabberwockyPrefab, new Vector3(0, 30), new Quaternion()).
                    GetComponent<Jabberwocky>().Init(player, Duration, AttackPoint, 2, 2);
                break;
            case 3:
            default:
                ObjectPoolManager.SpawnObject(jabberwockyPrefab, new Vector3(0, 30), new Quaternion()).
                    GetComponent<Jabberwocky>().Init(player, Duration, AttackPoint, 3, 1);
                ObjectPoolManager.SpawnObject(jabberwockyPrefab, new Vector3(0, 30), new Quaternion()).
                    GetComponent<Jabberwocky>().Init(player, Duration, AttackPoint, 3, 2);
                ObjectPoolManager.SpawnObject(jabberwockyPrefab, new Vector3(0, 30), new Quaternion()).
                    GetComponent<Jabberwocky>().Init(player, Duration, AttackPoint, 3, 3);
                break;
        }

        SetLevel(level, 1, false);
    }

    // Hedgehog
    public void ThrowBullet()
    {
        ProjectileSpeed = 10;
        int level = GetLevel((int)WeaponType.Hedgehog);

        switch (level)
        {
            case 1:
                AttackPoint = 30;
                break;
            case 2:
                AttackPoint = 50;
                break;
            case 3:
            default:
                AttackPoint = 70;
                break;
        }

        ObjectPoolManager.SpawnObject(hedgehogPrefab, new Vector3(), new Quaternion()).
            GetComponent<Hedgehog>().Init(player, AttackPoint, ProjectileSpeed);

        SetLevel(level, 1, false);
    }

    // Flamingo
    public void AreaBullet()
    {
        int level = GetLevel((int)WeaponType.Flamingo);

        switch (level)
        {
            case 1:
                AttackPoint = 20;
                break;
            case 2:
                AttackPoint = 30;
                break;
            case 3:
            default:
                AttackPoint = 40;
                break;
        }

        ObjectPoolManager.SpawnObject(flamingoPrefab, new Vector3(), new Quaternion()).
            GetComponent<Flamingo>().Init(AttackPoint, player);
    }

    // Faketurtle
    public void GuardBullet()
    {
        int level = GetLevel((int)WeaponType.Faketurtle);

        switch (level)
        {
            case 1:
                ObjectPoolManager.SpawnObject(turtlePrefab, new Vector3(), new Quaternion()).
                    GetComponent<FakeTurtle>().Init(player, 0, 1);
                break;
            case 2:
                ObjectPoolManager.SpawnObject(turtlePrefab, new Vector3(), new Quaternion()).
                    GetComponent<FakeTurtle>().Init(player, 0, 2);
                ObjectPoolManager.SpawnObject(turtlePrefab, new Vector3(), new Quaternion()).
                    GetComponent<FakeTurtle>().Init(player, 1, 2);
                break;
            case 3:
            default:
                ObjectPoolManager.SpawnObject(turtlePrefab, new Vector3(), new Quaternion()).
                    GetComponent<FakeTurtle>().Init(player, 0, 3);
                ObjectPoolManager.SpawnObject(turtlePrefab, new Vector3(), new Quaternion()).
                    GetComponent<FakeTurtle>().Init(player, 1, 3);
                ObjectPoolManager.SpawnObject(turtlePrefab, new Vector3(), new Quaternion()).
                    GetComponent<FakeTurtle>().Init(player, 2, 3);
                break;
        }

        SetLevel(level, 1, false);
    }

    // Heart
    public void HeartUp()
    {
        player.SetHp(itemInfo[WeaponType.Heart]);
        player.SetIsUse(false);
    }

    // Watch
    public void TimeStop()
    {
        int level = GetLevel((int)WeaponType.Watch);
        switch (level)
        {
            case 1:
                Duration = 3;
                break;
            case 2:
                Duration = 5;
                break;
            case 3:
            default:
                Duration = 8;
                break;
        }

        ObjectPoolManager.SpawnObject(watchPrefab, new Vector3(), new Quaternion()).
            GetComponent<Watch>().Init(Duration, player.transform);

        SetLevel(level, 1, false);
    }

    public void OnUse(int type)
    {
        WeaponType w = (WeaponType)type;

        player.SetIsUse(true);
        switch (w)
        {
            case WeaponType.Jabberwocky:
                DamageBeam();
                break;
            case WeaponType.Hedgehog:
                ThrowBullet();
                break;
            case WeaponType.Flamingo:
                StartCoroutine(IdleFlamingo());
                break;
            case WeaponType.Faketurtle:
                GuardBullet();
                break;
            case WeaponType.Heart:
                HeartUp();
                break;
            case WeaponType.Watch:
                TimeStop();
                break;
        }
        itemInfo[w] = 1;
    }

    public int GetLevel(int itemname)
    {
        int value;
        itemInfo.TryGetValue((WeaponType)itemname, out value);

        return value;
    }
    
    public void SetLevel(int itemname, int level, bool isPlus)
    {
        if (isPlus)
            itemInfo[(WeaponType)itemname] += level;
        else
            itemInfo[(WeaponType)itemname] = level;
    }

    IEnumerator IdleFlamingo()
    {
        while (true)
        {
            time += Time.deltaTime;

            if (time >= 0.025f)
            {
                AreaBullet();
                time = 0;
                break;
            }

            yield return new WaitForSeconds(0.01f);
        }
    }

    public void ItemToPlayer(WeaponType type)
    {
        player.AddOnceWeapon((int)type);
    }
}
