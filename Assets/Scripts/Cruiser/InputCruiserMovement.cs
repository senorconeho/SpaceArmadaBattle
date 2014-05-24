using UnityEngine;
using System.Collections;

/// <summary>
/// Class description here
/// </summary>
public class InputCruiserMovement : MonoBehaviour {
	
	/* -----------------------------------------------------------------------------------------------------------
	 * UNITY'S STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */

	public Texture2D	txCursorSelectPosition;		//< Cursor to select the ship's position
	Texture2D txCurrentCursor = null;
	public Vector3							vGUICursorPosition;
	public string	stMenuMessage = null;

	int nMenuLevel = 0;
	InputGenericMouseCursorPosition	mouseScript;

	/// <summary>
	/// Will execute when loaded
	/// </summary>
	void Awake() {

	}

	/// <summary>
	/// Start will execute when an object with it is instantiated
	/// </summary>
	void Start () {
	
		mouseScript = GetComponent<InputGenericMouseCursorPosition>();
	}
	
	/// <summary>
	/// Update is called once per frame
	/// </summary>
	void Update () {
	
		// Input
		if(Input.GetKey(KeyCode.M)) {

			// Movement Menu
			nMenuLevel = 1;
		}

		if(Input.GetMouseButton(0) && nMenuLevel == 1) {
			// Confirming the move position
			nMenuLevel++;
		}

		if(Input.GetKey(KeyCode.Escape)) {

			// Return to a previous state
			nMenuLevel -=1;
			if(nMenuLevel < 0)
				nMenuLevel = 0;
		}

		ProcessMovementMenuLevel(nMenuLevel);
	}

	/* -----------------------------------------------------------------------------------------------------------
	 * MY STUFF HERE
	 * -----------------------------------------------------------------------------------------------------------
	 */
	void ProcessMovementMenuLevel(int nCurrentMenuLevel) {

		if(nCurrentMenuLevel == 0) {
			
			// DEBUG
			txCurrentCursor = null;
			stMenuMessage = null;
		}
		// First menu level: the player pressed 'm' to start movement;
		if(nCurrentMenuLevel == 1) {
			
			// DEBUG
			txCurrentCursor = txCursorSelectPosition;
			stMenuMessage = "[MOVE] Select target position";
		}
		// The player confirmed the target position. Now he must select the ship orientation
		if(nCurrentMenuLevel == 2) {
			
			// DEBUG
			txCurrentCursor = txCursorSelectPosition;
			stMenuMessage = "[MOVE] Select ship orientation";
		}
	}

	/// <summary>
	///
	/// </summary>
	void OnGUI() {

		if(txCurrentCursor != null) {

			vGUICursorPosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f);
			GUI.DrawTexture(new Rect(vGUICursorPosition.x - 16, Screen.height - vGUICursorPosition.y - 16, 32, 32), txCurrentCursor);
		}
	
		// Shows a text message on the screen with the current movement status
		if(stMenuMessage != null) {

			GUI.Label(new Rect(100, Screen.height - 200, 200, 100), stMenuMessage);
		}
	}
}
