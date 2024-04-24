#include "Tile.h"

void Tile::intitTile(int x, int y)
{
	m_x = x;
	m_y = y;
	m_num = 0;
	m_isOpen = false;
}

void Tile::draw()
{
	const char* tileImg[] = { "¡Û","¨ç","¨è","¨é","¨ê","¨ë","¨ì","¨í","¨î","¡Ú","¡á" };

	if (m_isOpen)
		ScreenPrint(m_x, m_y, tileImg[m_num]);
	else
		ScreenPrint(m_x, m_y, tileImg[10]);
}
