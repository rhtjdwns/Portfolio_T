#pragma comment(lib, "ws2_32.lib")
#include <iostream>
#include <WinSock2.h>

#pragma warning(disable : 4996)

using namespace std;

void ErrorHandling(const char* message)
{
	fputs(message, stderr);
	fputc('\n', stderr);
	exit(1);
}

int main(int argc, char* argv[])
{
	WSADATA wsaData;
	SOCKET hSocket;
	SOCKADDR_IN servAddr;

	char message[30];
	int strlen = 0, idx = 0, readLen = 0;
	if (argc != 3)
	{
		cout << "Usage : " << argv[0] << " <IP> <port>" << endl;
		exit(1);
	}

	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) // ���� ���̺귯�� �ʱ�ȭ
		ErrorHandling("WSAStartup() error!");

	hSocket = socket(PF_INET, SOCK_STREAM, 0); // 1. ���� ����
	if (hSocket == INVALID_SOCKET)
		ErrorHandling("socket() error");

	memset(&servAddr, 0, sizeof(servAddr)); // �ּ� ���� �ʱ�ȭ
	servAddr.sin_family = AF_INET; // IPv4 ���
	servAddr.sin_addr.s_addr = inet_addr(argv[1]); // ip�ּ�
	servAddr.sin_port = htons(atoi(argv[2])); // ��Ʈ��ȣ

	// 2. ������ ���� ��û
	if (connect(hSocket, (SOCKADDR*)& servAddr, sizeof(servAddr)) == SOCKET_ERROR)
		ErrorHandling("connect() error!");

	// recv �Լ��� ���� �����κ��� ������ ����
	while (strlen = recv(hSocket, &message[idx++], 1, 0))
	{
		if (strlen == -1)
			ErrorHandling("read() error!");

		cout << "Message from server : " << message << endl; // ������ ���� �޼��� ���
		readLen += strlen;
	}

	cout << "Message from server : " << message << endl; // ������ ���� �޼��� ���
	cout << "Function read call count : " << readLen << endl;
	closesocket(hSocket);
	WSACleanup(); // �ʱ�ȭ�� ���� ���̺귯�� ����
	return 0;
}