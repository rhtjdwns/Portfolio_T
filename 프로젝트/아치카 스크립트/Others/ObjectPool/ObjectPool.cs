using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ObjectPool : Singleton<ObjectPool>
{
    [Header("�̸� ������ ��ü�� ������")]
    [SerializeField] private List<NameingClass> _prefabStorges = new List<NameingClass>();
    //[SerializeField] private List<GameObject> _prefabStorage = new List<GameObject>();
    [Header("�ʱ� ������ ��ü�� ��")]
    [SerializeField] private int _initialSize = 10;

    private Dictionary<string, Queue<GameObject>> pool = new Dictionary<string, Queue<GameObject>>();
    private CharacterManager _characterManager;

    private void Awake()
    {
        _characterManager = FindObjectOfType<CharacterManager>();

        foreach (NameingClass nc in _prefabStorges)
        {
            foreach (GameObject prefab in nc.objects)
            {
                CreatePool(prefab);
            }
        }

        //foreach (GameObject prefab in _prefabStorage)
        //{
        //    CreatePool(prefab);
        //}
    }

    public void CreatePool(GameObject prefab)
    {
        if (!FindPrefab(prefab.name))
        {
            _prefabStorges[_prefabStorges.Count].objects.Add(prefab);
        }

        //if (!FindPrefab(prefab.name))
        //{
        //    _prefabStorage.Add(prefab);
        //}

        Queue<GameObject> queue = new Queue<GameObject>();

        GameObject poolObj = new GameObject(prefab.name + "Pool");
        poolObj.transform.SetParent(transform);

        for (int i = 0; i < _initialSize; i++)
        {
            GameObject obj = Instantiate(prefab, poolObj.transform);

            obj.SetActive(false);
            queue.Enqueue(obj);
        }
        pool.Add(prefab.name, queue);

    }

    // ��ü�� Ǯ���� ��������
    public GameObject Spawn(string name, float t = 0, Transform parent = null)
    {
        if (!FindPool(name))
        {
            return null;
        }

        GameObject obj;

        if (pool[name].Count > 0)
        {
            obj = pool[name].Dequeue();
            obj.SetActive(true);
        }
        else
        {
            // Ǯ�� ��ü�� ������ ���� ����
            obj = Instantiate(GetPrefab(name), transform);
            Reset(obj);
        }

        CharacterBase ch;
        if (obj.TryGetComponent<CharacterBase>(out ch))
        {
            _characterManager.AddCharacter(ch.gameObject);
        }

        if (parent)
        {
            obj.transform.SetParent(parent);
            obj.transform.localPosition = Vector3.zero;
        }

        if (t != 0)
        {
            StartCoroutine(ReturnObj(obj, t));
        }

        return obj;
    }

    // ��ü�� Ǯ�� ��ȯ�ϱ�
    public void Remove(GameObject obj)
    {
        Reset(obj);
        obj.SetActive(false);
        pool[RemoveClone(obj.name)].Enqueue(obj);
    }

    private IEnumerator ReturnObj(GameObject obj, float t)
    {
        yield return new WaitForSeconds(t);

        if (obj.activeSelf)
        {
            Remove(obj);
        }

    }

    private void Reset(GameObject obj)
    {
        string poolName = RemoveClone(obj.name) + "Pool";

        obj.transform.SetParent(GetPool(poolName));
        //obj.transform.localPosition = Vector3.zero;
        //obj.transform.localRotation = Quaternion.identity;
        //obj.transform.localScale = Vector3.one;
    }

    public GameObject GetPrefab(string name)
    {
        foreach (NameingClass nc in _prefabStorges)
        {
            foreach (GameObject prefab in nc.objects)
            {
                if (prefab.name == name)
                {
                    return prefab;
                }
            }
        }

        return null;

        //foreach (GameObject prefab in _prefabStorage)
        //{
        //    if (prefab.name == name)
        //    {
        //        return prefab;
        //    }
        //}

        //return null;
    }

    public bool FindPrefab(string name)
    {
        foreach (NameingClass nc in _prefabStorges)
        {
            foreach (GameObject prefab in nc.objects)
            {
                if (prefab.name == name)
                {
                    return true;
                }
            }
        }

        return false;

        //foreach (GameObject prefab in _prefabStorage)
        //{
        //    if (prefab.name == name)
        //    {
        //        return true;
        //    }
        //}

        //return false;
    }

    public Transform GetPool(string name)
    {
        foreach (Transform pool in transform)
        {
            if (pool.name == name)
            {
                return pool;
            }
        }

        return transform;
    }

    public bool FindPool(string name)
    {
        StringBuilder stringBuilder = new StringBuilder();

        foreach (Transform pool in transform)
        {
            stringBuilder.Append(name);
            stringBuilder.Append("Pool");

            if (stringBuilder.Equals(pool.name))
            {
                return true;
            }
            stringBuilder.Clear();
        }

        return false;
    }


    private string RemoveClone(string name)
    {
        int index = name.IndexOf("(Clone)");
        if (index > 0)
            name = name.Substring(0, index);

        return name;
    }
}

[Serializable]
public class NameingClass
{
    public string name;
    [SerializeField] public List<GameObject> objects = new List<GameObject>(); 
}
