using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    [HideInInspector] public static GameManager instance;

    public List<GameObject> Monsters;
    public List<GameObject> Ammos;

    public List<bool> SceneCompleted;

    public int CurrentScene;

    public void Awake()
    {
        if (instance == null)
            instance = this;
        if (instance != this)
            Destroy(this.gameObject);

        DontDestroyOnLoad(gameObject);
        CurrentScene = -1;
    }
}
