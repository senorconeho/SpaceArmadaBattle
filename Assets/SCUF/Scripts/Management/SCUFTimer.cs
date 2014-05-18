using UnityEngine;
using System.Collections;

/// <summary>
/// My own deltaTime class, so we can actually pause the game
/// </summary>

public class SCUFTimer : MonoBehaviour {

	float _timeAtCurrentFrame;
	float _timeAtLastFrame;
	float _deltaTime;
	bool _bnGameIsPaused;

	public float myDeltaTime { get {  return ((_bnGameIsPaused == true) ? 0.0f : _deltaTime); } }

	// Update is called once per frame
	void Update () {

		_timeAtCurrentFrame = Time.realtimeSinceStartup;
		_deltaTime = _timeAtCurrentFrame - _timeAtLastFrame;
		_timeAtLastFrame = _timeAtCurrentFrame;
	}
}
