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
	char message[] = "202013144_고성준"; // 클라이언트로 보낼 메세지
	if (argc != 2)
	{
		cout << "Usage : " << argv[0] << " <port>" << endl;
		exit(1);
	}

	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0) // 소켓 라이브러리 초기화
		ErrorHandling("WSAStartup() error!");

	// 1. 소켓 생성 , PF_INET(IPv4), SOCK_STREAM(연결 지향형 TCP)
	hServSock = socket(PF_INET, SOCK_STREAM, 0);
	if (hServSock == INVALID_SOCKET)
		ErrorHandling("socket() error");

	memset(&servAddr, 0, sizeof(servAddr)); // 서버 주소정보 초기화
	servAddr.sin_family = AF_INET; // IPv4 할당
	servAddr.sin_addr.s_addr = htonl(INADDR_ANY);
	servAddr.sin_port = htons(atoi(argv[1])); // 포트

	// 2. 소켓에 IP주소와 PORT 주소 할당
	if (bind(hServSock, (SOCKADDR*)& servAddr, sizeof(servAddr)) == SOCKET_ERROR)
		ErrorHandling("bind() error");

	// 3. 생성한 소켓을 서버 소켓으로 완성 (클라이언트 대기 상태)
	if (listen(hServSock, 5) == SOCKET_ERROR)
		ErrorHandling("listen() error");

	szclntAddr = sizeof(clntAddr);
	// 4. 클라이언트의 연결요청을 수락하기 위해 accept 함수 호출
	hClntSock = accept(hServSock, (SOCKADDR*)& clntAddr, &szclntAddr);
	if (hClntSock == INVALID_SOCKET)
		ErrorHandling("accept() error");

	// send 함수 호출을 통해서 연결된 클라이언트에게 데이터를 전송 
	send(hClntSock, message, sizeof(message), 0);
	closesocket(hClntSock);
	closesocket(hServSock);
	WSACleanup(); // 초기화한 소켓 라이브러리 해제
	return 0;
}

void ErrorHandling(const char* message)
{
	fputs(message, stderr);
	fputc('\n', stderr);
	exit(1);
}