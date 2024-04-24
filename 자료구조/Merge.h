#pragma once
#include "sorting.h"

template <class T>
class Merge : public Sort<T>
{
public:
	Merge(int Max = 100) : Sort<T>(Max) {}
	void sorting() override;
	void mergesort(T* data, int first, int last);
	void merge(T* data, int first, int middle, int last);
};

template <class T>
void Merge<T>::merge(T* data, int first, int middle, int last)
{
	int N = this->getSize();
	T* sorted = new T[N];
	int f1 = first;
	int l1 = middle;
	int f2 = middle + 1;
	int l2 = last;

	int index = f1;
	while (f1 <= l1 && f2 <= l2)
	{
		if (data[f1] < data[f2])
			sorted[index++] = data[f1++];
		else
			sorted[index++] = data[f2++];
	}

	for (; f1 <= l1; f1++, index++)
		sorted[index] = data[f1];
	for (; f2 <= l2; f2++, index++)
		sorted[index] = data[f2];

	for (index = first; index <= last; index++)
		data[index] = sorted[index];
}

template <class T>
void Merge<T>::mergesort(T* data, int first, int last)
{
	if (first < last)
	{
		int middle = (first + last) / 2;
		mergesort(data, first, middle);
		mergesort(data, middle + 1, last);
		merge(data, first, middle, last);
	}
}

template <class T>
void Merge<T>::sorting()
{
	mergesort(this->getData(), 0, this->getSize() - 1);
}