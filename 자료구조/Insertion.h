#pragma once
#include "sorting.h"

template <class T>
class Insertion : public Sort<T>
{
public:
	Insertion(int Max = 100) : Sort<T>(Max) {}
	void sorting() override;
};

template <class T>
void Insertion<T>::sorting()
{
	int size = this->getSize();
	T* data = this->getData();

	for (int i = 1; i < size; ++i)
	{
		// [0][1][2][3][4][5][6][7][8][9]
		// 10, 7, 9, 1, 3, 2, 8, 6, 4, 5
		// 7, 10, 9, 1, 3, 2, 8, 6, 4, 5
		// 7, 9, 10, 1, 3, 2, 8, 6, 4, 5
		int j;
		int pick = i;
		T pickData = data[pick];
		for (j = pick - 1; j >= 0; --j)
		{
			if (data[j] > pickData)
			{
				data[j + 1] = data[j];
				cout << "ÀÌµ¿ 1 : " << data[j] << " ";
				continue;
			}

			break;
		}

		cout << endl;
		data[j + 1] = pickData;
	}
}
