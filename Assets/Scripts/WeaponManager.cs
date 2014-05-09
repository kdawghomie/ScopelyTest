using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {

	[SerializeField] private List<GameObject> _weaponPrefabs = null;
	private List<GameObject> _weapons = new List<GameObject>();
	private int _currentWeaponIndex = 0;

	#region Lifecycle
	public void Init()
	{
		Player player = this.GetComponent<Player>();
		if(_weaponPrefabs.Count == 0)
		{
			Debug.LogError("WeaponManager: No weapons found.");
			return;
		}
		for(int i = 0; i < _weaponPrefabs.Count; i++)
		{
			GameObject weapon = Instantiate(_weaponPrefabs[i], player.WeaponPositionTransform.position, Quaternion.identity) as GameObject;
			weapon.transform.parent = player.transform;
			if(i != 0) // first weapon in list is first active weapon
			{
				weapon.SetActive(false);
			}
			_weapons.Add(weapon);
		}
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.Tab))
		{
			CycleToNextWeapon();
		}
	}
	#endregion

	#region Helper functions
	private void CycleToNextWeapon()
	{
		if(_currentWeaponIndex == _weapons.Count-1)
		{
			_currentWeaponIndex = 0;
		}
		else
		{
			_currentWeaponIndex++;
		}
		for(int i = 0; i < _weapons.Count; i++)
		{
			if(i == _currentWeaponIndex)
			{
				_weapons[i].SetActive(true);
			}
			else
			{
				_weapons[i].SetActive(false);
			}
		}
	}
	#endregion
}
