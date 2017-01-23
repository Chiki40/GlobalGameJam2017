using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour {

	private static GravityManager m_instance = null;

	private List<GravityComponent> m_gravityComponents = null;

	public GameObject magnetUp = null;
	public GameObject magnetDown = null;
	public GameObject magnetLeft = null;
	public GameObject magnetRight = null;
    private float m_verticalDistance;
    private float m_horizontalDistance;

    public struct AttractionForces
    {
        public Vector3 left;
        public Vector3 right;
        public Vector3 top;
        public Vector3 bottom;
    }

	public void Awake() {
		if (m_instance) {
			Destroy(this.gameObject);
		} else {
			m_instance = this;
			m_gravityComponents = new List<GravityComponent>();
			if(!magnetUp || !magnetDown || !magnetLeft || !magnetRight) {
				Debug.LogError("Error. GravityManager does not have a magnet assigned");
			}

            m_verticalDistance = (magnetUp.transform.position - magnetDown.transform.position).magnitude +4;
            m_horizontalDistance = (magnetRight.transform.position - magnetLeft.transform.position).magnitude + 4;
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
        AttractionForces gravityForce;

		// Wave visual effect for magnets
		WaveGenerator WaveGeneratorUp = magnetUp.transform.GetChild(0).gameObject.GetComponent<WaveGenerator>();
		WaveGenerator WaveGeneratorDown = magnetDown.transform.GetChild(0).gameObject.GetComponent<WaveGenerator>();
		WaveGenerator WaveGeneratorLeft = magnetLeft.transform.GetChild(0).gameObject.GetComponent<WaveGenerator>();
		WaveGenerator WaveGeneratorRight = magnetRight.transform.GetChild(0).gameObject.GetComponent<WaveGenerator>();
		WaveGeneratorUp.enabled = up;
		WaveGeneratorDown.enabled = down;
		WaveGeneratorLeft.enabled = left;
		WaveGeneratorRight.enabled = right;

		for (int i = 0; i < m_gravityComponents.Count; ++i) {
			gravityForce.left = Vector3.zero;
            gravityForce.right = Vector3.zero;
            gravityForce.top = Vector3.zero;
            gravityForce.bottom = Vector3.zero;
			if (up) {
                Vector3 dir = (magnetUp.transform.position - m_gravityComponents[i].transform.position);
				//dir = new Vector3(0.0f, 0.0f, dir.z);
                float distance = dir.magnitude;
                if (distance < m_verticalDistance)
                {
                    dir.Normalize();
                    Vector3 force = dir * (m_verticalDistance - distance);
                    gravityForce.top = force * GameManager.GetInstance().gravityModule;
                }
			}
			if (down) {
                Vector3 dir = (magnetDown.transform.position - m_gravityComponents[i].transform.position);
				//dir = new Vector3(0.0f, 0.0f, dir.z);
                float distance = dir.magnitude;
                if (distance < m_verticalDistance)
                {
                    dir.Normalize();
                    Vector3 force = dir * (m_verticalDistance - distance);
                    gravityForce.bottom = force * GameManager.GetInstance().gravityModule;
                }
			}
			if (left) {
                Vector3 dir = (magnetLeft.transform.position - m_gravityComponents[i].transform.position);
				//dir = new Vector3(dir.x, 0.0f, 0.0f);
                float distance = dir.magnitude;
                if (distance < m_horizontalDistance)
                {
                    dir.Normalize();
                    Vector3 force = dir * (m_horizontalDistance - distance);
                    gravityForce.left = force * GameManager.GetInstance().gravityModule;
                }
			}
			if (right) {
                Vector3 dir = (magnetRight.transform.position - m_gravityComponents[i].transform.position);
				//dir = new Vector3(dir.x, 0.0f, 0.0f);
                float distance = dir.magnitude;
                if (distance < m_horizontalDistance)
                {
                    dir.Normalize();
                    Vector3 force = dir * (m_horizontalDistance - distance);
                    gravityForce.right = force * GameManager.GetInstance().gravityModule;
                }
			}					
			m_gravityComponents[i].SetGravity(gravityForce);
		}
	}
}
