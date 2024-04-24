#include "MinerStates.h"
#include "CMiner.h"
#include <iostream>

extern EnterMineAndDigForNugget* _EnterMineAndDigForNugget;
extern VisitBankAndDepositGold* _VisitBankAndDepositGold;
extern GoHomeAndSleepTilRested* _GoHomeAndSleepTilRested;
extern QuenchThirst* _QuenchThirst;

#pragma region EnterMineAndDigForNugget
void EnterMineAndDigForNugget::Enter(CMiner* instance)
{
	std::cout << "Miner Entered Mine" << std::endl;
}

void EnterMineAndDigForNugget::Execute(CMiner* instance)
{
	std::cout << "Mining" << std::endl;
	instance->SetGold(instance->GetGold() + 1);
	instance->SetTired(instance->GetTired() + 1);
	instance->SetThirst(instance->GetThirst() + 1);

	if (instance->GetGold() > 5)
		instance->ChangeState(_VisitBankAndDepositGold);
	if (instance->GetTired() > 3)
		instance->ChangeState(_GoHomeAndSleepTilRested);
	if (instance->GetThirst() > 2)
		instance->ChangeState(_QuenchThirst);
}

void EnterMineAndDigForNugget::Exit(CMiner* instance)
{
	std::cout << "Exit Mine" << std::endl;
}
#pragma endregion

#pragma region VisitBankAndDepositGold
void VisitBankAndDepositGold::Enter(CMiner* instance)
{
	std::cout << "Visited Bank" << std::endl;
}

void VisitBankAndDepositGold::Execute(CMiner* instance)
{
	std::cout << "Depositing Gold to Bank" << std::endl;
	instance->SetDeposit(instance->GetGold());
	instance->SetGold(0);
	if (instance->GetGold() <= 0)
		instance->ChangeState(_EnterMineAndDigForNugget);
}

void VisitBankAndDepositGold::Exit(CMiner* instance)
{
	std::cout << "Leaving Bank" << std::endl;
	
}
#pragma endregion

#pragma region GoHomeAndSleepTilRested
void GoHomeAndSleepTilRested::Enter(CMiner* instance)
{
	std::cout << "Heading Home" << std::endl;
}

void GoHomeAndSleepTilRested::Execute(CMiner* instance)
{
	std::cout << "Sleeping" << std::endl;
	instance->SetTired(instance->GetTired() - 1);

	if (instance->GetTired() <= 0)
		instance->ChangeState(_EnterMineAndDigForNugget);
}

void GoHomeAndSleepTilRested::Exit(CMiner* instance)
{
	std::cout << "Leaving Home" << std::endl;
}
#pragma endregion

#pragma region QuenchThirst
void QuenchThirst::Enter(CMiner* instance)
{
	std::cout << "Going to Pub" << std::endl;
}

void QuenchThirst::Execute(CMiner* instance)
{
	std::cout << "Drinking" << std::endl;
	instance->SetThirst(instance->GetThirst() - 1);
	if (instance->GetThirst() <= 0)
		instance->ChangeState(_EnterMineAndDigForNugget);
}

void QuenchThirst::Exit(CMiner* instance)
{
	std::cout << "Leaving Pub" << std::endl;
}
#pragma endregion