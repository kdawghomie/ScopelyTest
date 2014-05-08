using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelMap : MonoBehaviour 
{
	[SerializeField] private GameObject _enemySpawnerPrefab = null;
	[SerializeField] private GameObject _playerPrefab = null;

	private Player _player = null;
	private List<Spawner> _enemySpawners = new List<Spawner>();

	public Player Player
	{
		get{ return _player; }
	}

	void Awake()
	{
		GameObject[] enemySpawnMarkers = GameObject.FindGameObjectsWithTag("EnemySpawnMarker");
		foreach(GameObject enemySpawnMarker in enemySpawnMarkers)
		{
			GameObject enemySpawnerObject = Instantiate(_enemySpawnerPrefab, enemySpawnMarker.transform.position, Quaternion.identity) as GameObject;
			enemySpawnerObject.transform.parent = this.transform;
			_enemySpawners.Add(enemySpawnerObject.GetComponent<Spawner>());
		}

		Transform playerSpawnMarker = this.transform.FindChild("PlayerSpawnMarker");
		GameObject playerObject = Instantiate(_playerPrefab, playerSpawnMarker.position, Quaternion.identity) as GameObject;
		playerObject.transform.parent = this.transform;
		_player = playerObject.GetComponent<Player>();
	}
}
