#include "util.h"

void DrawTest()
{

}

void main()
{
	ScreenInit();

	// 게임 로직
	// 1. 화면 클리어
	// 2. 메모리에 화면 출력 내용을 저장
	// 3. 메모리버퍼 활성화 => 더블 버퍼링
	// 4. 입력처리
	// 5. 업데이트

	ScreenClear();
	while (1)
	{
		SetColor(15);
		ScreenPrint(5, 5, "A");
		ScreenPrint(10, 5, "B");

		SetColor(10);
		DrawTest();

		ScreenFlipping();
	}
	ScreenRelease();
}