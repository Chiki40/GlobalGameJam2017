using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

	private static GravityManager m_instance = null;

	private Vector3 m_gravityForce = Vector3.zero;
	private List<GravityComponent> m_gravityComponents = null;

	public float gravityModule = 9.8f;

	public void Awake() {
		if (m_instance) {
			Destroy(this.gameObject);
		} else {
			m_instance = this;
			DontDestroyOnLoad(m_instance);
			m_gravityComponents = new List<GravityComponent>();
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
		m_gravityForce = Vector3.zero;
		if (up) {
			m_gravityForce.z += gravityModule;
		}
		if (down) {
			m_gravityForce.z -= gravityModule;	
		}
		if (left) {
			m_gravityForce.x -= gravityModule;
		}
		if (right) {
			m_gravityForce.x += gravityModule;
		}

		for (int i = 0; i < m_gravityComponents.Count; ++i) {
			m_gravityComponents[i].SetGravity(m_gravityForce);
		}
	}

}
