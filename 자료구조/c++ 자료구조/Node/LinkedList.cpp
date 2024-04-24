#include "LinkedList.h"
#include <iostream>
using namespace std;

void LinkedList::insert(int pos, int data)
{
	if (pos < 0 || pos > count)
		return;
	// 추가할 데이터 저장 공간 확보
	node* inData = new node;
	inData->data = data;

	node* temp = head; // 해당 위치로 이동하기 위한 포인터 변수

	// 추가할 위치가 첫번째이면 pos == 0 이면 ==> head의 값이 바뀌어야 함
	if (pos == 0) 
	{
		inData->next = head;
		head = inData;
	}
	else
	{
		for (int i = 0; i < pos - 1; ++i)
			temp = temp->next;
		inData->next = temp->next;
		temp->next = inData;
	}
	count++;
}

void LinkedList::remove(int pos)
{
}

int LinkedList::retrieve(int pos)
{
	return 0;
}

void LinkedList::draw()
{
	node* curNode = head;
	while (curNode != nullptr)
	{
		cout << curNode->data << endl;
		curNode = curNode->next;
	}
}
