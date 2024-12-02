using System;
using System.Collections.Generic;
using UnityEngine;

public enum StateType
{
	Enemy,
	Player
}

[Serializable]
public struct BooleanArray
{
	public bool[] array;
}

[CreateAssetMenu(fileName = "StateChangeConditions", menuName = "ScriptatbleObject/State/ChangeConditions")]
public class StateChangeConditions : ScriptableObject
{
	public StateType stateType;
	[SerializeField] public BooleanArray[] conditions;

	public bool GetChangable(int current, int next)
	{
		int length = conditions.Length - 1;

		if(current == next) { return false; }					// ���� State�� �����
		if(current < 0 || current > length) { return false; }	// current�� ������ ��� ��� �����(�߸��� �Է�)
		if(next < 0 || next > length) { return false; }        // next�� ������ ��� ��� �����(�߸��� �Է�)

		return conditions[current].array[next];
	}
}