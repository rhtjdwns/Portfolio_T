#define _CRT_SECURE_NO_WARNINGS

#include<math.h>
#include<Windows.h>
#include<time.h>
#include<iostream>
#include<vector>
#include<algorithm>

using namespace std;

const int SPACE =		1;
const int WALL =		2;
const int ITEM =		3;
const int p_POINT =		4;
const int PLAYER =		5;
const int ENEMY =       6;

const int UP =          7;
const int DOWN =        8;
const int RIGHT =       9;
const int LEFT =        10;
const int DIE =         11;

struct Point
{
	int x;
	int y;
};

static void gotoxy(int x, int y)
{
	COORD pos = { x,y };
	SetConsoleCursorPosition(GetStdHandle(STD_OUTPUT_HANDLE), pos);
}

static void SetColor(int txt, int bg)
{
	SetConsoleTextAttribute(GetStdHandle(STD_OUTPUT_HANDLE), txt + bg * 16);
}

enum ConsoleColor
{
    BLACK,
    DARKBLUE,
    DARKGREEN,
    DARKCYAN,
    DARKRED,
    DARKMAGENTA,
    DARKYELLOW,
    GRAY,
    DARKGRAY,
    BLUE,
    GREEN,
    CYAN,
    RED,
    MAGENTA,
    YELLOW,
    WHITE,
    LIGHTGRAY = 7,
    ORIGINAL = 7,
    ORIGINALFONT = 7,
    ORIGINALBG = 0,
};