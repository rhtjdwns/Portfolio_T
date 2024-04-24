#define _CRT_SECURE_NO_WARNINGS
#define _WINSOCK_DEPRECATED_NO_WARNINGS
#pragma comment(lib, "ws2_32.lib")
#include <WinSock2.h>
#include <stdio.h>

void ErrorHanding(const char* message);
int main(int argc, char* argv[])
{
	WSADATA wsaData;
	if (WSAStartup(MAKEWORD(2, 2), &wsaData) != 0)
		ErrorHanding("WSAStartup() error!");

	/* inet_addr �Լ� ȣ�� ���� */
	{
		const char* addr = "127.212.124.78";
		unsigned long conv_addr = inet_addr(addr);
		if (conv_addr == INADDR_NONE)
			printf("Error occured! \n");
		else
			printf("Network ordered integer addr : %#lx \n", conv_addr);
	}

	// inet_ntoa �Լ� ȣ�� ����
	struct sockaddr_in addr;
	char* strPtr;
	char strArr[20];

	addr.sin_addr.S_un.S_addr = htonl(0x1020304);
	strPtr = inet_ntoa(addr.sin_addr);
	strcpy(strArr, strPtr);
	printf("Dotted-Decimal notation3 %s \n", strArr);
}

void ErrorHanding(const char* message)
{
	fputs(message, stderr);
	fputc('\n', stderr);
	exit(1);
}