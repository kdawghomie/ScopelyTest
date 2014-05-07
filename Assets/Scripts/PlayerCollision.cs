using UnityEngine;

public class PlayerCollision : MonoBehaviour 
{
	private void OnTriggerEnter(Collider collider)
	{
		if(collider.gameObject.tag == "Pickup")
		{
			GameObject.Destroy(collider.gameObject);
		}
	}
}
