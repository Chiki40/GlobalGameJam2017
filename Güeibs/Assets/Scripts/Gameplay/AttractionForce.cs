using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttractionForce : MonoBehaviour {

	private GameObject m_player = null;

	public float attractionForce = 20;
	public float distanceToAttract = 10.0f;

	void Start() {
		m_player = GameObject.FindGameObjectWithTag("Player");
	}

	// Update is called once per frame
	void Update () {
		if (m_player) {
			float distance = Vector3.Distance(this.gameObject.transform.position, m_player.transform.position);
			if (distance <= distanceToAttract) {
				Rigidbody rb = m_player.GetComponent<Rigidbody>();
				if (rb) {
					Vector3 forceDirection = this.gameObject.transform.position - m_player.transform.position;
					rb.AddForce(forceDirection.normalized * attractionForce, ForceMode.Acceleration);
				}
			}
		}
	}
}
