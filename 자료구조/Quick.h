#pragma once
#include "sorting.h"

template <class T>
class Quick : public Sort<T>
{
public:
	Quick(int Max = 100) : Sort<T>(Max) {}
	void sorting() override;
	void quicksort(int low, int high);
	int partition(int low, int high);
};

template <class T>
int Quick<T>::partition(int low, int high)
{
	T* data = this->getData();
	int pivotItem = data[low];
	int pivotPoint = low;
	for (int cur = low + 1; cur <= high; cur++)
	{
		if (data[cur] < pivotItem)
		{
			pivotPoint++;
			swap(data[cur], data[pivotPoint]);
		}
	}
	swap(data[low], data[pivotPoint]);
	return pivotPoint;
}

template <class T>
void Quick<T>::quicksort(int low, int high)
{
	int pivotPoint;
	if (high > low)
	{
		pivotPoint = partition(low, high);
		quicksort(low, pivotPoint - 1);
		quicksort(pivotPoint + 1, high);
	}
}

template <class T>
void Quick<T>::sorting()
{
	quicksort(0, this->getSize() - 1);
}