#pragma comment(lib, "ws2_32.lib")
#include <WinSock2.h>
#include <iostream>

using namespace std;

void ErrorHandling(const char* message);

int main(int argc, char* argv[])
{
	WSADATA wsaData;
	SOCKET hServSock, hClntSock;
	SOCKADDR_IN servAddr, clntAddr;

	int szclntAddr;
	char message[] = "202013144_����"; // Ŭ���̾�Ʈ�� ���� �޼���
	if (argc != 2)
	{
		cout << "Usage : " << argv[0] << " <port>" << endl;
		exit(1);
	}

	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) // ���� ���̺귯�� �ʱ�ȭ
		ErrorHandling("WSAStartup() error!");

	// 1. ���� ���� , PF_INET(IPv4), SOCK_STREAM(���� ������ TCP)
	hServSock = socket(PF_INET, SOCK_STREAM, 0);
	if (hServSock == INVALID_SOCKET)
		ErrorHandling("socket() error");

	memset(&servAddr, 0, sizeof(servAddr)); // ���� �ּ����� �ʱ�ȭ
	servAddr.sin_family = AF_INET; // IPv4 �Ҵ�
	servAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	servAddr.sin_port = htons(atoi(argv[1])); // ��Ʈ

	// 2. ���Ͽ� IP�ּҿ� PORT �ּ� �Ҵ�
	if (bind(hServSock, (SOCKADDR*)& servAddr, sizeof(servAddr)) == SOCKET_ERROR)
		ErrorHandling("bind() error");

	// 3. ������ ������ ���� �������� �ϼ� (Ŭ���̾�Ʈ ��� ����)
	if (listen(hServSock, 5) == SOCKET_ERROR)
		ErrorHandling("listen() error");

	szclntAddr = sizeof(clntAddr);
	// 4. Ŭ���̾�Ʈ�� �����û�� �����ϱ� ���� accept �Լ� ȣ��
	hClntSock = accept(hServSock, (SOCKADDR*)& clntAddr, &szclntAddr);
	if (hClntSock == INVALID_SOCKET)
		ErrorHandling("accept() error");

	// send �Լ� ȣ���� ���ؼ� ����� Ŭ���̾�Ʈ���� �����͸� ���� 
	send(hClntSock, message, sizeof(message), 0);
	closesocket(hClntSock);
	closesocket(hServSock);
	WSACleanup(); // �ʱ�ȭ�� ���� ���̺귯�� ����
	return 0;
}

void ErrorHandling(const char* message)
{
	fputs(message, stderr);
	fputc('\n', stderr);
	exit(1);
}