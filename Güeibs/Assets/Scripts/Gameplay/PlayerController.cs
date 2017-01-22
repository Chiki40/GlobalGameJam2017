using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DeathReason { Blackhole, Electrocution };

public class PlayerController : MonoBehaviour {
	
	private float m_BlackholeDyingTime = 2.5f;
	private float m_BlackholeDyingKillerRotationSpeed = 500.0f;

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.R)) {
			int rand = Random.Range(1, 4);
			UtilSound.instance.PlaySound("click" + rand.ToString());
			GameManager.GetInstance().RestartLevel();
			return;
		}
		bool up = false, down = false, left = false, right = false;
		GetInputDirections(out up, out down, out left, out right);
		GravityManager.GetInstance().SetGravityDirection(up, down, left, right);
	}

	private void GetInputDirections(out bool up, out bool down, out bool left, out bool right) {
		Vector2 mousePos = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);

        
        bool r2 = Input.GetJoystickNames().Length > 0 ? Input.GetAxis("R2") > -0.8f : false;
        bool l2 = Input.GetJoystickNames().Length > 0 ? Input.GetAxis("L2") > -0.8f : false;

        up = Input.GetKey(KeyCode.Joystick1Button5) || Input.GetKey(KeyCode.Keypad9) || Input.GetKey(KeyCode.E) || Input.GetKey(KeyCode.UpArrow) || (Input.GetMouseButton(0) && mousePos.y >= 0.6f);
        down = l2 || Input.GetKey(KeyCode.Keypad1) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.DownArrow) || (Input.GetMouseButton(0) && mousePos.y <= 0.4f);
        left = Input.GetKey(KeyCode.Joystick1Button4) || Input.GetKey(KeyCode.Keypad7) || Input.GetKey(KeyCode.Q) || Input.GetKey(KeyCode.LeftArrow) || (Input.GetMouseButton(0) && mousePos.x <= 0.4f);
        right = r2 || Input.GetKey(KeyCode.Keypad3) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow) || (Input.GetMouseButton(0) && mousePos.x >= 0.6f);
	}

	public void Die(DeathReason reason, GameObject killer) {
		Debug.Log("You have died by " + reason.ToString());
		this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
		StartCoroutine(DieCoroutine(reason, killer));
	}

	private IEnumerator DieCoroutine(DeathReason reason, GameObject killer) {
		this.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero; // Reset layer velocity
		this.gameObject.GetComponent<GravityComponent>().enabled = false; // Disable character controller
		this.gameObject.GetComponent<Animator>().SetFloat("x", 0.0f);
		this.gameObject.GetComponent<Animator>().SetFloat("y", 0.0f);
		switch (reason) {
			case DeathReason.Blackhole:
				yield return StartCoroutine(DieBlackhole(killer));
				break;
			case DeathReason.Electrocution:
				yield return StartCoroutine(DieElectrocution(killer));
				break;
			default:
				Debug.LogError("Error. Death reason behaviour not implemented in PlayerController.DieCoroutine");
				yield return null;
				break;
		}
		GameManager.GetInstance().RestartLevel();
	}

	private IEnumerator DieBlackhole(GameObject killer) {
		UtilSound.instance.PlaySound("blackhole");
		this.gameObject.transform.SetParent(killer.transform);
		Vector3 initPos = this.gameObject.transform.localPosition;
		Vector3 initScale = this.gameObject.transform.localScale;
		float timeAcum = 0.0f;
		while (timeAcum < m_BlackholeDyingTime) {
			timeAcum += Time.deltaTime;
			// Position
			Vector3 partialPos = Vector3.Slerp(initPos, Vector3.zero, timeAcum / m_BlackholeDyingTime);
			this.gameObject.transform.localPosition = partialPos;
			// Escale
			Vector3 partialScale = Vector3.Slerp(initScale, Vector3.zero, timeAcum / m_BlackholeDyingTime);
			this.gameObject.transform.localScale = partialScale;
			// Parent's rotation
			Vector3 parentPartialRotation = killer.transform.eulerAngles;
			parentPartialRotation.y += m_BlackholeDyingKillerRotationSpeed * Time.deltaTime;
			killer.transform.eulerAngles = parentPartialRotation;
			yield return null;
		}
		yield return null;
	}

	private IEnumerator DieElectrocution(GameObject killer) {
		UtilSound.instance.PlaySound("electrocution");
		UtilSound.instance.PlaySound("electrocution2");
        GetComponent<Animator>().SetTrigger("Electrocution");
		Transform rayitos = this.gameObject.transform.FindChild("rayitos");
		if (!rayitos) {
			Debug.LogError("Error. rayitos child not found on object " + this.gameObject.name);
			yield break;
		}
		rayitos.gameObject.GetComponent<Animator>().SetTrigger("Electrocution");
        yield return null;
        yield return new WaitForSeconds(GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).length);
	}

	public void OnCollisionStay(Collision col) {
		if (col.relativeVelocity.magnitude > 20.0f && !UtilSound.instance.IsPlayingFamilySound("crash")) {
			int rand = Random.Range(1, 4);
			UtilSound.instance.PlaySound("crash" + rand.ToString(), 0.5f);
		}
	}
}
