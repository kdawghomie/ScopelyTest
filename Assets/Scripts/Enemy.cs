using UnityEngine;
using System.Collections;

public abstract class Enemy : MonoBehaviour {

	[SerializeField] protected float ATTACK_DAMAGE = 1.0f;
	[SerializeField] protected float ATTACK_RESET_TIME = 1.0f;
	[SerializeField] protected float MOVE_SPEED = 32.0f;
	[SerializeField] protected int KILL_SCORE = 100;
	[SerializeField] protected float ITEM_PICKUP_DROP_ODDS = 1.0f;
	[SerializeField] protected float HEALTH = 100f;

	public const float CORPSE_REMOVAL_DELAY = 5.0f;
	
	protected Player _player;
	protected int _updates;
	protected float _timeDead;
	protected bool _dead = false;
	protected bool _canAttack = true;

	#region Properties
	public int KillScore
	{
		get{ return KILL_SCORE; }
	}

	public float ItemPickupDropOdds
	{
		get{ return ITEM_PICKUP_DROP_ODDS; }
	}

	public bool Dead
	{
		get{ return _dead; }
	}
	#endregion

	#region Unity Lifecycle
	protected virtual void Awake(){
		this.GetComponent<MeshRenderer>().enabled = false;
		this.collider.enabled = true;
		this.rigidbody.angularDrag = 0.15f;
		this.rigidbody.drag = 1000.0f;
	}
	protected virtual void Start () {
		
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

	protected virtual void LateUpdate (){

		// If dead, do nothing
		if (_dead){
			return;
		}

		// Let the physics engine take over again
		this.rigidbody.isKinematic = false;
	}

	protected virtual void Update () {

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

	protected void OnCollisionStay(Collision collision)
	{
		if(collision.gameObject.name == "PlayerCollider")
		{
			AttackPlayer();
		}
	}
	#endregion
	
	#region Internal Helpers
	protected virtual void MoveTowardsPlayer(){

		// Early return if there is no player
		if (_player == null){
			return;
		}
		
		// Chase after player
		Vector3 directionToPlayer = _player.transform.position - this.transform.position;
		directionToPlayer.Normalize();
		Movement.Move(this.gameObject, directionToPlayer, MOVE_SPEED);
	}
	protected virtual void AttackPlayer()
	{
		if(!_dead && _canAttack)
		{
			_player.Attack(ATTACK_DAMAGE);
			_canAttack = false;
			Invoke("ResetCanAttack", ATTACK_RESET_TIME);
		}
	}
	protected void ResetCanAttack()
	{
		_canAttack = true;
	}
	protected void Ragdoll(){
		this.rigidbody.constraints = RigidbodyConstraints.None;
		this.rigidbody.useGravity = true;
		this.rigidbody.drag = 0.1f;
	}
	protected void AnimateBulletImpact(Vector3 impactDirection, Vector3 impactPosition){
		Ragdoll();
		this.rigidbody.AddForceAtPosition(impactDirection * 3000, impactPosition);
	}
	protected void AnimateExplosion(Vector3 center, float force, float radius){
		Ragdoll();
		this.rigidbody.AddExplosionForce(force, center, radius);
	}
	protected virtual void TakeDamage(float damage)
	{
		if(!_dead)
		{
			HEALTH -= damage;
			if(HEALTH <= 0f)
			{
				HEALTH = 0f;
				_dead = true;
				_player.OnKilledEnemy(this);
			}
		}
	}
	#endregion

	#region Exposed
	public virtual void Damage(Vector3 impactDirection, Vector3 impactPosition, float damage){
		TakeDamage(damage);
		if(_dead)
		{
			AnimateBulletImpact(impactDirection, impactPosition);
		}
	}
	public virtual void Explode(Vector3 explosionCenter, float explosionForce, float explosionRadius, float damage){
		TakeDamage(damage);
		if(_dead)
		{
			AnimateExplosion(explosionCenter, explosionForce, explosionRadius);
		}
	}
	#endregion
}
