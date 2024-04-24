#include "GameMap.h"

void GameMap::setMine(int mineNum)
{
	m_mineNum = mineNum;

	for (int i = 0; i < mineNum; ++i)
	{
		int x, y;
		do
		{
			x = rand() % m_width;
			y = rand() % m_height;
		} while (m_map[y][x].getTileNum == MINE); // �̹� ���ڰ� ��ġ�� �Ǿ� �ִ� ��ġ�̸� ���ο� ��ġ�� ����
		updateTile(x, y); // x, y��ġ�� ���� ��ġ / x, y ��ġ�� �������� 8������ Ÿ�� ��ȣ�� ����
	}
}

void GameMap::updateTile(int x, int y)
{
	// curX, curY : x, y�� �������� 8���� ��ǥ
	for (int curY = y - 1; curY <= y + 1; curY++)
		for (int curX = x - 1; curX <= x + 1; curX++)
		{
			// curX, curY�� ���� ������ ��� ��� ���
			if (curX < 0 || curX > m_width - 1 || curY < 0 || curY > m_height - 1)
				continue;
			// curX, curY == x, y�̸� ���� ��ġ
			if (curX == x && curY == y)
				m_map[curY][curX].setTileNum(MINE);
			else
			{
				int tileNum = m_map[curY][curX].getTileNum();
				if (tileNum == MINE)	// �̹� ���ڰ� ��ġ�Ǿ� ���� ���� ������ ó��
					m_map[curY][curX].setTileNum(tileNum + 1);
			}

		}

}

int GameMap::openTile(int x, int y)
{
	Tile& curTile = m_map[y][x];
	if (x < 0 || x > m_width || y < 0 || y > m_height - 1 || curTile)
	return 0;

	if (curTile.getTileNum() != 0 && !curTile.isTileOpen())
	{
		curTile.setTileNum(true);
		m_openTileNum++;

	}
}

void GameMap::openAllTile()
{
	for (int i = 0; i < m_height; ++i)
		for (int j = 0; j < m_width; ++j)
			m_map[j][i].setTileOpen(true);
}

void GameMap::draw()
{
	for (int i = 0; i < m_height; ++i)
		for (int j = 0; j < m_width; ++j)
			m_map[j][i].draw();
}
