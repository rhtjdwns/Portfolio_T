using UnityEngine;

public class BuffPlatform : MonoBehaviour
{
    [SerializeField] private Define.BuffInfo _info = Define.BuffInfo.NONE;
    private BuffData _buffData;
    private bool _isEntered = false;


    // �÷��� ��ü �Լ�
    public void Change(Define.BuffInfo info) 
    {    
        Exit();

        _info = info;

        Material temp;
        if (_info == Define.BuffInfo.NONE)
        {
            temp = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            temp.color = Color.white;         
        }
        else
        {
            _buffData = BuffManager.Instance.GetBuff(_info);
            _buffData.Platform = this;

            temp = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            temp.color = _buffData.color;
        }

        if (Application.isPlaying)
        {
            GetComponent<Renderer>().material = temp;
        }
        else
        {
            GetComponent<Renderer>().sharedMaterial = temp;
        }
    }

    // �÷��� �� ���� �Լ�(������ �󿡼� �̸� ���� �����ϴ� �뵵)
    public void ChangeColor(Define.BuffInfo info)
    {
        Material temp;
        if (info == Define.BuffInfo.NONE)
        {
            temp = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            temp.color = Color.white;
        }
        else
        {
            temp = new Material(Shader.Find("Universal Render Pipeline/Lit"));
            temp.color = BuffManager.Instance.GetBuff(info).color;
        }

        if (Application.isPlaying)
        {
            GetComponent<Renderer>().material = temp;
        }
        else
        {
            GetComponent<Renderer>().sharedMaterial = temp;
        }
    }


    // �÷����� ������ ��
    private void Enter()
    {
        if (!_isEntered)
        {
            if (_info == Define.BuffInfo.NONE) return;

            if (BuffManager.Instance.GetBuff(_info).type != Define.BuffType.EXIT)
            {
                BuffManager.Instance.AddBuff(_info);
            }

            _isEntered = true;
        }
    }

    // �÷������� ������ ��
    private void Exit()
    {
        if (_isEntered)
        {
            if (_info == Define.BuffInfo.NONE) return;

            if (BuffManager.Instance.GetBuff(_info).type == Define.BuffType.EXIT)
            {
                BuffManager.Instance.AddBuff(_info);
            }
            else if (BuffManager.Instance.GetBuff(_info).type == Define.BuffType.STAY)
            {
                BuffManager.Instance.RemoveBuff(_info);
            }

            _isEntered = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Enter();
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Exit();
        }

    }
}
