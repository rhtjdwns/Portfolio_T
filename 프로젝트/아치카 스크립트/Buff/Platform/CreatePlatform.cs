using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class CreatePlatform : MonoBehaviour
{
    [System.Serializable]
    private class buffInfoList
    {     
        public List<Define.BuffInfo> infoList = new List<Define.BuffInfo>(); // ���� ���� ����Ʈ
    }

    #region ����
    // �÷��� ���� ����Ʈ
    [SerializeField] private List<buffInfoList> _platformList = new List<buffInfoList>();
    private List<BuffPlatform> _curPlatformList = new List<BuffPlatform>();

    // ������
    [SerializeField] private GameObject _platformPrefab;

    // ���� ���(�ڵ� ��ü)
    [SerializeField] private bool _isAutoMode = true;

    // �÷����� �ٲ�� �ð�
    [SerializeField] private float _changeTime; // �ٲ�� �ð�
    private float _timer;

    private int _index = 0;
    #endregion

    #region ������Ƽ 
    public List<BuffPlatform> CurPlatformList { get => _curPlatformList; }
    public bool IsAutoMode { get => _isAutoMode; set => _isAutoMode = value; }
    public int Index
    {
        get => _index;
        set
        {
            _index = value;
            _index = _index % _platformList.Count;
        }
    }
    #endregion;

    private void Awake()
    {
        // ������ �÷����� �����ϸ� ������ �߰�
        if (_curPlatformList.Count <= 0) 
        {
            int i = 0;

            foreach (Transform t in transform)
            {
                t.GetComponent<BuffPlatform>().Change(_platformList[0].infoList[i]);
                _curPlatformList.Add(t.GetComponent<BuffPlatform>());

                i++;
            }
        }
    }

    void Update()
    {
        if (_isAutoMode) // ���� ����� ��
        {
            if (_changeTime == 0 || _platformList.Count < 2) return;

            if (_timer >= _changeTime)
            {
                ChangeToNext();
                _timer = 0;
            }
            else
            {
                _timer += Time.deltaTime;
            }
        }
        else // ���� ��尡 �ƴ� ��
        {

        }
       
    }

#if UNITY_EDITOR
    // �ʱ� �÷��� ���� �Լ�(������ �󿡼� ���)
    public void Create()
    {
        if (_platformList.Count == 0) return;
        if (_platformList[_index].infoList.Count == 0) return;

        Delete(); // ���� �� ����

        float width = _platformPrefab.GetComponent<Renderer>().bounds.size.x;
        int half = _platformList[_index].infoList.Count / 2;
        float posX = transform.position.x - (width * half);

        if (_platformList[_index].infoList.Count % 2 == 0) // ���� ������ ¦���� ���
        {
            posX += width / 2;
        }

        for (int i = 0; i< _platformList[_index].infoList.Count; i++) // �÷��� ����
        {
            GameObject temp = Instantiate(_platformPrefab, new Vector3(posX, transform.position.y, transform.position.z), Quaternion.identity, transform);
            temp.GetComponent<BuffPlatform>().ChangeColor(_platformList[_index].infoList[i]);
            posX += width;
        }
    }

    // ������ �÷��� ���� �Լ�
    public void Delete()
    {
        for (int i = transform.childCount-1; i>=0;i--)
        {
            Undo.DestroyObjectImmediate(transform.GetChild(i).gameObject);
        }
    }
#endif

    // ���� �÷��� ������ ��ü �Լ�
    private void ChangeToNext() 
    {
        Index++;

        for (int i = 0; i < _curPlatformList.Count; i++)
        {
            _curPlatformList[i].Change(_platformList[_index].infoList[i]);
        }       
    }

    // �÷��� ��ü �Լ�
    public void Change(int index, Define.BuffInfo info) 
    {
        if (_curPlatformList.Count <= 1) return;

        _curPlatformList[index].Change(info);
    }

    // ������ �÷��� ũ�� �����ִ� �뵵
    private void OnDrawGizmos()
    {
        if (_platformList.Count == 0) return;

        Vector3 size = _platformPrefab.GetComponent<Renderer>().bounds.size;
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, new Vector3(size.x * _platformList[0].infoList.Count, size.y, size.z));
    }
}
