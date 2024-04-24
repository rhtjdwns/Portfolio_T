#pragma once
#include "arrayStack.h"
#include <assert.h>

class SimpleCalc
{
	char* postfix;					// ����ǥ�� ���� ����
	arrayStack<float> floatStack;	// ���ڸ� ������ ����
	float result;					// ��� ����� ������ ����

	int curReadPos;					// ���� ���Ŀ��� �о�帮�� ��ġ
public:
	SimpleCalc(int max = 100) : result(0), curReadPos(0) {
		postfix = new char[max];
		assert(postfix != nullptr);
	}
	~SimpleCalc() {
		if (postfix)
			delete postfix;
	}

	void setPostfix(const char* str);
	int getToken(char* token);				// ���ڿ��� ��ū���� �и�
	float getResult() { return result; }
	void calc();
	void draw();
};

