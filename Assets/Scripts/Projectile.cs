using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour 
{
	[SerializeField] private float _initialVelocity = 500.0f;
	[SerializeField] private float _maxVelocity = 500.0f;
	[SerializeField] private float _acceleration = 50.0f;
	[SerializeField] private GameObject _impactEffect = null;

	private float _currentVelocity;

	private void Awake()
	{
		this.rigidbody.velocity = this.transform.forward*_initialVelocity;
		_currentVelocity = _initialVelocity;
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
			Effect(this.transform.position, _impactEffect);
			GameObject.Destroy(this.gameObject);
		}
	}

	private void Effect(Vector3 position, GameObject effectObject){
		GameObject effect = GameObject.Instantiate(effectObject) as GameObject;
		effect.transform.position = position;
	}
}
