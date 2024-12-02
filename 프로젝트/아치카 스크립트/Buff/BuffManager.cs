using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : Singleton<BuffManager>
{

    [SerializeField] private List<BuffData> _buffStorage = new List<BuffData>(); // ���� �����

    private List<BuffData> _currentBuffList = new List<BuffData>();              // ���� ���� ����Ʈ
    private List<BuffData> _removeBuffList = new List<BuffData>();               // ���� ���� ����Ʈ


    private void Update()
    {

        foreach (BuffData data in _currentBuffList)
        {
            data.Stay();
        }
         
        // ���� ���� ����
        if (_removeBuffList.Count <= 0) return;

        foreach (BuffData data in _removeBuffList)
        {
            _currentBuffList.Remove(data);
        }

        _removeBuffList.Clear();
    }

    // ���� �߰�
    public void AddBuff(Define.BuffInfo buff) 
    {

        if (GetCurretBuff(buff) != null) // ���� ���� ������ �ִ��� Ȯ��
        {
            RemoveBuff(buff); // ���� ���� ����
        }

        BuffData buffData = GetBuff(buff);

        if (buffData == null) return;
        
        buffData.Enter();
        _currentBuffList.Add(buffData);
    }

    // ���� ����
    public void RemoveBuff(Define.BuffInfo buff) // ���� �߰�
    {
        BuffData buffData = GetCurretBuff(buff);

        if (buffData == null) return;

        buffData.Exit();
        _removeBuffList.Add(buffData);
    }

    // ���� ����ҿ��� ã��
    public BuffData GetBuff(Define.BuffInfo info) 
    {
        foreach (BuffData buff in _buffStorage)
        {
            if (buff.info == info)
            {
                return buff;
            }
        }

        return null;
    }

    // ���� ���� ����Ʈ���� ã��
    private BuffData GetCurretBuff(Define.BuffInfo info)
    {
        foreach (BuffData buff in _currentBuffList)
        {
            if (buff.info == info)
            {
                return buff;
            }
        }

        return null;
    }
}