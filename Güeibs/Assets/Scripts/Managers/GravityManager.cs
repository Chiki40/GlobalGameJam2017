using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

	private static GravityManager m_instance = null;

	private List<GravityComponent> m_gravityComponents = null;

	public float gravityModule = 9.8f;
	public GameObject magnetUp = null;
	public GameObject magnetDown = null;
	public GameObject magnetLeft = null;
	public GameObject magnetRight = null;

	public void Awake() {
		if (m_instance) {
			Destroy(this.gameObject);
		} else {
			m_instance = this;
			m_gravityComponents = new List<GravityComponent>();
			if(!magnetUp || !magnetDown || !magnetLeft || !magnetRight) {
				Debug.LogError("Error. GravityManager does not have a corner assigned");
			}
		}
	}

	public static GravityManager GetInstance() {
		return m_instance;
	}

	public void RegisterGravityComponent(GravityComponent gravComp) {
		if (m_gravityComponents.Contains(gravComp)) {
			Debug.LogError("[GravityManager] Error. GravityComponent of " + gravComp.name + " is already registered");
			return;
		}
		m_gravityComponents.Add(gravComp);
	}

	public void SetGravityDirection(bool up, bool down, bool left, bool right) {
		Vector3 gravityForce;
		for (int i = 0; i < m_gravityComponents.Count; ++i) {
			gravityForce = Vector3.zero;
			if (up) {
				gravityForce += (magnetUp.transform.position - m_gravityComponents[i].transform.position).normalized * gravityModule;
			}
			if (down) {
				gravityForce += (magnetDown.transform.position - m_gravityComponents[i].transform.position).normalized * gravityModule;
			}
			if (left) {
				gravityForce += (magnetLeft.transform.position - m_gravityComponents[i].transform.position).normalized * gravityModule;
			}
			if (right) {
				gravityForce += (magnetRight.transform.position - m_gravityComponents[i].transform.position).normalized * gravityModule;
			}					
			m_gravityComponents[i].SetGravity(gravityForce);
		}
	}

}
