using System;
using UnityEngine;

[CreateAssetMenu(fileName = "AnimationCurve", menuName = "ScriptableObjects/Animation/Curve/Curve", order = 1)]
public class AnimationCurveScriptable : ScriptableObject
{
    public CurveClass[] curves;
}

[Serializable]
public class CurveClass
{
    [Header("Ŀ�� �̸�")]
    public string curveName;
    [Header("X�� Ŀ��")]
    public AnimationCurve xCurve;
    [Header("X�� �̵��Ÿ�")]
    public float xDis;
    [Header("Y�� Ŀ��")]
    public AnimationCurve yCurve;
    [Header("Y�� �̵��Ÿ�")]
    public float yDis;
    [Header("Ŀ�갡 ���� �� ���� �ɸ��� �ð�")]
    public float curveTime;
}
