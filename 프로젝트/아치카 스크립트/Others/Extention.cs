using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Extention
{

    //���� ��ǥ�� ĵ���� ��ǥ�� �ٲٴ� Ȯ�� �Լ�
    public static Vector2 WorldToCanvasPosition(this RectTransform rectTransform, string canvasName, Vector3 worldPosition, bool worldCamera = false)
    {
        Canvas canvas = GameObject.Find(canvasName).GetComponent<Canvas>();

        // 1. ���� ��ǥ�� ȭ�� ��ǥ�� ��ȯ
        Vector3 screenPos = Camera.main.WorldToScreenPoint(worldPosition);

        // 2. ȭ�� ��ǥ�� ĵ���� ��ǥ�� ��ȯ
        Vector2 localPoint;
        if (worldCamera)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPos, canvas.worldCamera, out localPoint);
        }
        else
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, screenPos, null, out localPoint);
        }

        return localPoint; // ĵ���� ���� ��ǥ ��ȯ
    }
}
