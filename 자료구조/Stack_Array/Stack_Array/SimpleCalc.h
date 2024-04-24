#pragma once
#include "arrayStack.h"
#include <assert.h>

class SimpleCalc
{
	char* postfix;					// 후위표현 수식 저장
	arrayStack<float> floatStack;	// 숫자를 관리할 스택
	float result;					// 계산 결과를 저장할 변수

	int curReadPos;					// 현재 수식에서 읽어드리는 위치
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
	int getToken(char* token);				// 문자열을 토큰으로 분리
	float getResult() { return result; }
	void calc();
	void draw();
};

