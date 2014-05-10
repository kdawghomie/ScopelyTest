using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelMap : MonoBehaviour 
{
	// Constants
	private const float WAVE_TRANSITION_DELAY = 5.0f;

	// Editor variables
	[SerializeField] private GameObject _enemySpawnerPrefab = null;
	[SerializeField] private GameObject _playerPrefab = null;
	// each level prefab can determine starting enemy wave count; difficulty ramp-up
	[SerializeField] private int _startWaveEnemyCount = 10;
	[SerializeField] private int _minNextWaveEnemyIncrease = 4;
	[SerializeField] private int _maxNextWaveEnemyIncrease = 7;

	// Member variables
	private Player _player = null;
	private List<Spawner> _enemySpawners = new List<Spawner>();
	private EnemyWaveManager _enemyWaveManager;

	#region Properties
	public Player Player
	{
		get{ return _player; }
	}
	#endregion

	#region Lifecycle
	private void Awake()
	{
		_enemyWaveManager = new EnemyWaveManager(_startWaveEnemyCount, _minNextWaveEnemyIncrease, _maxNextWaveEnemyIncrease);

		GameObject[] enemySpawnMarkers = GameObject.FindGameObjectsWithTag("EnemySpawnMarker");
		foreach(GameObject enemySpawnMarker in enemySpawnMarkers)
		{
			GameObject enemySpawnerObject = Instantiate(_enemySpawnerPrefab, enemySpawnMarker.transform.position, Quaternion.identity) as GameObject;
			enemySpawnerObject.transform.parent = this.transform;
			Spawner spawner = enemySpawnerObject.GetComponent<Spawner>();
			spawner.Init(_enemyWaveManager, this.GetComponent<EnemySpawnConfig>());
			_enemySpawners.Add(spawner);
		}

		Transform playerSpawnMarker = this.transform.FindChild("PlayerSpawnMarker");
		GameObject playerObject = Instantiate(_playerPrefab, playerSpawnMarker.position, Quaternion.identity) as GameObject;
		playerObject.transform.parent = this.transform;
		_player = playerObject.GetComponent<Player>();
	}
	#endregion

	#region Exposed
	public void OnWaveComplete()
	{
		Invoke("BeginNextWave", WAVE_TRANSITION_DELAY);
	}
	#endregion

	#region Helper functions
	private void BeginNextWave()
	{
		_enemyWaveManager.BeginNextWave();
	}
	#endregion
}
