#include "stack.h"
#include <cassert>

stack::~stack()
{
	while (top != nullptr)
		pop();
}

void stack::push(int Item)
{
	// First In Last Out
	node* temp = new node;
	temp->item = Item;
	temp->next = top;
	top = temp;
}

int stack::pop()
{
	if (top == nullptr)
		return -1;

	// First In Last Out
	node* temp = top;
	int numTemp = temp->item;
	top = top->next;
	delete temp;
	return numTemp;
}

int stack::getTop()
{
	// top == isEmpty()
	if (top == nullptr)
		return -1;

	return top->item;
}

bool stack::isEmpty()
{
	return (top == nullptr);
}
