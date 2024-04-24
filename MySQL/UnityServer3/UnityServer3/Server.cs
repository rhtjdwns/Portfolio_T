using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
// 네트워크
using System.Net;
using System.Net.Sockets;
using System.Threading;
// DB
using MySql.Data.MySqlClient;

namespace UnityServer3
{
    enum SendType
    {
        None,
        RegisterData,
        LoginData,
        MoveData,
        WinnerData,
    }

    public class ClientInfo
    {
        public IPEndPoint ipPoint { get; set; }

        public ClientInfo(IPEndPoint endPoint)
        {
            ipPoint = endPoint;
        }
    }

    class Server
    {
        static UdpClient udpSocket = new UdpClient(AddressFamily.InterNetwork);
        static IPEndPoint endPoint = new IPEndPoint(IPAddress.Loopback, 10200);

        static List<ClientInfo> clients = new List<ClientInfo>();
        static List<ClientInfo> matchingList = new List<ClientInfo>();

        static MySqlConnection mySql;
        static string serverName = "localhost";
        static string database = "unitynetwork";
        static string id = "root";
        static string pw = "dkqmfkzkxk0";
        static string connectionAddress = "";

        static int seed;

        static void Main(string[] args)
        {
            Thread serverThread = new Thread(serverFunc);

            serverThread.IsBackground = true;

            Random rand = new Random();

            seed = rand.Next(0, 100);

            serverThread.Start();
            Thread.Sleep(500);

            Console.WriteLine("종료하려면 아무 키나 누르세요.");
            Console.ReadLine();

            serverThread.Abort();
        }

        static void serverFunc(object obj)
        {
            udpSocket.Client.Bind(endPoint);

            IPEndPoint clientEP = new IPEndPoint(IPAddress.None, 0);

            connectionAddress = string.Format("Server = {0}; Database = {1}; Uid = {2}; Pwd = {3};",
                    serverName, database, id, pw);
            mySql = new MySqlConnection(connectionAddress);
            mySql.Open();

            while (true)
            {
                try
                {
                    byte[] recvBytes = new byte[2048];
                    recvBytes = udpSocket.Receive(ref clientEP);
                    string txt = Encoding.Default.GetString(recvBytes);

                    ClientInfo clientInfo = GetClientInfo(clientEP);
                    if (clientInfo == null)
                    {
                        clientInfo = new ClientInfo(clientEP);
                        clients.Add(clientInfo);
                        matchingList.Add(clientInfo);
                    }

                    string[] txtNum = txt.Split('/');
                    switch (txtNum[txtNum.Length - 1])
                    {
                        case "0":
                            clients.Remove(clientInfo);
                            break;
                        case "1":
                            if (RegisterInsertData(mySql, txtNum[0], txtNum[1]) != -1)
                            {
                                Console.WriteLine("Sucess Insert ID");

                                SendSystemMessage("1", clientInfo, SendType.RegisterData);
                            }
                            else
                            {
                                Console.WriteLine("Failed Insert ID");

                                SendSystemMessage("2", clientInfo, SendType.RegisterData);
                            }
                            break;
                        case "2":
                            bool result = LoginCompareData(mySql, txtNum[0], txtNum[1]);
                            if (result)
                            {
                                Console.WriteLine("Sucess Login ID");

                                SendSystemMessage("1", clientInfo, SendType.LoginData);
                            }
                            else
                            {
                                Console.WriteLine("Failed Login ID");

                                SendSystemMessage("2", clientInfo, SendType.LoginData);
                            }
                            break;
                        case "3":
                            SendMessage(txtNum, clientInfo, SendType.MoveData);
                            break;
                        case "4":
                            SendMessage(txtNum, clientInfo, SendType.WinnerData);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        static void MatchingSystem()
        {
            lock (matchingList)
            {
                if (matchingList.Count >= 2)
                {
                    foreach (ClientInfo client in matchingList)
                    {
                        byte[] sendData;
                        // 매칭 완료 신호
                        sendData = Encoding.Default.GetBytes("5");
                        udpSocket.Send(sendData, sendData.Length, client.ipPoint);
                    }

                    matchingList.Clear();
                }
            }
        }

        static void SendSystemMessage(string message, ClientInfo p, SendType st)
        {
            byte[] sendData;
            // 1 : 회원가입 2 : 로그인
            switch (st)
            {
                case SendType.RegisterData:
                    sendData = Encoding.Default.GetBytes(message + "/1");
                    udpSocket.Send(sendData, sendData.Length, p.ipPoint);
                    break;
                case SendType.LoginData:
                    sendData = Encoding.Default.GetBytes(message + "/" + seed.ToString() + "/2");
                    udpSocket.Send(sendData, sendData.Length, p.ipPoint);
                    MatchingSystem();
                    break;
            }
        }

        static void SendMessage(string[] message, ClientInfo p, SendType st)
        {
            byte[] sendData;
            string sendSt;

            if (st == SendType.MoveData)
            {
                foreach (ClientInfo client in clients)
                {
                    if (client.ipPoint == p.ipPoint)
                    {
                        // 보낸 플레이어 데이터
                        continue;
                    }
                    else
                    {
                        // 다른 플레이어 데이터
                        sendSt = message[0] + "/" + message[1] + "/3";
                        sendData = Encoding.Default.GetBytes(sendSt);
                        udpSocket.Send(sendData, sendData.Length, client.ipPoint);
                    }
                }
            }
            else if (st == SendType.WinnerData)
            {
                foreach (ClientInfo client in clients)
                {
                    if (client.ipPoint == p.ipPoint)
                    {
                        // 보낸 플레이어 데이터
                        continue;
                    }
                    else
                    {
                        // 다른 플레이어 데이터
                        sendData = Encoding.Default.GetBytes("4");
                        udpSocket.Send(sendData, sendData.Length, client.ipPoint);
                    }
                }
            }
        }

        static bool LoginCompareData(MySqlConnection sql, string id, string password)
        {
            string selectQuery = string.Format("Select * from userid WHERE id = '{0}' and password = '{1}'", id, password);

            MySqlCommand command = new MySqlCommand(selectQuery, sql);
            MySqlDataReader reader = command.ExecuteReader();

            bool r = reader.Read();
            reader.Close();
            return r;
        }

        static int RegisterInsertData(MySqlConnection sql, string id, string password)
        {
            // 테이블에 인설트할 string 변수
            string insertQuery = string.Format("INSERT INTO userid (id, password) Values ('{0}', '{1}');", id, password);

            MySqlCommand command = new MySqlCommand(insertQuery, sql);

            return command.ExecuteNonQuery();
        }

        private static ClientInfo GetClientInfo(IPEndPoint clientInfo)
        {
            lock (clients)
            {
                foreach (ClientInfo cInfo in clients)
                {
                    if (cInfo.ipPoint.Equals(clientInfo))
                        return cInfo;
                }
            }

            return null;
        }
    }
}
