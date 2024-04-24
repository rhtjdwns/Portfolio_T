#pragma once
#include "Player.h"
#include "AStar.h"
#include "FSM.h"

class Ghost
{
private:
	int x;
	int y;
	char state;
	IState* curState;

public:
	Ghost(int _x, int _y);
	~Ghost();

	void Update(Player* player, GameMap* map);
	void SetPoint(int _x, int _y) { x = _x, y = _y; }
	Point GetPoint() { return { x,y }; }
	void ChangeState(IState* state);
};

