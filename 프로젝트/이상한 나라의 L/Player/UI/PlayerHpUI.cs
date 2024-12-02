using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpUI : MonoBehaviour
{
    public GameObject[] HpUis;
    int count = 0;

    public void CheckHp()
    {
        HpUis[count++].SetActive(false);
    }
}
