﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelTrigger : MonoBehaviour {

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("we finish the level, play some sound and some animation, particles... whatever");
        LevelManager.GetInstance().NextLevel();
    }
}
