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
		} while (m_map[y][x].getTileNum == MINE); // 이미 지뢰가 설치가 되어 있는 위치이면 새로운 위치를 생성
		updateTile(x, y); // x, y위치에 지뢰 설치 / x, y 위치를 기준으로 8방향의 타일 번호를 변경
	}
}

void GameMap::updateTile(int x, int y)
{
	// curX, curY : x, y를 기준으로 8방향 좌표
	for (int curY = y - 1; curY <= y + 1; curY++)
		for (int curX = x - 1; curX <= x + 1; curX++)
		{
			// curX, curY가 맵의 범위를 벗어난 경우 통과
			if (curX < 0 || curX > m_width - 1 || curY < 0 || curY > m_height - 1)
				continue;
			// curX, curY == x, y이면 지뢰 설치
			if (curX == x && curY == y)
				m_map[curY][curX].setTileNum(MINE);
			else
			{
				int tileNum = m_map[curY][curX].getTileNum();
				if (tileNum == MINE)	// 이미 지뢰가 설치되어 있지 않을 때에만 처리
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
