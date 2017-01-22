using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour {

    PlayerController pc;
    Vector3 initialPlayerPOs;
    float acumTime;
    float totalTime = 1;

    void OnTriggerEnter(Collider other)
    {
        pc = other.GetComponent<PlayerController>();
        other.enabled = false;
        pc.enabled = false;
        initialPlayerPOs = pc.transform.position;
        acumTime = 0;
    }

    void Update()
    {
        if (!pc) return;
        acumTime += Time.deltaTime;
        if (acumTime < totalTime)
        {
            pc.transform.position = Vector3.Lerp(initialPlayerPOs, transform.position, acumTime / totalTime);
        }
        else
        {
            DoEndLevel();
        } 
    }

    private IEnumerator DoEndLevel()
    {
        Debug.Log("Level completed");
		GameManager.GetInstance().NextLevel();
        return null;
    }
}
