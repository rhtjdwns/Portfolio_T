#pragma once
#include <iostream>
#include <cassert>

using namespace std;

template <class T>
class Sort
{
	T* m_data;
	int m_size;

public:
	Sort(int Max = 100) { m_data = new T[Max]; }
	~Sort() { if(m_data) delete[] m_data; }

	void initData(T* data, int size)
	{
		m_size = size;
		if (m_data)
			delete[] m_data;

		m_data = new T[m_size];
		assert(m_data != nullptr);
		memcpy(m_data, data, sizeof(T) * m_size);
	}
	int getSize() { return m_size; }
	T* getData() { return m_data; }
	void printAll()
	{
		for (int i = 0; i < m_size; ++i)
			cout << m_data[i] << " ";
		cout << "\n";
	}
	virtual void sorting() = 0;
};