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

	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) // 소켓 라이브러리 초기화
		ErrorHandling("WSAStartup() error!");

	hSocket = socket(PF_INET, SOCK_STREAM, 0); // 1. 소켓 생성
	if (hSocket == INVALID_SOCKET)
		ErrorHandling("socket() error");

	memset(&servAddr, 0, sizeof(servAddr)); // 주소 정보 초기화
	servAddr.sin_family = AF_INET; // IPv4 사용
	servAddr.sin_addr.s_addr = inet_addr(argv[1]); // ip주소
	servAddr.sin_port = htons(atoi(argv[2])); // 포트번호

	// 2. 서버에 연결 요청
	if (connect(hSocket, (SOCKADDR*)&servAddr, sizeof(servAddr)) == SOCKET_ERROR)
		ErrorHandling("connect() error!");

	// recv 함수를 통해 서버로부터 데이터 수신
	while (strlen = recv(hSocket, &message[idx++], 1, 0))
	{
		if (strlen == -1)
			ErrorHandling("read() error!");

		readLen += strlen;
	}

	cout << "Message from server : " << message << endl; // 서버로 받은 메세지 출력
	cout << "Function read call count : " << readLen << endl;
	closesocket(hSocket);
	WSACleanup(); // 초기화한 소켓 라이브러리 해제
	return 0;
}