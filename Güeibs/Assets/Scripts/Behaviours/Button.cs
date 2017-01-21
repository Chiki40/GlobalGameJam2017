using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent Actions;

    void OnTriggerEnter(Collider other)
    {
        DoButton();
    }

    public void OnCollisionEnter(Collision collision)
    {
        DoButton();
    }

    private void DoButton()
    {
        Actions.Invoke();
    }
}
