using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraShake", menuName = "ScriptableObjects/Camera Shake", order = 0)]
public class CameraShakeProfile : ScriptableObject
{
    [Header("Shake Settings")]
    [Tooltip("��鸲 �ð�")]
    public float ShakeTime = 0.2f;
    [Tooltip("��鸲 ��")]
    public float ShakeForce = 1.0f;
    [Tooltip("��鸲 ����")]
    public Vector3 Velocity = new Vector3(0, -1f, 0);
    [Tooltip("��鸲 Ŀ����")]
    public bool ShakeCustom = false;
    [Tooltip("��鸲 �ӵ� �")]
    public AnimationCurve ShakeCurve;

    [Space]
    [Tooltip("��鸲 ����")]
    public float ShakeAmplitude = 1.0f;
    [Tooltip("��鸲 ��")]
    public float ShakeFrequency = 1.0f;
    [Tooltip("��鸲 ����")]
    public float ShakeDuration = 1.0f;
}
