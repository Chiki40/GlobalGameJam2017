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
		Vector2 mousePos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

		up = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) || (Input.GetMouseButton(0) && mousePos.y >= 0.6f);
		down = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || (Input.GetMouseButton(0) && mousePos.y <= 0.4f);
		left = Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow) || (Input.GetMouseButton(0) && mousePos.x <= 0.4f);
		right = Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || (Input.GetMouseButton(0) && mousePos.x >= 0.6f);
	}
}
