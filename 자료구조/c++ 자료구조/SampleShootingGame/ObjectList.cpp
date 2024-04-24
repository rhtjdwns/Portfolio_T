#include "ObjectList.h"
#include "GameObject.h"

ObjectList::~ObjectList()
{
	node* delNode = head;
	node* nextNode = head;
	while (delNode)
	{
		nextNode = nextNode->next;
		delete delNode;
		delNode = nextNode;
	}
	head = nullptr;
}

void ObjectList::insert(int pos, GameObject* obj)
{
	node* curNode = head;

	// 抗寇 贸府
	if (pos < 0 || pos > count)
		return;

	node* inNode = new node;
	inNode->object = obj;

	if (pos == 0)
	{
		inNode->next = head;
		head = inNode;
	}
	else
	{
		for (int i = 0; i < pos - 1; ++i)
		{
			curNode = curNode->next;
		}
		inNode->next = curNode->next;
		curNode->next = inNode;
	}
	count++;
}

void ObjectList::remove(int pos)
{
	// 抗寇 贸府
	if (!head || pos < 0 || pos > count - 1) return;

	node* curNode = head;
	node* delNode;
	if (pos == 0)
	{
		delNode = curNode;
		head = head->next;
	}
	else
	{
		for (int i = 0; i < pos - 1; ++i)
		{
			curNode = curNode->next;
		}
		delNode = curNode->next;
		curNode->next = delNode->next;
	}
	delete delNode;
	count--;
}

void ObjectList::remove(GameObject* obj)
{
	int curPos = getPos(obj);
	if (curPos >= 0)
		remove(curPos);
}

GameObject* ObjectList::getObject(int pos)
{
	// 抗寇 贸府
	if (pos < 0 || pos > count - 1 || head == nullptr)
		return nullptr;

	node* curNode = head;
	for (int i = 0; i < pos; ++i)
	{
		curNode = curNode->next;
	}

	return curNode->object;
}

int ObjectList::getPos(GameObject* obj)
{
	int x = obj->getX();
	int y = obj->getY();

	node* curNode = head;
	for (int curPos = 0; curPos < count; curPos++)
	{
		if (curNode->object->getX() == x && curNode->object->getY() == y)
			return curPos;
		curNode = curNode->next;
	}
	return -1;
}

void ObjectList::update()
{
	node* curNode = head;
	for (int i = 0; i < count; ++i)
	{
		curNode->object->update();
		curNode = curNode->next;
	}
}

void ObjectList::draw()
{
	node* curNode = head;
	if (head)
	{
		for (int i = 0; i < count; ++i)
		{
			curNode->object->draw();
			curNode = curNode->next;
		}
	}
}
