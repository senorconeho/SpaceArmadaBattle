using UnityEngine;
using System.Collections;

public class ShieldBehavior : CBaseActor {

	public float alpha;
	public float visibleTime;

	private float alphaCorrente;
	private float currentTime;

	// Use this for initialization
	void Start () {

		alphaCorrente = alpha;
		alpha = 0;
		SetAlpha(alpha);
		
	}
	
	// Update is called once per frame
	void Update () {


	
	}

	public override void OnDamage(float fAmount, Vector3 v3FromDirection, CBaseActor actorShooter) {
		StartCoroutine(ShowShield(visibleTime));
		base.OnDamage(fAmount,v3FromDirection,actorShooter);
	}

	private void SetAlpha(float newAlpha){
		Color corMaterial = transform.renderer.material.color;
		corMaterial.a = newAlpha;
		renderer.material.color = corMaterial;
	}

	IEnumerator ShowShield(float time){
		SetAlpha (alphaCorrente);
		yield return new WaitForSeconds(time);
		SetAlpha(0);

	}
	
}
