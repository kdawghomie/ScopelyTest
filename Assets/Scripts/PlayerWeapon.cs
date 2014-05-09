using UnityEngine;
using System.Collections;

public abstract class PlayerWeapon : MonoBehaviour {
	// Editor variables
	public float SHOOT_COOLDOWN = 0.15f;
	public float DAMAGE_AMOUNT = 35.0f;
	public int MAX_AMMO = 100;
	public AudioClip shootSound;
	public GameObject bulletImpact;

	// Events
	public delegate void WeaponShootHandler(int ammo);
	public event WeaponShootHandler WeaponShoot = null;

	// Member variables
	protected float _shootCooldown;
	protected Camera _cam;
	protected int _currentAmmo;
	protected bool _allowShooting = true;

	#region Properties
	public int CurrentAmmo
	{
		get{ return _currentAmmo; }
	}
	#endregion

	#region Lifecycle
	public void Init() {
		
		// Get reference to Camera
		_cam = Camera.main;
		if (_cam == null){
			Debug.LogError("PlayerWeapon: Start: could not find required Camera component");
			return;
		}

		_currentAmmo = MAX_AMMO;
		_shootCooldown = SHOOT_COOLDOWN;
	}
	protected void Update () 
	{
		if(!_allowShooting) 
		{
			_shootCooldown -= Time.deltaTime;
			if(_shootCooldown <= 0f)
			{
				_shootCooldown = SHOOT_COOLDOWN;
				_allowShooting = true;
				Debug.Log("allow shoot");
			}
		} 
		else
		{
			/*// Press spacebar to shoot
			if(Input.GetKey(KeyCode.Space) && _currentAmmo > 0) 
			{
				Shoot();
			}*/
		}
	}
	#endregion
	
	#region Exposed
	public int AddAmmo(int ammo)
	{
		_currentAmmo += ammo;
		if(_currentAmmo > MAX_AMMO)
		{
			_currentAmmo = MAX_AMMO;
		}
		return _currentAmmo;
	}
	#endregion

	#region Gun Helpers
	protected void Effect(Vector3 position, GameObject effectObject){
		GameObject effect = GameObject.Instantiate(effectObject) as GameObject;
		effect.transform.position = position;
	}
	protected Quaternion GetOffsetQuaternion(float radius, float angleInRadians){
		Quaternion xQuaternion = Quaternion.AngleAxis(Mathf.Sin (angleInRadians) * radius, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis(Mathf.Cos (angleInRadians) * radius, Vector3.left);
		return xQuaternion * yQuaternion;
	}
	protected virtual void Shoot()
	{
		_allowShooting = false;

		_currentAmmo--;
		if(WeaponShoot != null)
		{
			WeaponShoot(_currentAmmo);
		}
	}
	#endregion

	#region Exposed
	public void TryShoot()
	{
		if(_allowShooting && _currentAmmo > 0)
		{
			Shoot();
		}
	}
	#endregion
}
