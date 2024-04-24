using System.Collections;
using System.Collections.Generic;
using System;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

public class NetworkDirector : MonoBehaviour
{
    private static NetworkDirector instance;
    public static NetworkDirector Instance 
    {
        get 
        {
            if (null == instance)
                return null;
            return instance;
        }
    }

    public GameObject enemy;

    UdpClient clntSocket;
    IPEndPoint serverEP = new IPEndPoint(IPAddress.Loopback, 10200);

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Thread receiveThread = new Thread(ReceiveData);

        clntSocket = LoginManager.clientSocket;

        receiveThread.IsBackground = true;
        receiveThread.Start();
    }

    public void SendWinner()
    {
        byte[] sendData = new byte[256];

        // 4는 승리했다는 뜻을 알림
        sendData = Encoding.Default.GetBytes("4");
        clntSocket.Send(sendData, sendData.Length, serverEP);
    }

    public void SendPos(float x, float y)
    {
        byte[] sendData = new byte[256];
        string sendSt;

        // 3은 플레이어의 위치 좌표를 의미
        sendSt = x.ToString() + "/" + y.ToString() + "/3";
        sendData = Encoding.Default.GetBytes(sendSt);
        clntSocket.Send(sendData, sendData.Length, serverEP);
    }

    public void SendExit()
    {
        byte[] sendData = new byte[256];

        sendData = Encoding.Default.GetBytes("0");
        clntSocket.Send(sendData, sendData.Length, serverEP);
    }

    void ReceiveData()
    {
        while (true)
        {
            byte[] recvData = new byte[256];
            string recvSt;

            recvData = clntSocket.Receive(ref serverEP);
            recvSt = Encoding.Default.GetString(recvData);

            string[] splitSt = recvSt.Split('/');
            
            switch (splitSt[splitSt.Length - 1])
            {
                case "3":
                    enemy.GetComponent<Enemy>().SetPosition(float.Parse(splitSt[0]), float.Parse(splitSt[1]));
                    break;
                case "4":
                    GameDirector.Instance.OnGameEndUI(2);
                    break;
            }
        }
    }
}
