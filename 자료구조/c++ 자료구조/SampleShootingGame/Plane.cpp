#include "Plane.h"
#include "Bullet.h"
#include "util.h"
#include <conio.h>
#include "GameManager.h"

// Ű���� ��,������ ����Ű�� �̵�
// Space bar Ű�� ������ �Ѿ��� �����ȴ�.
void Plane::update()
{
	// ���� ����� ��ġ
	int curX = getX();
	int curY = getY();

	int nKey; // Ű���忡�� �Է¹��� Ű��
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
			Bullet* bullet = new Bullet(x, y - 1, "��");
			GameManager::Instance()->getBulletList()->insert(0, bullet);
			break;
		}
	}
}
