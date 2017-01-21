using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		bool up = false, down = false, left = false, right = false;
		GetInputDirections(out up, out down, out left, out right);
		GravityManager.GetInstance().SetGravityDirection(up, down, left, right);
	}

	private void GetInputDirections(out bool up, out bool down, out bool left, out bool right) {
		up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
		down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
		left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow);
		right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow);
	}
}
