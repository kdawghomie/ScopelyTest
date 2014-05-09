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
			OnGotPickup(collider.gameObject);
		}
	}

	private void OnGotPickup(GameObject pickupObject)
	{
		if(pickupObject.GetComponent<HealthPickup>() != null)
		{
			HealthPickup healthPickup =  pickupObject.GetComponent<HealthPickup>();
			_player.AddHealth(healthPickup.healAmount);
		}
		else if(pickupObject.GetComponent<BulletsPickup>() != null)
		{
			BulletsPickup bulletsPickup = pickupObject.GetComponent<BulletsPickup>();
			_player.GetComponent<WeaponManager>().AddAmmoForWeapon(typeof(PlayerWeaponAK47), bulletsPickup.ammoAmount);
		}

		GameObject.Destroy(pickupObject.gameObject);
	}
}
