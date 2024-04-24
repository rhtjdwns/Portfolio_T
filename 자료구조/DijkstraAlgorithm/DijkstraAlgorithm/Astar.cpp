#include "Astar.h"
#include "GameMap.h"

void Astar::ExtractMin(point& choicePos, point& targetPos)
{
	int min = INT_MAX;
	int width = m_gameMap->getWidth();
	int height = m_gameMap->getHeight();

	int curX, curY;
	list<point>::reverse_iterator curPos;

	for (curPos = m_visitNode.rbegin(); curPos != m_visitNode.rend(); curPos++)
	{
		// 현재 노드를 기준으로 8방향을 스캐닝
		for (int i = -1; i <= 1; ++i)
		{
			for (int j = -1; j <= 1; ++j)
			{
				curX = curPos->x + j;
				curY = curPos->y + i;
				if (curX < 0 || curX > width - 1 || curY < 0 || curY > height - 1
					|| (i == 0 && j == 0))
					continue;

				int hx = abs(targetPos.x - curX) * 10;
				int hy = abs(targetPos.y - curY) * 10;
				int hDist = hx + hy;

				if (m_gameMap->getMapVal(curX, curY) + hDist < min &&
					m_gameMap->getVisitInfo(curX, curY) == false)
				{
					min = m_gameMap->getMapVal(curX, curY) + hDist;
					choicePos = { curX, curY };
				}
			}
		}
	}
}

bool Astar::FindPath(int sx, int sy, int dx, int dy)
{
	m_bFound = false;
	int width = m_gameMap->getWidth();
	int height = m_gameMap->getHeight();

	point** parent;
	parent = new point * [height];
	for (int i = 0; i < height; ++i)
	{
		parent[i] = new point[width];
	}

	point choicePos;
	point targetPos = { dx, dy };
	m_gameMap->setMapVal(sx, sy, 0);		// 시작 위치의 비용을 0으로 설정
	choicePos = { sx, sy };
	parent[sy][sx] = choicePos;

	for (int i = 0; i < width * height; ++i)
	{
		m_gameMap->setVisitInfo(choicePos.x, choicePos.y, true);
		m_visitNode.push_back(choicePos);

		// 목표 노드를 만나면 종료
		if (choicePos.x == dx && choicePos.y == dy)
		{
			m_bFound = true;
			break;
		}

		// 엣지 완화 알고리즘 적용
		for (int ty = -1; ty <= 1; ++ty)
		{
			for (int tx = -1; tx <= 1; ++tx)
			{
				int nx = choicePos.x + tx;
				int ny = choicePos.y + ty;
				int dist;

				if (nx < 0 || nx > width - 1 || ny < 0 || ny > height - 1)
					continue;
				if (m_gameMap->getVisitInfo(nx, ny) == false)
				{
					dist = (tx == 0 || ty == 0) ? 10 : 14;

					// 엣지 완화 처리
					if (m_gameMap->getMapVal(nx, ny) >
						m_gameMap->getMapVal(choicePos.x, choicePos.y) + dist)
					{
						int tCost = m_gameMap->getMapVal(choicePos.x, choicePos.y) + dist;
						m_gameMap->setMapVal(nx, ny, tCost);
						parent[ny][nx] = choicePos;
					}
				}
			}
		}

		ExtractMin(choicePos, targetPos);
		Draw();
	}

	if (m_bFound)
	{
		point p;
		p = { dx, dy };
		m_path.push(p);

		while (p.x != sx || p.y != sy)
		{
			p = parent[p.y][p.x];
			m_path.push(p);
		}
		return true;
	}

	return false;
}

void Astar::Draw()
{
	m_gameMap->draw();
	if (m_bFound)
	{
		point p;
		do
		{
			p = m_path.top();
			cout << "(" << p.x << " , " << p.y << ") ==> ";
			m_path.pop();
		} while (!m_path.empty());

		cout << "최단 경로를 찾았습니다." << endl;
	}
}
