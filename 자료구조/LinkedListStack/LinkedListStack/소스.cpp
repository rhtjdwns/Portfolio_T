#include "stack.h"
#include <iostream>


using namespace std;

int main()
{
	stack<float> intStack;

	for (int i = 0; i < 5; ++i)
	{
		intStack.push(i * 2.55f);
	}

	do
	{
		cout << intStack.pop() << endl;
	} while (!intStack.isEmpty());
}