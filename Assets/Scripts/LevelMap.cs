﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelMap : MonoBehaviour 
{
	[SerializeField] private GameObject _enemySpawnerPrefab = null;
	[SerializeField] private GameObject _playerPrefab = null;
	// each level prefab can determine starting enemy wave count; difficulty ramp-up
	[SerializeField] private int _startWaveEnemyCount = 10;
	[SerializeField] private int _minNextWaveEnemyIncrease = 4;
	[SerializeField] private int _maxNextWaveEnemyIncrease = 7;

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
			spawner.Init(_enemyWaveManager);
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
		Invoke("BeginNextWave", 3.0f);
	}
	#endregion

	#region Helper functions
	private void BeginNextWave()
	{
		_enemyWaveManager.BeginNextWave();
		foreach(Spawner enemySpawner in _enemySpawners)
		{
			enemySpawner.WaveComplete = false;
		}
	}
	#endregion
}
