#include "Plane.h"
#include "Bullet.h"
#include "util.h"
#include <conio.h>
#include "GameManager.h"

// 키보드 왼,오른쪽 방향키로 이동
// Space bar 키를 누르면 총알이 생성된다.
void Plane::update()
{
	// 현재 비행기 위치
	int curX = getX();
	int curY = getY();

	int nKey; // 키보드에서 입력받은 키값
	if (_kbhit())
	{
		nKey = _getch();
		switch (nKey)
		{
		case LEFT:
			if (curX - 1 > -1)
				--x;
			break;
		case RIGHT:
			if (curX + 1 < GameManager::Instance()->getWidth() - 1)
				++x;
			break;
		case SPACE:
			Bullet* bullet = new Bullet(x, y - 1, "◆");
			GameManager::Instance()->getBulletList()->insert(0, bullet);
			break;
		}
	}
}
