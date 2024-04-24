#pragma once
#include "Interfaces.h"

class EnterMineAndDigForNugget :public IState
{
private:
	CMiner* Instance;
public:
	virtual void Enter(CMiner* instance);
	virtual void Execute(CMiner * instance);
	virtual void Exit(CMiner * instance);
};

class VisitBankAndDepositGold :public IState
{
private:
	CMiner* Instance;
public:
	virtual void Enter(CMiner* instance);
	virtual void Execute(CMiner* instance);
	virtual void Exit(CMiner* instance);
};

class GoHomeAndSleepTilRested : public IState
{
private:
	CMiner* Instance;

public:
	virtual void Enter(CMiner* instance);
	virtual void Execute(CMiner* instance);
	virtual void Exit(CMiner* instance);
};

class QuenchThirst :public IState
{
private:
	CMiner* Instance;

public:
	virtual void Enter(CMiner* instance);
	virtual void Execute(CMiner* instance);
	virtual void Exit(CMiner* instance);
};