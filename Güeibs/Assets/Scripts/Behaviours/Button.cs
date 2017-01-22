using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent Actions;

    void OnTriggerEnter(Collider other)
    {
		if (other.gameObject.tag == "Player") {
			DoButton();
		}
    }

    public void OnCollisionEnter(Collision collision)
    {
		if (collision.collider.gameObject.tag == "Player") {
			DoButton();
		}
    }

    private void DoButton()
    {
		if (gameObject.name.Contains("Key")) {
			UtilSound.instance.PlaySound("key");
		} else if (gameObject.name.Contains("Button")) {
			UtilSound.instance.PlaySound("switch");
		}

		Actions.Invoke();
    }
}
