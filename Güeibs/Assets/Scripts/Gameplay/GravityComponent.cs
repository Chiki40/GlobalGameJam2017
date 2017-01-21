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

	public void SetGravity(GravityManager.AttractionForces newGravity) {
		if (!this.enabled) {
			return;
		}
		m_gravity = newGravity.left + newGravity.right + newGravity.top + newGravity.bottom;
        Animator animator = GetComponent<Animator>();
        if (animator)
        {
            animator.SetFloat("x", Mathf.Clamp(m_gravity.normalized.x, -1, 1), 0.2f, Time.deltaTime);
            animator.SetFloat("y", Mathf.Clamp(m_gravity.normalized.z, -1, 1), 0.2f, Time.deltaTime);
            //animator.SetFloat("x",Mathf.Clamp((newGravity.left + newGravity.right).x,-1,1), 0.2f,Time.deltaTime);
            //animator.SetFloat("y", Mathf.Clamp((newGravity.top + newGravity.bottom).z,-1,1), 0.2f, Time.deltaTime);
        }
	}

	public void FixedUpdate() {
		m_rigidBody.AddForce(m_gravity, ForceMode.Acceleration);
	}
}
