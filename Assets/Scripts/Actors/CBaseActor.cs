using UnityEngine;
using System;
using System.Collections;

/// <summary>
/// Basic actor Class
/// </summary>
public class CBaseActor : MonoBehaviour {
	
	/* ==========================================================================================================
	 * CLASS VARIABLES
	 * ==========================================================================================================
	 */
	// PUBLIC
	public float		fMaxHealth{ get{return m_fMaxHealth;} }
	public float		fHealth{ get{return m_fHealth;} }
	public float		fNormalizedHealth	{ get{ return m_fHealth/m_fMaxHealth; }}
	public float		fStartHealth	= 100.0f;	//< initial amount of health
 
	// PROTECTED
	protected float			m_fHealth;
	protected float			m_fMaxHealth;
	protected Transform	trMesh				= null;
	protected bool			bnIsDead			= false;

	/* ==========================================================================================================
	 * UNITY METHODS
	 * ==========================================================================================================
	 */

	public virtual void Awake() {

		m_fHealth =	m_fMaxHealth = fStartHealth;

	}

	// Use this for initialization
	public virtual void Start () {
		
		trMesh = GetMeshObject();

		if(!trMesh)
			Debug.LogError("no mesh for " + this.transform);
	}
	
	// Update is called once per frame
	public virtual void Update () {
	
	}
	
	/* ==========================================================================================================
	 * CLASS METHODS
	 * ==========================================================================================================
	 */
	/// <summary>
	/// Take damage from a hit, subtracting the hit damage from the current health. If health is depleted, the
	/// Actor dies
	/// </summary>
	/// <param name="fAmount"> Amount of the damage taken </param>
	/// <param name="v3FromDirection"> From where the damage came </param>
	/// <param name="actorShooter"> Script from the shooter </param>
	public virtual void OnDamage(float fAmount, Vector3 v3FromDirection, CBaseActor actorShooter) {

		m_fHealth -= fAmount;

		if(m_fHealth <= 0 && !bnIsDead) {
			m_fHealth = 0;
			bnIsDead = true;

			Die();
		}
	}	
	
	/// <summary>
	/// Replenishes the actor health by a certain amount
	/// </summary>
	/// <param name="fAmount"> Amount of health being given to the actor </param>
	public virtual void OnHeal(float fAmount) {
		
		m_fHealth = Mathf.Min(m_fHealth + fAmount, m_fMaxHealth);
	}
	
	/// <summary>
	/// Kill the actor
	/// </summary>
	protected virtual void Die() {
		
		Destroy(gameObject);
	}

	/// <summary>
	/// Delay the Die() for n seconds
	/// </summary>
	public IEnumerator WaitAndThenDie(float fWaitTime) {

		yield return new WaitForSeconds(fWaitTime);
		Die();
	}

	public virtual void StartFire() {

	}

	public virtual void StopFire() {

	}

	public virtual bool IsFiring() {

		return false;
		
	}

	/// <summary>
	/// Get the mesh object in the hierarchy, as this:
	/// Object -> collider
	/// | - Mesh 
	///	  | - Imported FBX -> animation
	/// </summary>
	public Transform GetMeshObject() {

		// First, find the "Mesh" in the hierarchy
		Transform meshHierarchy = transform.Find("Mesh");

		// Now, get the children of the Mesh, This will be the real model
		if(meshHierarchy) {

			foreach(Transform child in meshHierarchy) {
				
				return child;
			}
		}

		return null;
	}

	/// <summary>
	/// Is this actor dead?
	/// </summary>
	public bool IsDead() {

		return bnIsDead;
	}

	/* ==========================================================================================================
	 * DEBUG METHODS
	 * ==========================================================================================================
	 */
	///
	void OnGUI() {

		// DEBUG
		string stText = "H: " + String.Format("{0:0.0}", fHealth);

		Vector3 v3TextPosition = Camera.main.WorldToScreenPoint(transform.position);
		GUI.Label(new Rect(v3TextPosition.x, Screen.height - v3TextPosition.y, 100, 100), stText);
	}
}
