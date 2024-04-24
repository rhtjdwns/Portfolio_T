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

void ScreenInit();			// 화면 초기화 - 프로그램이 처음 시작될 때
void ScreenFlipping();		// 메모리 내용을 고속 복사
void ScreenClear();			// 화면 제거
void ScreenRelease();		// 해제 - 프로그램 종료 전에 처리
void ScreenPrint( int x, int y, const char * str ); // 화면 x , y 위치에 string을 메모리 버퍼에 저장
void SetColor( unsigned short color ); // 문자의 색깔을 지정
