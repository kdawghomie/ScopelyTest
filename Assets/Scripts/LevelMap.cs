using UnityEngine;
using System.Collections;

public class LevelMap : MonoBehaviour 
{
	[SerializeField] private GameObject _enemySpawnerPrefab = null;
	[SerializeField] private GameObject _playerPrefab = null;

	void Awake()
	{
		GameObject[] enemySpawnMarkers = GameObject.FindGameObjectsWithTag("EnemySpawnMarker");
		foreach(GameObject enemySpawnMarker in enemySpawnMarkers)
		{
			GameObject enemySpawnerObject = Instantiate(_enemySpawnerPrefab, enemySpawnMarker.transform.position, Quaternion.identity) as GameObject;
			enemySpawnerObject.transform.parent = this.transform;
		}

		GameObject playerObject = Instantiate(_playerPrefab, _playerPrefab.transform.position, Quaternion.identity) as GameObject;
		playerObject.transform.parent = this.transform;
	}
}
