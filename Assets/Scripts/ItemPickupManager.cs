using UnityEngine;
using System.Collections.Generic;

public class ItemPickupManager : MonoBehaviour
{
	#region singleton data
	private static ItemPickupManager _instance;
	
	public static ItemPickupManager GetInstance()
	{
		if(_instance == null)
		{
			GameObject gameScreensObject = GameObject.Find("ItemPickupManager") as GameObject;
			_instance = gameScreensObject.GetComponent<ItemPickupManager>();
		}
		return _instance;
	}
	#endregion

	#region objects
	[SerializeField] private List<GameObject> _pickups = null;
	#endregion

	#region exposed
	public GameObject InstantiateRandomPickup(Vector3 position)
	{
		int randomPickupIndex = Random.Range(0, _pickups.Count);
		GameObject result = Instantiate(_pickups[randomPickupIndex], position, Quaternion.identity) as GameObject;
		return result;
	}
	#endregion
}
