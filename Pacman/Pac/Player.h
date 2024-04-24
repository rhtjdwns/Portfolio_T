#pragma once
#include "GameMap.h"

class Player
{
private:
	int	x;
	int	y;
	char state;
	int	score;
	char color;
	char bgcolor;
	bool buff;
	int	buffStartTime;
	int buffEndTime;

public:
	Player(int _x, int _y);
	~Player();

	void InputKey();
	void Update(GameMap* map);
	void Draw();
	void GetBuff();

	bool isDie;

	void SetX(int x) { this->x = x; }
	void SetY(int y) { this->y = y; }
	void SetState(char state) { this->state = state; }
	void SetIsDie(bool temp) { this->isDie = temp; }

	int	GetX() { return x; }
	int	GetY() { return y; }
	int	GetState() { return state; }
	int	GetScore() { return score; }
	bool IsDied() { return isDie; }
	bool IsBuff() { return buff; }
};

