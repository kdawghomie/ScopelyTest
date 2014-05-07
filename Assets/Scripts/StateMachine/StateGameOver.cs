using UnityEngine;

public class StateGameOver : State 
{
	private GameOverUI _gameOverUI;
	private string _levelName;

	public StateGameOver(string levelName)
	{
		_levelName = levelName;
	}
	
	public override void Enter()
	{
		GameObject gameOverObject = UIManager.GetInstance().InstantiateForegroundUI(UIManager.GetInstance().GameOverUI);
		_gameOverUI = gameOverObject.GetComponent<GameOverUI>();
		_gameOverUI.MainMenuPressed += OnMainMenuPressed;
		_gameOverUI.RetryPressed += OnRetryPressed;
	}
	
	public override void Exit()
	{
		GameObject.Destroy(_gameOverUI.gameObject);
	}

	public override void Update(float deltaTime)
	{}

	private void OnMainMenuPressed()
	{
		_stateMachine.SetState(new StateGameStart());
	}
	
	private void OnRetryPressed()
	{
		_stateMachine.SetState(new StateGameplay(_levelName));
	}
}
