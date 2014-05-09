using UnityEngine;

public class StateGameplay : State 
{
	private string _levelName = "";
	private LevelMap _levelMap = null;
	private GameplayHUD _gameHUD = null;

	public StateGameplay(string levelName)
	{
		_levelName = levelName;
	}

	#region overriding from State
	public override void Enter()
	{
		ShowMouse(false);

		GameObject gameHUDObject = UIManager.GetInstance().InstantiateForegroundUI(UIManager.GetInstance().GameplayHUD);
		_gameHUD = gameHUDObject.GetComponent<GameplayHUD>();
		_gameHUD.Init();

		_levelMap = LevelObjectManager.GetInstance().InstantiateLevel(_levelName);
		_levelMap.Player.Init(_gameHUD);
		_levelMap.Player.PlayerDead += OnPlayerDead;
		_levelMap.Player.KilledEnemy += OnEnemyDead;
	}
	
	public override void Exit()
	{
		_levelMap.Player.enabled = false;
		GameObject.Destroy(_levelMap.gameObject);
		/* We must delay HUD destruction a bit, since we cannot destroy NGUI elements mid-frame whilst physics triggers/functions are active;
		   this is because NGUI uses DestroyImmediate when in Editor and Unity will not allow physics calls to occur after DestroyImmediate in that frame */
		GameObject.Destroy(_gameHUD.gameObject, .001f);	

		ShowMouse(true);
	}
	
	public override void Update(float deltaTime)
	{}
	#endregion

	#region private
	private void OnPlayerDead()
	{
		OnGameOver();
	}

	private void OnEnemyDead(Enemy enemyDied)
	{
		GameObject pickup = ItemPickupManager.GetInstance().InstantiateRandomPickup(enemyDied.transform.position);
		pickup.transform.parent = _levelMap.transform;

		Enemy[] enemies = (Enemy[])GameObject.FindObjectsOfType(typeof(Enemy));
		bool enemiesStillAlive = false;
		foreach(Enemy enemy in enemies)
		{
			if(!enemy.Dead)
			{
				enemiesStillAlive = true;
				break;
			}
		}

		if(!enemiesStillAlive)
		{
			OnWaveComplete();
		}
	}

	private void OnWaveComplete()
	{
		_gameHUD.DisplayWaveCompleteLabel();
		_levelMap.OnWaveComplete();
	}

	private void OnGameOver()
	{
		_stateMachine.SetState(new StateGameOver(_levelName, _gameHUD.Score));
	}
	#endregion
}
