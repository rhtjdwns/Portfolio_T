using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class FloorGeneator : MonoBehaviour
{
    public GameObject floorObject;

    float x, y;
    int amountFloor = 8;
    bool istime = true;

    int seed;

    // X 8.3 -8.4 Y 2.75 2.75
    void Start()
    {
        Thread timeThread = new Thread(TimeThread);
        seed = int.Parse(LoginManager.randString);
        UnityEngine.Random.InitState(seed);

        timeThread.IsBackground = true;
        timeThread.Start();
    }

    void Update()
    {
        FloorGeneatoring();
    }

    void FloorGeneatoring()
    {
        if (istime)
        {
            for (int i = 0; i < amountFloor; ++i)
            {
                x = UnityEngine.Random.Range((float)-8.4, (float)8.3);
                y = UnityEngine.Random.Range((float)-2.75, (float)2.75);

                Vector2 v2 = new Vector2(x, y);

                Instantiate(floorObject, new Vector3(x, y, 0), new Quaternion());
            }

            istime = false;
        }
    }

    void TimeThread()
    {
        while (true)
        {
            Thread.Sleep(5000);

            istime = true;
        }
    }
}
