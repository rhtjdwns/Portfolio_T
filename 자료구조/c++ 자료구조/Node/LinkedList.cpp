#include "LinkedList.h"
#include <iostream>
using namespace std;

void LinkedList::insert(int pos, int data)
{
	if (pos < 0 || pos > count)
		return;
	// �߰��� ������ ���� ���� Ȯ��
	node* inData = new node;
	inData->data = data;

	node* temp = head; // �ش� ��ġ�� �̵��ϱ� ���� ������ ����

	// �߰��� ��ġ�� ù��°�̸� pos == 0 �̸� ==> head�� ���� �ٲ��� ��
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
