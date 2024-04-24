#include "stack.h"

// 구현 부분
template <class T>
stack<T>::~stack()
{
	while (top != nullptr)
		pop();
}

template <class T>
void stack<T>::push(T Item)
{
	// First In Last Out
	node<T>* temp = new node<T>;
	temp->item = Item;
	temp->next = top;
	top = temp;
}

template <class T>
T stack<T>::pop()
{
	if (top == nullptr)
		return -1;

	// First In Last Out
	node<T>* temp = top;
	T numTemp = temp->item;
	top = top->next;
	delete temp;
	return numTemp;
}

template <class T>
T stack<T>::getTop()
{
	// top == isEmpty()
	if (top == nullptr)
		return -1;

	return top->item;
}

template <class T>
bool stack<T>::isEmpty()
{
	return (top == nullptr);
}