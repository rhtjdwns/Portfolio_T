#pragma once
#include <assert.h>

template <class T>
class arrayStack
{
private:
	int max;
	int top;
	T* Stack;
public:
	arrayStack(int max_ = 100) : top(0), max(max_) {
		Stack = new T[max];
		assert(Stack != nullptr);
	}

	~arrayStack() { if (Stack) delete Stack; }
	void Push(T item) { IsFull() ? -1 : Stack[top++] = item; }
	T Pop() { return IsEmpty() ? T() : Stack[--top]; }
	bool IsEmpty() { return top == 0; }
	bool IsFull() { return top == max; }
	T getTop() { return IsEmpty() ? T() : Stack[top - 1]; }
	int getSize() { return top; }
};

