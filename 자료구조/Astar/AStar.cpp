#include "AStar.h"
#include "GameMap.h"
#include <iostream>

void AStar::extractMin(point& choicePos, int dx, int dy)
{
    int min = INT_MAX;
    int width = m_gameMap->getWidth();
    int height = m_gameMap->getHeight();

    int curX, curY;
    list<point>::reverse_iterator  curPos;
    for (curPos = m_visitNode.rbegin(); curPos != m_visitNode.rend(); curPos++) {
        // ���� ��带 �������� 8������ ��ĳ��
        for (int ty = -1; ty <= 1; ty++) {
            for (int tx = -1; tx <= 1; tx++) {
                curX = curPos->x + tx;
                curY = curPos->y + ty;
                if (curX < 0 || curX > width - 1 || curY < 0 || curY > height - 1
                    || (tx == 0 && ty == 0))
                    continue;
                int hx = abs(dx - curX) * 10;
                int hy = abs(dy - curY) * 10;
                int hdist = hx + hy;
                if (m_gameMap->getMapVal(curX, curY) + hdist < min &&
                    m_gameMap->getVisitInfo(curX, curY) == false) {
                    min = m_gameMap->getMapVal(curX, curY) + hdist;
                    choicePos = { curX, curY };
                }
            }
        }
    }
}

bool AStar::findPath(int sx, int sy, int dx, int dy)
{
    m_bFound = false;
    int width = m_gameMap->getWidth();
    int height = m_gameMap->getHeight();

    point** parent;
    parent = new point * [height];
    for (int i = 0; i < height; i++)
        parent[i] = new point[width];

    point choicePos;
    m_gameMap->setMapVal(sx, sy, 0);  // ���� ��ġ�� ����� 0���� ����
    choicePos = { sx, sy };
    parent[sy][sx] = choicePos;

    for (int i = 0; i < width * height; i++) {
        m_gameMap->setVisitInfo(choicePos.x, choicePos.y, true);
        m_visitNode.push_back(choicePos);

        // ��ǥ ��带 ������ ����
        if (choicePos.x == dx && choicePos.y == dy) {
            m_bFound = true;
            break;
        }
        // ������ȭ �˰��� ����
        for (int ty = -1; ty <= 1; ty++) {
            for (int tx = -1; tx <= 1; tx++) {
                int nx = choicePos.x + tx;
                int ny = choicePos.y + ty;
                int dist;
                if (nx < 0 || nx > width - 1 || ny < 0 || ny > height - 1)
                    continue;
                if (m_gameMap->getVisitInfo(nx, ny) == false) {
                    dist = (tx == 0 || ty == 0) ? 10 : 14;

                    // ������ȭ ó��
                    if (m_gameMap->getMapVal(nx, ny) >
                        m_gameMap->getMapVal(choicePos.x, choicePos.y) + dist) {
                        int tcost = m_gameMap->getMapVal(choicePos.x, choicePos.y) + dist;
                        m_gameMap->setMapVal(nx, ny, tcost);
                        parent[ny][nx] = choicePos;
                    }
                }
            }
        }
        extractMin(choicePos, dx, dy);
        draw();
    }

    if (m_bFound) {
        point p;
        p = { dx, dy };
        m_path.push(p);
        while (p.x != sx || p.y != sy) {
            p = parent[p.y][p.x];
            m_path.push(p);
        }
        return true;
    }

    return false;
}

void AStar::draw()
{
    m_gameMap->draw();
    if (m_bFound) {
        point p;
        do {
            p = m_path.top();
            cout << "(" << p.x << "," << p.y << ") ==> ";
            m_path.pop();
        } while (!m_path.empty());
        cout << "�ִ� ��θ� ã�ҽ��ϴ�..." << endl;
    }
}
