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
	FD_SET(hServSock, &reads); // fd_set형 변수 reads에 서버 소켓을 등록

	while (1)
	{
		cpyReads = reads;
		timeout.tv_sec = 5;
		timeout.tv_usec = 5000;

		// 무한루프 내에서 Select 함수호출, 관찰의 목적에 맞게 3, 4번째 인수가 비어있음.
		if ((fdNum = select(0, &cpyReads, 0, 0, &timeout)) == SOCKET_ERROR)
			break;

		if (fdNum == 0)
			continue;

		for (i = 0; i < reads.fd_count; ++i)
		{
			// select 함수가 1이상 반환했을 때 FD_ISSET함수를 호출하면서 상태변화가 있었던
			// 수신된 데이터가 있는 소켓의 파일 디스크립터를 찾고 있다.
			if (FD_ISSET(reads.fd_array[i], &cpyReads))
			{
				// 상태 변화가 확인되면 제일 먼저 서버 소켓에서 변화가 있었는지 확인
				if (reads.fd_array[i] == hServSock)
				{
					adrSz = sizeof(clntAdr);
					// 서버 소켓의 상태변화가 맞으면 이어서 연결요청에 대한 수락의 과정 진행
					hClntSock = accept(hServSock, (SOCKADDR*)& clntAdr, &adrSz);
					// fd_set형 변수 reads에 클라이언트와 연결된 소켓의 파일 디스크립터 정보를 등록
					FD_SET(hClntSock, &reads);
					printf("connected client: %d \n", hClntSock);
				}
				// 상태 변화가 발생한 소켓이 서버 소켓이 아닌 경우에 실행 - 수신할 데이터가 있는 경우
				else
				{
					strLen = recv(reads.fd_array[i], buf, BUF_SIZE - 1, 0);
					if (strLen == 0)
					{
						// 수신할 데이터가 EOF인 경우 소켓을 종료하고 변수 reads에서 해당정보를 삭제하는
						// 과정을 거쳐야 한다.
						FD_CLR(reads.fd_array[i], &reads);
						closesocket(cpyReads.fd_array[i]);
						printf("closed client: %d \n", cpyReads.fd_array[i]);
					}
					else
					{
						// 수신한 데이터가 문자열인 경우 에코에 충실
						send(reads.fd_array[i], buf, strLen, 0); // 에코
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