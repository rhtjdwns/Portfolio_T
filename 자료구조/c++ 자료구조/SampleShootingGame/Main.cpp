#include "GameObject.h"
#include "Plane.h"
#include "Bullet.h"
#include <Windows.h>
#include "GameManager.h"
#include "ObjectList.h"
#include <time.h>

void main()
{
	GameObject* plane = new Plane(15, 16, "бс");
	ObjectList* bulletList = new ObjectList;

	GameManager::Instance()->InitGame(plane, bulletList, 30, 18);

	ScreenInit();

	bool isStop = false;
	char str[30];
	clock_t oldTime, curTime;
	oldTime = clock();

	while (!isStop)
	{
		ScreenClear();

		GameManager::Instance()->update();
		if (GameManager::Instance()->collision())
		{
			sprintf_s(str, "b:%d", bulletList->getSize());
			ScreenPrint(1, 20, str);
		}
		GameManager::Instance()->draw();

		ScreenFlipping();
		while (true)
		{
			curTime = clock();
			if (curTime - oldTime > 33)
			{
				oldTime = curTime;
				break;
			}
		}
	}
}