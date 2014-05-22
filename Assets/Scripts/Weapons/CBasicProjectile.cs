using UnityEngine;
using System.Collections;

/// <summary>
/// Behaviour of a very simple projectile
/// </summary>
public class CBasicProjectile : MonoBehaviour {

	/* ==========================================================================================================
	 * CLASS VARIABLES
	 * ==========================================================================================================
	 */
	// PUBLIC
	public float			fSpeed						= 4.2f;		//< Travel speed
	public float			fLifeTime					= 2.0f;		//< How long it lasts
	public float			fDamageDone				= 0.25f;	//< Damage done when hit something
	public bool				bnEnableCollision	=	false;	//< Should we do a collision check for this projectile?
	public bool				bnRecycleGameObject = false;	//< Should we use the object cache
	public Transform	trShooter;									//< Who shot this?
	[HideInInspector]
	public float			fStartSpeed = 0.0f;
	public float			fDistance					= 100;		//< Distance to the target

	// PRIVATE
	private float			fSpawnTime = 0.0f;	//< internal TTL timer
	private Transform	tr;									//< pointer to this object transform

	/* ==========================================================================================================
	 * UNITY METHODS
	 * ==========================================================================================================
	 */

	/// <summary>
	/// Use this for initialization
	/// </summary>
	void Start () {
	
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
	
		DoMovement();

		if(Time.time > fSpawnTime + fLifeTime || fDistance < 0) {

			Die();
		}
	}

	/// <summary>
	/// When this object is enabled
	/// </summary>
	void OnEnable() {

		tr = this.transform;
		fSpawnTime = Time.time;
	}

	/* ==========================================================================================================
	 * CLASS METHODS
	 * ==========================================================================================================
	 */
	/// <summary>
	/// Do the movement of this proctile
	/// </summary>
	protected virtual void DoMovement() {

		// Travels forward
		tr.position += tr.up * (fSpeed + fStartSpeed) * Time.deltaTime;
		fDistance -= fSpeed * Time.deltaTime;

	}

	/// <summary>
	/// What to do when this object "dies"
	/// </summary>
	protected void Die() {

		if(bnRecycleGameObject) {

			//	Using the spawner
			Spawner.Destroy(gameObject);
		}
		else {

			if(gameObject) {

				Destroy(gameObject);
			}
		}
	}

	/// <summary>
	/// Check if we hit something. If we are using raycast, this method will not be called. If we are relying on
	/// collision between this projectile and the world, this method will be called when a collision happens.
	/// </summary>
	public virtual void Hit(Transform trEntityHit, Vector3 v3Position) {

		if(!trShooter) 
			return;

		// Ignore entities shooting themselves or friendlies
		if(trShooter.gameObject.layer == trEntityHit.gameObject.layer)
			return;

		CBaseActor actorScript = trEntityHit.GetComponent<CBaseActor>();

		if(actorScript != null) {
			
			actorScript.OnDamage( fDamageDone, transform.forward, null ); // FIXME: passing null instead of CBaseActor here...
		}

		Die();
	}

	/// <summary>
	/// Check if this projectile collided with something 
	/// </summary>
	void OnTriggerEnter2D(Collider2D col) {

		Hit(col.gameObject.transform, this.transform.position);
	}
}
