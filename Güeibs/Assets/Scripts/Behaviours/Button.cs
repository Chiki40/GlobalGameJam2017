using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent[] Actions;

    void OnTriggerEnter(Collider other)
    {
        DoButton();
    }

    private void DoButton()
    {
        for (int i = 0; i < Actions.Length; ++i)
        {
            Actions[i].Invoke();
        }
    }
}
