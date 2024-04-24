#include "SimpleCalc.h"
#include <iostream>
using namespace std;

enum TOKENTYPE {PLUS, MINUS, MULTI, DIVIDE, NUMBER, BLANK, END};
char op[] = { '+', '-', '*', '/' };

void SimpleCalc::setPostfix(const char* str)
{
	//strcpy_s(postfix, 100, str);
	memcpy(postfix, str, strlen(str) + 1);
}

// 문자열 넣기
int SimpleCalc::getToken(char* token)
{
	int tokenPos = 0;
	switch (postfix[curReadPos])
	{
	case '+':
		token[tokenPos++] = postfix[curReadPos++];
		token[tokenPos] = '\0';
		return PLUS;
	case '-':
		token[tokenPos++] = postfix[curReadPos++];
		token[tokenPos] = '\0';
		return MINUS;
	case '*':
		token[tokenPos++] = postfix[curReadPos++];
		token[tokenPos] = '\0';
		return MULTI;
	case '/':
		token[tokenPos++] = postfix[curReadPos++];
		token[tokenPos] = '\0';
		return DIVIDE;
	case ' ':
		curReadPos++;
		return BLANK;
	case '\0':
	case '=':
		return END;
	default: // 숫자처리부분
		while (postfix[curReadPos] >= '0' && postfix[curReadPos] <= '9')
		{
			token[tokenPos++] = postfix[curReadPos++];
		}
		token[tokenPos] = '\0';
		return NUMBER;
	}
	return -1;
}

void SimpleCalc::calc()
{
	float popVal1, popVal2;			// pop data
	float number = 0;				// 문자열로 가져온 숫자문자열을 숫자로 변경하기 위한 변수
	char token[20];

	curReadPos = 0;
	int curToken;
	while ((curToken = getToken(token)) != END)
	{
		switch (curToken)
		{
		case PLUS:
			popVal2 = floatStack.Pop();
			popVal1 = floatStack.Pop();
			floatStack.Push(popVal1 + popVal2);
			break;
		case MINUS:
			popVal2 = floatStack.Pop();
			popVal1 = floatStack.Pop();
			floatStack.Push(popVal1 - popVal2);
			break;
		case MULTI:
			popVal2 = floatStack.Pop();
			popVal1 = floatStack.Pop();
			floatStack.Push(popVal1 * popVal2);
			break;
		case DIVIDE:
			popVal2 = floatStack.Pop();
			popVal1 = floatStack.Pop();
			floatStack.Push(popVal1 / popVal2);
			break;
		case BLANK:
			break;
		case NUMBER:
			number = 0;
			// 문자열을 숫자로 변환
			// 3, 5, null
			// 3 -> number = 10 * 0 + '3' - '0' = 3
			// 5 -> number = 10 * 3 + '5' - '0' = 35
			for (int i = 0; token[i] != '\0'; ++i)
				number = 10 * number + token[i] - '0';
			floatStack.Push(number);
			break;
		}
	}
	// 최종 결과 -> 스택에 있음
	result = floatStack.Pop();
}

void SimpleCalc::draw()
{
	cout << "Postfix : " << postfix << " = " << result << endl;
}
