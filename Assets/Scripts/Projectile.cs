using UnityEngine;
using System.Collections.Generic;

public class Projectile : MonoBehaviour 
{
	[SerializeField] private float _initialVelocity = 500.0f;
	[SerializeField] private float _maxVelocity = 500.0f;
	[SerializeField] private float _acceleration = 50.0f;
	[SerializeField] private float _explosiveRadius = 500.0f;
	[SerializeField] private float _explosiveForce = 20000.0f;
	[SerializeField] private GameObject _impactEffect = null;

	private float _currentVelocity;
	private float _damage;
	
	public void Init(float damage)
	{
		this.rigidbody.velocity = this.transform.forward*_initialVelocity;
		_currentVelocity = _initialVelocity;
		_damage = damage;
	}

	private void Update()
	{
		Accelerate();
	}

	private void Accelerate()
	{
		if(_currentVelocity != _maxVelocity)
		{
			_currentVelocity += _acceleration*Time.deltaTime;
			if(_currentVelocity > _maxVelocity)
			{
				_currentVelocity = _maxVelocity;
			}
			this.rigidbody.velocity = this.transform.forward*_currentVelocity;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		bool canCollide = collider.gameObject.GetComponent<ItemPickup>() == null;

		if(canCollide)
		{
			GameObject collidingObject = collider.gameObject;
			GameObject.Instantiate(_impactEffect, this.transform.position, Quaternion.identity);
			DamageEnemies(collider);

			GameObject.Destroy(this.gameObject);
		}
	}

	private void DamageEnemies(Collider collider)
	{
		List<Enemy> enemiesHit = new List<Enemy>();
		Enemy[] enemies = (Enemy[])GameObject.FindObjectsOfType(typeof(Enemy));

		foreach(Enemy enemy in enemies)
		{
			float distanceEnemyToProjectile = Vector3.Distance(enemy.transform.position, this.transform.position);
			if(distanceEnemyToProjectile <= _explosiveRadius)
			{
				enemiesHit.Add(enemy);
			}
		}

		foreach(Enemy enemyHit in enemiesHit)
		{
			enemyHit.Explode(this.transform.position, _explosiveForce, _explosiveRadius, _damage);
		}
	}
}
