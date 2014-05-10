using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	// Rendering Constants
	public const float NEAR_Z = 0.3f;
	public const float DEFAULT_VIEW_DISTANCE = 1000.0f;
	public const float FOG_DEPTH = 800.0f;
	
	// Input Constants
	public const float SENSITIVITY_X = 4.0f;
	public const float SENSITIVITY_Y = 4.0f;
	public const float MAX_VERTICAL_LOOK_DEGREES = 40.0f;
	
	// Player Constants
	public const float MOVE_SPEED = 75.0f;
	public const float PLAYER_HEIGHT = 16.0f;

	// Events
	public delegate void PlayerDeadHandler();
	public event PlayerDeadHandler PlayerDead = null;

	public delegate void KilledEnemyHandler(Enemy enemy);
	public event KilledEnemyHandler KilledEnemy = null;

	// Private member variables
	private Camera _cam;
	private float _rotX;
	private float _rotY;
	private Quaternion _initialCameraRot;
	private Transform _weaponPositionTransform;
	private Transform _capsuleCollider;
	private Quaternion _capsuleColliderRotation;

	private float _health = 100.0f;

	private GameplayHUD _gameplayHUD;

	#region Properties
	public Transform WeaponPositionTransform
	{
		get{ return _weaponPositionTransform; }
	}
	public GameplayHUD GameplayHUD
	{
		get{ return _gameplayHUD; }
	}
	#endregion

	#region Unity Lifecycle
	public void Init(GameplayHUD gameplayHUD)
	{
		_gameplayHUD = gameplayHUD;

		_weaponPositionTransform = this.transform.FindChild("WeaponPositionMarker");

		_capsuleCollider = this.transform.FindChild("PlayerCollider");
		_capsuleColliderRotation = _capsuleCollider.rotation;

		// Get required components and default camera orientation
		_cam = Camera.main;
		if (_cam == null){
			Debug.LogError("Player: Start: _cam is null");
			return;
		}
		
		_initialCameraRot = _cam.transform.rotation;
		
		// Set default render settings
		DefaultFog();
		DefaultClipping();
		
		// Fall to ground if Camera was placed too high
		Move(Vector3.up, 0.0f);

		this.GetComponent<WeaponManager>().Init();
	}

	void Update () {

		// Let the player look, shoot, and move
		UpdateLookRotation ();
		UpdatePosition ();
	}
	#endregion

	#region Public
	public void Attack(float damage)
	{
		float newHealth = _health - damage;
		if(newHealth <= 0f)
		{
			if(PlayerDead != null)
			{
				PlayerDead();
			}
		}
		SetHealth(newHealth);
		_gameplayHUD.DisplayDamageIndicator();
	}

	public void AddHealth(float add)
	{
		SetHealth(_health + add);
	}

	public void SetHealth(float newHealth)
	{
		if(newHealth > 100f)
		{
			newHealth = 100f;
		}
		else if(newHealth < 0f)
		{
			newHealth = 0f;
		}
		_health = newHealth;
		_gameplayHUD.SetHealth(_health);
	}

	public void OnKilledEnemy(Enemy enemy)
	{
		_gameplayHUD.AddScore(enemy.KillScore);

		if(KilledEnemy != null)
		{
			KilledEnemy(enemy);
		}
	}
	#endregion

	#region Render Settings
	private void DefaultFog(){
		
		// Make fog respect the player's view distance
		RenderSettings.fog = true;
		RenderSettings.fogColor = new Color (0.25f, 0.3f, 0.35f);
		RenderSettings.fogMode = FogMode.Linear;
		RenderSettings.fogEndDistance = DEFAULT_VIEW_DISTANCE;
		RenderSettings.fogStartDistance = DEFAULT_VIEW_DISTANCE - FOG_DEPTH;
	}
	private void DefaultClipping(){
		
		// Adjust the clipping to match the player's view distance
		_cam.near = NEAR_Z;
		_cam.far = DEFAULT_VIEW_DISTANCE;
	}
	#endregion

	#region Move Helpers
	private void Move(Vector3 direction, float speed){
		if (Movement.Move(this.gameObject, direction, speed)){
			this.transform.position += new Vector3(0, PLAYER_HEIGHT, 0);
		}
	}
	private void UpdatePosition(){
		// Check keyboard input
		Vector3 moveDirection = new Vector3();
		if (Input.GetKey (KeyCode.W)) {
			moveDirection += _cam.transform.forward;
		}
		if (Input.GetKey (KeyCode.S)) {
			moveDirection -= _cam.transform.forward;
		}
		if (Input.GetKey (KeyCode.D)) {
			moveDirection += _cam.transform.right;
		}
		if (Input.GetKey (KeyCode.A)) {
			moveDirection -= _cam.transform.right;
		}
		if (moveDirection.magnitude > 0.0f){
			moveDirection.Normalize();
			Move(moveDirection, MOVE_SPEED);
		}
	}
	private void UpdateLookRotation(){
		
		// Determine rotation based on mouse movement
		_rotX = (_rotX + (Input.GetAxis("Mouse X") * SENSITIVITY_X)) % 360;
		_rotY = Mathf.Clamp(_rotY + (Input.GetAxis("Mouse Y") * SENSITIVITY_Y), -MAX_VERTICAL_LOOK_DEGREES, MAX_VERTICAL_LOOK_DEGREES);
		
		// Update camera orientation
		Quaternion xQuaternion = Quaternion.AngleAxis(_rotX, Vector3.up);
		Quaternion yQuaternion = Quaternion.AngleAxis(_rotY, Vector3.left);
		_cam.transform.localRotation = _initialCameraRot * xQuaternion * yQuaternion;

		// player collider should never rotate with camera
		_capsuleCollider.rotation = _capsuleColliderRotation;
	}
	#endregion
}
