using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformDestory : MonoBehaviour
{
    [Header("�ı� ���� �ɸ��� �ð�")]
    [SerializeField] float destoryTime = 0f;

    private CameraController controller;

    private void Awake()
    {
        controller = FindObjectOfType<CameraController>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            StartCoroutine(DestoryPlatform());
        }
    }

    IEnumerator DestoryPlatform()
    {
        yield return new WaitForSeconds(destoryTime);

        controller.SetCameraSetting(Define.CameraType.NONFOLLOW);
        GetComponent<Collider>().isTrigger = true;
    }
}
