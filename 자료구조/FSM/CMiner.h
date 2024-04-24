#pragma once
#include "Interfaces.h"

class CMiner : public IBaseGameEntity
{
private:
	IState* currentState;

	int gold;
	int thirst;
	int deposit;
	int tired;

public:
	CMiner(int _gold, int _thirst, int _deposit, int _tired) :
		gold(_gold), thirst(_thirst), deposit(_deposit), tired(_tired) {};
	
	virtual void Update();

	void ChangeState(IState* pNewState);

	int GetGold() { return gold; };
	void SetGold(int value) { gold = value; };
	
	int GetThirst() { return thirst; };
	void SetThirst(int value) { thirst = value; };

	int GetDeposit() { return deposit; };
	void SetDeposit(int value) { deposit = value; };
	
	int GetTired() { return tired; };
	void SetTired(int value) { tired = value; };
};
