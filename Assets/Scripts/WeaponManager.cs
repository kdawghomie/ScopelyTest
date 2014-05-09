using UnityEngine;
using System.Collections.Generic;

public class WeaponManager : MonoBehaviour {

	[SerializeField] private List<GameObject> _weaponPrefabs = null;
	private List<PlayerWeapon> _weapons = new List<PlayerWeapon>();
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
			GameObject weaponObject = Instantiate(_weaponPrefabs[i], player.WeaponPositionTransform.position, Quaternion.identity) as GameObject;
			weaponObject.transform.parent = player.transform;
			PlayerWeapon playerWeapon = weaponObject.GetComponent<PlayerWeapon>();
			playerWeapon.WeaponShoot += OnWeaponShoot;
			if(i != 0) // first weapon in list is first active weapon
			{
				weaponObject.SetActive(false);
			}
			_weapons.Add(playerWeapon);
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
				_weapons[i].gameObject.SetActive(true);
			}
			else
			{
				_weapons[i].gameObject.SetActive(false);
			}
		}
	}

	private void OnWeaponShoot(int ammo)
	{
		this.GetComponent<Player>().GameplayHUD.SetAmmo(ammo, false);
	}
	#endregion

	#region Exposed
	public void AddAmmoForWeapon(System.Type weaponType, int ammo)
	{
		for(int i = 0; i < _weapons.Count; i++)
		{
			PlayerWeapon weapon = _weapons[i];
			if(weapon.GetType() == weaponType)
			{
				int currentAmmo = weapon.AddAmmo(ammo);
				if(i == _currentWeaponIndex)
				{
					this.GetComponent<Player>().GameplayHUD.SetAmmo(currentAmmo, true);
				}
				break;
			}
		}
	}
	#endregion
}
