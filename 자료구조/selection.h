#pragma once
#include "sorting.h"

template <class T>
class Selection : public Sort<T>
{
public:
	Selection(int Max = 100) : Sort<T>(Max) {}
	void sorting() override;
};

template <class T>
void Selection<T>::sorting()
{
	int N = this->getSize();
	T* data = this->getData();

	for (int last = N - 1; last > 0; last--)
	{
		int largeIndex = 0;
		for (int cur = 1; cur < last + 1; cur++)
		{
			if (data[cur] > data[largeIndex])
				largeIndex = cur;
		}
		swap(data[last], data[largeIndex]);
	}
}