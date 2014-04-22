using UnityEngine;
using System.Collections;

public class StateGameplay : State 
{
	private int _levelNumber;

	public StateGameplay(int levelNumber)
	{
		_levelNumber = levelNumber;
	}

	public override void Enter()
	{
		LevelObjectManager.GetInstance().InstantiateLevel(_levelNumber);
	}
	
	public override void Exit()
	{

	}
	
	public override void Update(float deltaTime)
	{

	}
}
