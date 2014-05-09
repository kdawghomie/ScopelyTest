using UnityEngine;
using System.Collections;

public abstract class PlayerRaycastWeapon : PlayerWeapon 
{
	public const float IMPACT_OFFSET = 0.01f;
	public const float ACCURACY_DELTA = 3.0f;

	public GameObject bloodSpray;

	override protected void Shoot()
	{
		base.Shoot();
		
		// Calculate shoot direction
		Quaternion shootRotation = _cam.transform.rotation;
		float angle = Random.value * Mathf.PI * 2.0f;
		float radius = Mathf.Sqrt(Random.value) * ACCURACY_DELTA; 
		Vector3 shootVector = (shootRotation * GetOffsetQuaternion(radius, angle)) * Vector3.forward;
		
		// Play gunshot sound
		AudioSource.PlayClipAtPoint(shootSound, this.transform.position);
		
		// Do Raycast and find collision point
		RaycastHit hit = new RaycastHit ();
		bool collided = Physics.Raycast (_cam.transform.position, shootVector, out hit);
		if (!collided) {
			return;
		}
		
		// Damage the enemy
		bool hitEnemy = false;
		if (hit.collider.gameObject.tag == "Enemy") {
			try{
				Enemy e = hit.collider.gameObject.GetComponent<Enemy>();
				e.Damage(-hit.normal, hit.point, DAMAGE_AMOUNT);
				hitEnemy = true;
			}catch{
				Debug.LogError("PlayerWeapon: Shoot: not an enemy");
				return;
			}
		}
		
		// Create bullet impact effect
		Vector3 impactPoint = hit.point + hit.normal * IMPACT_OFFSET;
		if (hitEnemy){
			Effect (impactPoint, bloodSpray);
		}else{
			Effect (impactPoint, bulletImpact);
		}
	}
}
