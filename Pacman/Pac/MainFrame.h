#pragma once
#include "GameMap.h"
#include "Player.h"

class MainFrame
{
private:
	int delay;
	bool running;

	GameMap* gameMap;
	Player* player;

public:
	MainFrame(int delay);
	~MainFrame();

	void Update();
	void Draw();
};

