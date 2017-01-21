using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        DoDeath();
    }

    private void DoDeath()
    {
        Debug.Log("we are dead, maybe play some sound or some dead animation");
        LevelManager.GetInstance().RestartLevel();
    }
}
