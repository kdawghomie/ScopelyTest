using UnityEngine;

public class StateGameplay : State 
{
	private string _levelName;
	private LevelMap _levelMap;
	private GameplayHUD _gameHUD;

	public StateGameplay(string levelName)
	{
		_levelName = levelName;
	}

	public override void Enter()
	{
		GameObject gameHUDObject = UIManager.GetInstance().InstantiateForegroundUI(UIManager.GetInstance().GameplayHUD);
		_gameHUD = gameHUDObject.GetComponent<GameplayHUD>();

		_levelMap = LevelObjectManager.GetInstance().InstantiateLevel(_levelName);
	}
	
	public override void Exit()
	{
		GameObject.Destroy(_levelMap.gameObject);
		GameObject.Destroy(_gameHUD.gameObject);	
	}
	
	public override void Update(float deltaTime)
	{

	}
}
