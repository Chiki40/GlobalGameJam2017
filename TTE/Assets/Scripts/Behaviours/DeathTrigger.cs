using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

	private GameObject m_player = null;

	public void Start() {
		m_player = GameObject.FindGameObjectWithTag("Player");
		if (!m_player) {
			Debug.LogError("Error. No object with Player tag has been found by DeathTrigger script");
		}
	}

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject == m_player) {
			DoDeath();
		}
    }

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject == m_player) {
			DoDeath();
		}
	}

    private void DoDeath()
    {
		m_player.GetComponent<PlayerController>().Die(this.gameObject.tag == "BlackHole" ? DeathReason.Blackhole : DeathReason.Electrocution, this.gameObject);
    }
}
