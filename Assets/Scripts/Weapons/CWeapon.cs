using UnityEngine;
using System.Collections;

/// <summary>
/// Simple weapon fire, based on the Angry Bots weapon code
/// How does this work?
/// We need a place to spawn the projectiles - trSpawnPoint
/// The projectile itself is a prefab - bulletPrefab
/// </summary>
public class CWeapon : MonoBehaviour {

	/* ==========================================================================================================
	 * CLASS VARIABLES
	 * ==========================================================================================================
	 */
	// PUBLIC
	public GameObject		bulletPrefab			=	null;	//< Bullet to be instantiated when the weapon fires
	public GameObject		particlePrefab		= null;	//< Particle shown when the weapon shoot
	public Transform		trSpawnPoint			= null;	//< Where the bullet will be instantiated
	public float				fShotsPerSecond				= 10;		//< shots per second
	public float				fDamagePerShot	= 20;		//< Damage inflicted per second. FIXME: this should be in the projectile class		
	public Transform		trShooter					= null;	//< Who is shooting with this weapon
	public CBaseActor		actorShooterScript = null;

	// PRIVATE
	protected float				fSpawnTime		= 0.0f;	//< internal TTL timer
	protected float				fLastFireTime = -1;		//< Time when the last bullet was fired
	protected bool				bnIsFiring		= false;//< Are we firing right now?

	/* ==========================================================================================================
	 * UNITY METHODS
	 * ==========================================================================================================
	 */
	public virtual void Awake() {

		if( trSpawnPoint == null) {

			trSpawnPoint = transform;
		}

		if(trShooter) {

			actorShooterScript = trShooter.gameObject.GetComponent<CBaseActor>();
		}
	}

	/// <summary>
	/// Shoots the projectile
	/// </summary>
	public void Shoot(float fStartSpeed) {

		if(bnIsFiring)
			return;

		bnIsFiring = true;

		// Spawn a bullet
		// Using the spawner
		//GameObject go = Spawner.Spawn(bulletPrefab, trSpawnPoint.position, trSpawnPoint.rotation) as GameObject;
		GameObject go = Instantiate(bulletPrefab, trSpawnPoint.position, trSpawnPoint.rotation) as GameObject;
		CBasicProjectile bulletScript = go.GetComponent<CBasicProjectile>();

		// Sets the start velocity
		if(trShooter != null) {

			bulletScript.fStartSpeed = fStartSpeed;
		}

		fLastFireTime = Time.time;

		bulletScript.fDistance = 1000;
		bulletScript.trShooter = trShooter;
	}

	public void Shoot() {

		Shoot(0.0f);
	}
	
	// Update is called once per frame
	public virtual void Update () {

		if(bnIsFiring) {

			if(Time.time > fLastFireTime + ( 1/fShotsPerSecond)) {

				bnIsFiring = false;
			}
		}
	}

	/// <summary>
	///
	/// </summary>
	public virtual void FixedUpdate() {

	}
	/* ==========================================================================================================
	 * CLASS METHODS
	 * ==========================================================================================================
	 */
	/// <summary>
	/// Firing weapon
	/// </summary>
	public virtual void StartFire() {

		bnIsFiring = true;

		if(audio)
			audio.Play();

		// TODO: add audio source
	}

	/// <summary>
	/// Resume firing
	/// </summary>
	public virtual void StopFire() {

		bnIsFiring = false;
		
		if(audio)
			audio.Stop();
	}

	/// <summary>
	/// Is this weapon firing right now?
	/// </summary>
	/// <returns> True if we are firing, false otherwise<returns>
	public virtual bool IsFiring() {

		return bnIsFiring;
	}

	void OnEnable() {

		//bnIsFiring = false;
	}

	void OnDisable() {

		//bnIsFiring = false;
	}
}
