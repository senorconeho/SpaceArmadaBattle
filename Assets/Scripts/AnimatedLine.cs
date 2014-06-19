using UnityEngine;
using System.Collections;

/// <summary>
/// Animate the material for the line renderer
/// </summary>
public class AnimatedLine : MonoBehaviour {
	
	public Material	lrMaterial;
	public int nRepeatsPerMetre = 1;
	public float fLineWidth = 0.2f;
	public float fScrollSpeed = 1.4f;
	float fMagnitude;
	LineRenderer	lr;
	Vector3[] vPositions = new Vector3[2];
	/* -----------------------------------------------------------------------------------------------------------
	 * UNITY'S STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */
	/// <summary>
	/// Will execute when loaded
	/// </summary>
	void Awake() {

	}

	/// <summary>
	/// Start will execute when an object with it is instantiated
	/// </summary>
	void Start () {
	
		lr = gameObject.AddComponent<LineRenderer>();
		lr.SetVertexCount(2);
		lr.SetWidth(fLineWidth, fLineWidth);
		lr.material = lrMaterial;
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
	
		// animate
		float offset = lrMaterial.mainTextureOffset.x + Time.deltaTime * -fScrollSpeed;
		if(Mathf.Abs(offset) > 1.0f)
			offset = 0.0f;

		lrMaterial.mainTextureOffset = new Vector2(offset, 0);
	}

	/* -----------------------------------------------------------------------------------------------------------
	 * MY STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */
	public void SetLinePosition(Vector3 vNewStartPosition, Vector3 vNewEndPosition) {

		vPositions[0] = vNewStartPosition;
		vPositions[1] = vNewEndPosition;

		RecalculateRepeats();

		// Draw the line
		lr.SetPosition(0, vPositions[0]);
		lr.SetPosition(1, vPositions[1]);
	}

	public void SetPosition(int nVertex, Vector3 vPosition) {

		vPositions[nVertex] = vPosition;

		RecalculateRepeats();
		lr.SetPosition(nVertex, vPositions[nVertex]);
	}

	void RecalculateRepeats() {

		// Update the magnitude of the line
		fMagnitude = (vPositions[1] - vPositions[0]).magnitude;

		// Update the tiling in the material
		float fRepeats = fMagnitude / nRepeatsPerMetre;
		if(fRepeats < 1.0f)
			fRepeats = 1.0f;

		lrMaterial.mainTextureScale = new Vector2(fRepeats, 1);
	}

}
