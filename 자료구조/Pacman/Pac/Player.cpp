#include "Player.h"

Player::Player(int _x, int _y) : x(_x), y(_y)
{
	score = 0;
	isDie = false;
	buff = false;
}

Player::~Player()
{
}

void Player::InputKey()
{
	if (GetAsyncKeyState(VK_UP) & 0x8000)
		state = UP;
	else if (GetAsyncKeyState(VK_DOWN) & 0x8000)
		state = DOWN;
	else if (GetAsyncKeyState(VK_LEFT) & 0x8000)
		state = LEFT;
	else if (GetAsyncKeyState(VK_RIGHT) & 0x8000)
		state = RIGHT;
}

void Player::Update(GameMap* map)
{
	switch (state)
	{
	case UP:
		if (map->GetMap(x, y - 1) == WALL)
			break;
		y -= 1;
		break;
	case DOWN:
		if (map->GetMap(x, y + 1) == WALL)
			break;
		y += 1;
		break;
	case LEFT:
		if (x - 1 < 0)
			x = Width;
		if (map->GetMap(x - 1, y) == WALL)
			break;
		x -= 1;
		break;
	case RIGHT:
		if (x + 1 > Width - 1)
			x = 0;
		if (map->GetMap(x + 1, y) == WALL)
			break;
		x += 1;
		break;
	default:
		break;
	}

	switch (map->GetItem(x, y))
	{
	case p_POINT:
		score += 10;
		break;
	case ITEM:
		GetBuff();
		break;
	}

	buffEndTime = clock();
	if (buffEndTime - buffStartTime > 5000)
		buff = false;

	map->SetBuffer(x, y, PLAYER);
}

void Player::Draw()
{
	gotoxy(4, 31);
	SetColor(WHITE, BLACK);
	cout << "SCORE : " << score;
}

void Player::GetBuff()
{
	buffStartTime = clock();
	buff = true;
}
