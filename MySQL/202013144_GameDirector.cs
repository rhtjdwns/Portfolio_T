using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameDirector : MonoBehaviour
{
    public GameObject GameEndUI;
    public Text GameEndText;

    private static GameDirector instance;

    bool isEnd = false;

    public static GameDirector Instance 
    {
        get 
        {
            if (null == instance)
                return null;
            return instance;
        }
    }

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        if (isEnd)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Time.timeScale = 1;
                SceneManager.LoadScene("LoginScene");
            }
        }
    }

    public void OnGameEndUI(int endNum)
    {
        GameEndUI.SetActive(true);
        if (endNum == 1)
            GameEndText.text = "YOU WIN!!!";
        else if (endNum == 2)
            GameEndText.text = "YOU LOSE...";

        Time.timeScale = 0;
        isEnd = true;
    }

    private void OnApplicationQuit()
    {
        NetworkDirector.Instance.SendExit();
    }
}
