#include "MainFrame.h"

MainFrame::MainFrame(int _delay) : delay(_delay)
{
	CONSOLE_CURSOR_INFO cursorInfo = { 0, };
	cursorInfo.dwSize = 1;
	cursorInfo.bVisible = FALSE;
	SetConsoleCursorInfo(GetStdHandle(STD_OUTPUT_HANDLE), &cursorInfo);
	system("mode con lines=34 cols=54");

	player = new Player(13, 23);
	gameMap = new GameMap();
	running = true;
}

MainFrame::~MainFrame()
{
	delete player;
	delete gameMap;
}

void MainFrame::Update()
{
	player->InputKey();
	gameMap->Update();
	player->Update(gameMap);

	Sleep(delay);
}

void MainFrame::Draw()
{
	gameMap->Draw();
	player->Draw();
}
