#pragma once
#include <iostream>
using namespace std;

#define ENTER	13
#define LEFT    75
#define RIGHT   77
#define UP		72
#define DOWN	80
#define SPACE	32


void gotoxy(int x, int y);

void ScreenInit();			// ȭ�� �ʱ�ȭ - ���α׷��� ó�� ���۵� ��
void ScreenFlipping();		// �޸� ������ ��� ����
void ScreenClear();			// ȭ�� ����
void ScreenRelease();		// ���� - ���α׷� ���� ���� ó��
void ScreenPrint( int x, int y, const char * str ); // ȭ�� x , y ��ġ�� string�� �޸� ���ۿ� ����
void SetColor( unsigned short color ); // ������ ������ ����
