#include "CMiner.h"
#include "MinerStates.h"
#include <Windows.h>

EnterMineAndDigForNugget* _EnterMineAndDigForNugget = new EnterMineAndDigForNugget();
VisitBankAndDepositGold* _VisitBankAndDepositGold = new VisitBankAndDepositGold();
GoHomeAndSleepTilRested* _GoHomeAndSleepTilRested = new GoHomeAndSleepTilRested();
QuenchThirst* _QuenchThirst = new QuenchThirst();

int main() {
	CMiner* Entity = new CMiner(0, 0, 0, 0);
	Entity->ChangeState(_GoHomeAndSleepTilRested);
	//EnterMineAndDigForNugget* _EnterMineAndDigForNugget = new EnterMineAndDigForNugget();
	//VisitBankAndDepositGold* _VisitBankAndDepositGold = new VisitBankAndDepositGold();
	//GoHomeAndSleepTilRested* _GoHomeAndSleepTilRested = new GoHomeAndSleepTilRested();
	//QuenchThirst* _QuenchThirst = new QuenchThirst();


	while(true)
	{	
		Entity->Update();
		Sleep(800);
	}
}