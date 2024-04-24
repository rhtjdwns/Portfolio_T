using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] AnimationCurve yCurveUp;
    public GameObject player;
    float time = 0;

    [Header("Shaking")]
    [SerializeField] float Impulse;
    [SerializeField] float Frequency;
    float shakeTime = 0;
    CinemachineVirtualCamera cineCamera;

    [Header("Reverse")]
    Vector3 originTransform;
    Vector3 reverseTransform;

    private void Start()
    {
        cineCamera = GetComponent<CinemachineVirtualCamera>();
    }

    void Update()
    {
        if (shakeTime <= 0)
        {
            if (player.transform.position.y > 1.5f)
            {
                CameraUp();
            }
            else if (player.transform.position.y < -1.5f)
            {
                CameraDown();
            }
        }
    }

    void CameraUp()
    {
        if (this.transform.position.y <= 0.4f)
        {
            if (time < 1)
                time += Time.deltaTime;
            this.transform.position = new Vector3(0, yCurveUp.Evaluate(time), this.transform.position.z);
        }
    }

    void CameraDown()
    {
        if (this.transform.position.y >= -0.4f)
        {
            if (time > 0)
                time -= Time.deltaTime;
            this.transform.position = new Vector3(0, yCurveUp.Evaluate(time), this.transform.position.z);
        }
    }

    public void VibrateForTime(float times)
    {
        shakeTime = times;
        StartCoroutine(CameraShaking());
    }

    IEnumerator CameraShaking()
    {
        while (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            cineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = Frequency;
            cineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = Impulse;
            yield return 0;
        }
        cineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0;
        cineCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        yield return 0;
    }

    // 카메라 반전
    public void CameraReverse()
    {
        reverseTransform = new Vector3(transform.position.x, transform.position.y, 10);
        this.transform.position = reverseTransform;
        this.transform.rotation = new Quaternion(180, 0, 0, 0);
    }

    // 카메라 복구
    public void CameraRestore()
    {
        originTransform = new Vector3(transform.position.x, transform.position.y, -10);
        this.transform.position = originTransform;
        this.transform.rotation = new Quaternion(0, 0, 0, 0);
    }
}
