#include "itemList.h"
#include "item.h"
#include <iostream>

using namespace std;

itemList::itemList() : m_count(0), m_maxSize(20)
{
	
}

itemList::~itemList()
{
}

itemList::itemList(int Max) : m_count(0), m_maxSize(Max)
{
	m_items = new item[Max];
}

string errMsg[3] = { "리스트가 비었습니다.", "리스트가 꽉 찼습니다.", "범위를 벗어났습니다." };

void itemList::insert(int pos, item item)
{
	try
	{
		if (isFull())
			throw 1;
		if (pos < 0 || pos > m_count)
			throw 2;
		for (int curPos = m_count - 1; curPos >= pos; curPos--)
			m_items[curPos + 1] = m_items[curPos];
	}
	catch (int errN)
	{
		cout << errMsg[errN] << endl;
	}
}

void itemList::remove(int pos)
{
	try
	{
		if (isEmpty())
			throw 0;
		if (pos < 0 || pos > m_count - 1)
			throw 2;
		for (int curPos = pos; curPos < m_count - 1; curPos++)
			m_items[curPos] = m_items[curPos + 1];
	}
	catch (int errN)
	{
		cout << errMsg[errN] << endl;
	}
}

item itemList::retrieve(int pos)
{
	try
	{
		if (isEmpty())
			throw 0;
		if (pos < 0 || pos > m_count - 1)
			throw 2;
		return m_items[pos];
	}
	catch (int errN)
	{
		cout << errMsg[errN] << endl;
	}
	return item();
}

void itemList::draw()
{
	for (int curPos = 0; curPos < m_count; curPos++)
		m_items[curPos].draw();
}
