using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgeDead : MonoBehaviour
{
    void Update()
    {
        if (GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).IsName("HedgehogDead") &&
            GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
    }
}
