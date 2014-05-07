using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

	public const float ATTACK_DAMAGE = 10.0f;
	public const float ATTACK_RESET_TIME = 1.25f;
	public const float MOVE_SPEED = 32.0f;
	public const float CORPSE_REMOVAL_DELAY = 5.0f;
	
	private Player _player;
	private int _updates;
	private float _timeDead;
	private bool _dead = false;
	private bool _canAttack = true;
	private float _health = 100f;
	
	#region Unity Lifecycle
	void Awake(){
		this.GetComponent<MeshRenderer>().enabled = false;
		this.collider.enabled = true;
		this.rigidbody.angularDrag = 0.15f;
		this.rigidbody.drag = 1000.0f;
	}
	void Start () {
		
		// Get a reference to the Player. So we can chase him
		_timeDead = Random.value * CORPSE_REMOVAL_DELAY * 0.33f;
		_player = (Player)GameObject.FindObjectOfType(typeof(Player));
		if (_player == null){
			Debug.LogError("Enemy: Start: cant find player");
			return;
		}

		// Make sure this doesn't spawn in the air
		MoveTowardsPlayer();
	}

	void LateUpdate (){

		// If dead, do nothing
		if (_dead){
			return;
		}

		// Let the physics engine take over again
		this.rigidbody.isKinematic = false;
	}

	void Update () {

		// Remove corpse after a timeout
		if (_dead){
			_timeDead += Time.deltaTime;
			if (_timeDead > CORPSE_REMOVAL_DELAY){
				GameObject.Destroy(this.gameObject);
			}
			return;
		}

		// Make kinematic so we can move it around without breaking thinfgs
		this.rigidbody.isKinematic = true;

		// Move towards player
		MoveTowardsPlayer();

		// Make visible again
		_updates++;
		if (_updates == 2){
			this.GetComponent<MeshRenderer>().enabled = true;
		}
	}
	#endregion
	
	#region Internal Helpers
	private void MoveTowardsPlayer(){

		// Early return if there is no player
		if (_player == null){
			return;
		}
		
		// Chase after player
		Vector3 directionToPlayer = _player.transform.position - this.transform.position;
		if( directionToPlayer.magnitude <= 15f )
		{
			AttackPlayer();
		}

		directionToPlayer.Normalize();
		Movement.Move(this.gameObject, directionToPlayer, MOVE_SPEED);
	}
	private void AttackPlayer()
	{
		if(_canAttack)
		{
			_player.AttackWithDamage(ATTACK_DAMAGE);
			_canAttack = false;
			Invoke("ResetCanAttack", ATTACK_RESET_TIME);
		}
	}
	private void ResetCanAttack()
	{
		_canAttack = true;
	}
	private void Ragdoll(){
		this.rigidbody.constraints = RigidbodyConstraints.None;
		this.rigidbody.useGravity = true;
		this.rigidbody.drag = 0.1f;
	}
	private void AnimateBulletImpact(Vector3 impactDirection, Vector3 impactPosition){
		Ragdoll();
		this.rigidbody.AddForceAtPosition(impactDirection * 3000, impactPosition);
	}
	private void AnimateExplosion(Vector3 center, float force, float radius){
		Ragdoll();
		this.rigidbody.AddExplosionForce(force, center, radius);
	}
	private void TakeDamage(float damage)
	{
		_health -= damage;
		if(_health <= 0f)
		{
			_health = 0f;
			_dead = true;
		}
	}
	#endregion

	#region Exposed
	public void Damage(Vector3 impactDirection, Vector3 impactPosition, float damage){
		TakeDamage(damage);
		if(_dead)
		{
			AnimateBulletImpact(impactDirection, impactPosition);
		}
	}
	public void Explode(Vector3 explosionCenter, float explosionForce, float explosionRadius, float damage){
		TakeDamage(damage);
		if(_dead)
		{
			AnimateExplosion(explosionCenter, explosionForce, explosionRadius);
		}
	}
	#endregion
}
