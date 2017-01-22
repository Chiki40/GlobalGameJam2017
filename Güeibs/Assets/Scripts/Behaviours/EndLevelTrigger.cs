using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour {

    PlayerController pc;
    Vector3 initialPlayerPOs;
    float acumTime;
    float totalTime = 1;
	public float timeBeforeChangingLevel = 3.0f;
	private bool completed = false;

    void OnTriggerEnter(Collider other)
    {
        pc = other.GetComponent<PlayerController>();
        other.enabled = false;
        pc.enabled = false;
        initialPlayerPOs = pc.transform.position;
        acumTime = 0;
		pc.enabled = false;
		pc.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

	void Start() {
		completed = false;
	}

    void Update()
    {
        if (!pc) return;
        acumTime += Time.deltaTime;
        if (acumTime < totalTime)
        {
            pc.transform.position = Vector3.Lerp(initialPlayerPOs, transform.position, acumTime / totalTime);
        }
		else if (!completed)
        {
			completed = true;
			StartCoroutine(DoEndLevel());
        } 
    }

    private IEnumerator DoEndLevel()
    {
        Debug.Log("Level completed");
		UtilSound.instance.PlaySound("victory1", 1.25f);
		UtilSound.instance.PlaySound("victory2");
		yield return new WaitForSeconds(timeBeforeChangingLevel);
		GameManager.GetInstance().NextLevel();
    }
}
