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
		_gameHUD.Init();

		_levelMap = LevelObjectManager.GetInstance().InstantiateLevel(_levelName);
		_levelMap.Player.DamageTaken += OnPlayerDamageTaken;
	}
	
	public override void Exit()
	{
		GameObject.Destroy(_levelMap.gameObject);
		GameObject.Destroy(_gameHUD.gameObject);	
	}
	
	public override void Update(float deltaTime)
	{

	}

	private void OnPlayerDamageTaken(float playerHealth)
	{
		_gameHUD.SetHealth(playerHealth);
		if(playerHealth <= 0f)
		{
			OnGameOver();
		}
	}

	private void OnGameOver()
	{
		Debug.Log("GAME OVER");
	}
}
