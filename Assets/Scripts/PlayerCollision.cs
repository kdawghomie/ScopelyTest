using UnityEngine;

public class PlayerCollision : MonoBehaviour 
{
	private Player _player = null;

	private void Awake()
	{
		_player = this.transform.parent.GetComponent<Player>();
	}

	private void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "Pickup")
		{
			GameObject pickupObject = collider.gameObject;
			if(pickupObject.GetComponent<HealthPickup>() != null)
			{
				HealthPickup healthPickup =  pickupObject.GetComponent<HealthPickup>();
				_player.AddHealth(healthPickup.healAmount);
			}

			GameObject.Destroy(collider.gameObject);
		}
	}
}
