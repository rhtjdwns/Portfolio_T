#pragma once
#include <iostream>
using namespace std;
class item
{
	string m_name;
	int m_x;
	int m_y;
public:
	item() : m_x(0), m_y(0), m_name("") { }
	~item() {}
	item(int x, int y, string n) : m_x(x), m_y(y), m_name(n) { }
	void draw();
};

