using System;

public class StateGameplay : State 
{
	private string _levelName;

	public StateGameplay(string levelName)
	{
		_levelName = levelName;
	}

	public override void Enter()
	{
		LevelObjectManager.GetInstance().InstantiateLevel(_levelName);
		UIManager.GetInstance().InstantiateForegroundUI(UIManager.GetInstance().GameplayHUD);
	}
	
	public override void Exit()
	{

	}
	
	public override void Update(float deltaTime)
	{

	}
}
