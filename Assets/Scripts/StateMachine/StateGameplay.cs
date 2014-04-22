using UnityEngine;
using System.Collections;

public class StateGameplay : State 
{
	public StateGameplay(){}

	public override void Enter()
	{
		GameScreens.GetInstance().InstantiateGameObject(GameScreens.GetInstance().Level1);
		//Instantiate(GameScreens.GetInstance().Level1, Vector3.zero, Quaternion.identity);
	}
	
	public override void Exit()
	{

	}
	
	public override void Update(float deltaTime)
	{

	}
}
