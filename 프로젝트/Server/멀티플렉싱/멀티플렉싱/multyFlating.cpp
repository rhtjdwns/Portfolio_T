#define _WINSOCK_DEPRECATED_NO_WARNINGS
#pragma comment(lib, "ws2_32.lib")
#include <WinSock2.h>
#include <stdio.h>
#include <string.h>
#include <stdlib.h>

#define BUF_SIZE 1024
void ErrorHandling(const char* message);

int main(int argc, char* argv[])
{
	WSADATA wsaData;
	SOCKET hServSock, hClntSock;
	SOCKADDR_IN servAdr, clntAdr;
	TIMEVAL timeout;
	fd_set reads, cpyReads;

	int adrSz;
	int strLen, fdNum, i;
	char buf[BUF_SIZE];

	if (argc != 2)
	{
		printf("Usage : %s <port>\n", argv[0]);
		exit(1);
	}
	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
		ErrorHandling("WSAStartuo() error!");

	hServSock = socket(PF_INET, SOCK_STREAM, 0);
	memset(&servAdr, 0, sizeof(servAdr));
	servAdr.sin_family = AF_INET;
	servAdr.sin_addr.S_un.S_addr = htonl(INADDR_ANY);
	servAdr.sin_port = htons(atoi(argv[1]));

	if (bind(hServSock, (SOCKADDR*)& servAdr, sizeof(servAdr)) == SOCKET_ERROR)
		ErrorHandling("bind() error!");
	if (listen(hServSock, 5) == SOCKET_ERROR)
		ErrorHandling("listen() error!");

	FD_ZERO(&reads);
	FD_SET(hServSock, &reads); // fd_set�� ���� reads�� ���� ������ ���

	while (1)
	{
		cpyReads = reads;
		timeout.tv_sec = 5;
		timeout.tv_usec = 5000;

		// ���ѷ��� ������ Select �Լ�ȣ��, ������ ������ �°� 3, 4��° �μ��� �������.
		if ((fdNum = select(0, &cpyReads, 0, 0, &timeout)) == SOCKET_ERROR)
			break;

		if (fdNum == 0)
			continue;

		for (i = 0; i < reads.fd_count; ++i)
		{
			// select �Լ��� 1�̻� ��ȯ���� �� FD_ISSET�Լ��� ȣ���ϸ鼭 ���º�ȭ�� �־���
			// ���ŵ� �����Ͱ� �ִ� ������ ���� ��ũ���͸� ã�� �ִ�.
			if (FD_ISSET(reads.fd_array[i], &cpyReads))
			{
				// ���� ��ȭ�� Ȯ�εǸ� ���� ���� ���� ���Ͽ��� ��ȭ�� �־����� Ȯ��
				if (reads.fd_array[i] == hServSock)
				{
					adrSz = sizeof(clntAdr);
					// ���� ������ ���º�ȭ�� ������ �̾ �����û�� ���� ������ ���� ����
					hClntSock = accept(hServSock, (SOCKADDR*)& clntAdr, &adrSz);
					// fd_set�� ���� reads�� Ŭ���̾�Ʈ�� ����� ������ ���� ��ũ���� ������ ���
					FD_SET(hClntSock, &reads);
					printf("connected client: %d \n", hClntSock);
				}
				// ���� ��ȭ�� �߻��� ������ ���� ������ �ƴ� ��쿡 ���� - ������ �����Ͱ� �ִ� ���
				else
				{
					strLen = recv(reads.fd_array[i], buf, BUF_SIZE - 1, 0);
					if (strLen == 0)
					{
						// ������ �����Ͱ� EOF�� ��� ������ �����ϰ� ���� reads���� �ش������� �����ϴ�
						// ������ ���ľ� �Ѵ�.
						FD_CLR(reads.fd_array[i], &reads);
						closesocket(cpyReads.fd_array[i]);
						printf("closed client: %d \n", cpyReads.fd_array[i]);
					}
					else
					{
						// ������ �����Ͱ� ���ڿ��� ��� ���ڿ� ���
						send(reads.fd_array[i], buf, strLen, 0); // ����
						printf("Client %d : ", reads.fd_array[i]);
						for (int j = 0; j < BUF_SIZE; ++j)
						{
							if (buf[j] == '\n')
								break;

							printf("%c", buf[j]);
						}
						printf("\n");
					}
				}
			}
		}
	}
	closesocket(hServSock);
	WSACleanup();
	return 0;
}

void ErrorHandling(const char* message)
{
	fputs(message, stderr);
	fputc('\n', stderr);
	exit(1);
}