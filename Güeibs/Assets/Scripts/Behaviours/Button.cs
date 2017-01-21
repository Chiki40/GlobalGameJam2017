using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public MonoBehaviour[] BehavioursToActivate;
    public MonoBehaviour[] BehavioursToDisable;

    void OnTriggerEnter(Collider other)
    {
        DoButton();
    }

    private void DoButton()
    {
        for (int i = 0; i < BehavioursToActivate.Length; ++i)
        {
            BehavioursToActivate[i].enabled = true;
        }
        for (int i = 0; i < BehavioursToDisable.Length; ++i)
        {
            BehavioursToDisable[i].enabled = false;
        }
    }
}
