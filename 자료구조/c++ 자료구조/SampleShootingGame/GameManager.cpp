#include "GameManager.h"

GameManager* GameManager::Instance()
{
	static GameManager* instance = nullptr;
	if (instance == nullptr)
		instance = new GameManager();
	return instance;
}

bool GameManager::collision()
{
	int bulletNum = bulletList->getSize();
	if (bulletNum == 0)
		return false;

	for (int i = 0; i < bulletList->getSize(); ++i)
	{
		GameObject* curBullet = bulletList->getObject(i);
		if (curBullet->getY() <= 0)
			bulletList->remove(i);
	}
	
	return true;
}

void GameManager::update()
{
	plane->update();
	bulletList->update();
}

void GameManager::draw()
{
	plane->draw();
	bulletList->draw();
}
