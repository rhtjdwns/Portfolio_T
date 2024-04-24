#pragma once
#include "sorting.h"

template <class T>
class Bubble : public Sort<T>
{
public:
	Bubble(int Max = 100) : Sort<T>(Max) {}
	void sorting() override;
};

template <class T>
void Bubble<T>::sorting()
{
	int N = this->getSize();
	T* data = this->getData();

	bool sorted = false;
	for (int i = 0; i < N; ++i)
	{
		for (int j = 0; j < N - 1 - i; ++j)
		{
			if (data[j] > data[j + 1])
				swap(data[j], data[j + 1]);
		}
	}
}