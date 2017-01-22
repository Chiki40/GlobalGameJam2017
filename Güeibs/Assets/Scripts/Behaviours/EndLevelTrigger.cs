using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour {

    PlayerController pc;
    Vector3 initialPlayerPOs;
    Vector3 distance;
    float acumTime;
    float totalTime = 1;
    private Coroutine routine;
    private float speed = 1;

    public void Start()
    {
        routine = null;
    }

    void OnTriggerEnter(Collider other)
    {
        pc = other.GetComponent<PlayerController>();
        other.enabled = false;
        pc.enabled = false;
        distance = transform.position - pc.transform.position;
        initialPlayerPOs = pc.transform.position;
        acumTime = 0;
        routine = null;
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
        Debug.Log("we finish the level, play some sound and some animation, particles... whatever");
		GameManager.GetInstance().NextLevel();
        return null;
    }
}
