using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Watch : MonoBehaviour
{
    Animator anim;
    float duration = 0;
    public float time = 0;

    public void Init(float duration, Transform p)
    {
        this.duration = duration;
        p.GetComponent<Player>().SetIsUse(false);
        StartCoroutine(DurationCheck());
    }

    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (time >= duration)
            anim.SetBool("isEnd", true);

        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Watch_Explosion") &&
            anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
        {
            time = 0;
            ObjectPoolManager.ReturnObjectToPool(this.gameObject);
        }
    }

    IEnumerator DurationCheck()
    {
        while (true)
        {
            time += Time.unscaledDeltaTime;

            if (time < duration)
                Time.timeScale = 0.05f;
            else if (time >= duration)
                Time.timeScale = 1;

            yield return new WaitForSecondsRealtime(0.001f);
        }
    }
}
