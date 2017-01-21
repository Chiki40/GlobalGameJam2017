using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Player") {
			DoDeath();
		}
    }

	void OnCollisionEnter(Collision col)
	{
		if (col.collider.gameObject.tag == "Player") {
			DoDeath();
		}
	}

    private void DoDeath()
    {
        Debug.Log("we are dead, maybe play some sound or some dead animation");
		GameManager.GetInstance().RestartLevel();
    }
}
