using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    private static LoginManager instance;
    public static LoginManager Instance 
    {
        get 
        {
            if (null == instance)
                return null;
            return instance;
        }
    }

    public GameObject loginUI;
    public GameObject registerUI;
    public InputField loginID;
    public InputField loginPW;
    public InputField registerID;
    public InputField registerPW;
    public Text loginMessage;
    public Text registerMessage;

    public IPEndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 10200);
    public static UdpClient clientSocket;
    static string username;

    string isLogin = "";
    string isRegister = "";

    public static string randString;

    // 1 register 2 login

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Thread receiveThread = new Thread(ReceiveData);

        clientSocket = new UdpClient(Random.Range(0, 65535), AddressFamily.InterNetwork);

        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    private void Update()
    {
        if (isRegister != "")
            CheckRegister(isRegister);
        if (isLogin != "")
            CheckLogin(isLogin);
    }

    void LoadGameScene()
    {
        SceneManager.LoadScene("GameScene");
    }

    void ReceiveData()
    {
        while (true)
        {
            byte[] recvData = new byte[256];
            string recvSt;

            recvData = clientSocket.Receive(ref serverEP);
            recvSt = Encoding.Default.GetString(recvData);

            string[] splitSt = recvSt.Split('/');

            switch (splitSt[splitSt.Length - 1])
            {
                case "1":
                    isRegister = splitSt[0];
                    break;
                case "2":
                    isLogin = splitSt[0];
                    randString = splitSt[1];
                    break;
                case "5":
                    LoadGameScene();
                    break;
            }
        }
    }

    public void OnRegisterButton()
    {
        string sendSt;
        string idName = registerID.text;
        string pwName = registerPW.text;
        byte[] sendData = new byte[256];

        sendSt = idName + "/" + pwName + "/1";
        sendData = Encoding.Default.GetBytes(sendSt);

        clientSocket.Send(sendData, sendData.Length, serverEP);
    }

    public void OnLoginButton()
    {
        string sendSt;
        string idName = loginID.text;
        string pwName = loginPW.text;
        byte[] sendData = new byte[256];

        sendSt = idName + "/" + pwName + "/2";
        sendData = Encoding.Default.GetBytes(sendSt);

        clientSocket.Send(sendData, sendData.Length, serverEP);
    }

    public void OnRegisterUIButton()
    {
        registerUI.SetActive(true);
    }

    public void OffRegisterUIButton()
    {
        registerUI.SetActive(false);
    }

    public void OnLoginUIButton()
    {
        loginUI.SetActive(true);
    }

    public void OffLoginUIButton()
    {
        loginUI.SetActive(false);
    }

    void CheckLogin(string buf)
    {
        if (buf == "1")
        {
            username = loginID.text;
            loginMessage.text = "매칭중";
        }
        else if (buf == "2")
        {
            loginMessage.text = "잘못된 정보를 입력하였습니다.";
        }

        isLogin = "";
    }

    void CheckRegister(string buf)
    {
        if (buf == "1")
        {
            registerMessage.text = "회원가입 완료";
        }
        else if (buf == "2")
        {
            registerMessage.text = "회원가입 실패";
        }

        isRegister = "";
    }
}
