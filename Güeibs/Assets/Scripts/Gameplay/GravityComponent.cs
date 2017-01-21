using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityComponent : MonoBehaviour {

	private Rigidbody m_rigidBody = null;
	private Vector3 m_gravity = Vector3.zero;

	public void Start() {
		m_rigidBody = gameObject.GetComponent<Rigidbody>();
		if (!m_rigidBody) {
			Debug.LogError("Error. Object " + gameObject.name + " does not have a RigidBody");
			return;
		}
		GravityManager.GetInstance().RegisterGravityComponent(this);
		m_gravity = Vector3.zero;
	}

	public void SetGravity(Vector3 newGravity) {
		m_gravity = newGravity;
	}

	public void Update() {
		m_rigidBody.AddForce(m_gravity, ForceMode.Acceleration);
	}
}
