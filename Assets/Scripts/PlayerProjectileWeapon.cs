using UnityEngine;
using System.Collections;

public abstract class PlayerProjectileWeapon : PlayerWeapon 
{
	[SerializeField] GameObject _projectilePrefab;
	[SerializeField] Transform _projectileSpawnTransform;

	override protected void Shoot()
	{
		base.Shoot();
		GameObject projectile = Instantiate(_projectilePrefab, _projectileSpawnTransform.position, this.transform.rotation) as GameObject;
	}
}
