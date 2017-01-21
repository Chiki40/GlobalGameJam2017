using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour
{

    public Key[] m_keys;
    private int totalKeys;

    public UnityEvent SomeBehaviour;

    void Start()
    {
        totalKeys = 0;
    }

    public void AddKey(int id)
    {
        for (int i = 0; i < m_keys.Length; ++i)
        {
            if (m_keys[i].ID == id)
            {
                totalKeys++;
            }
        }
        if (CanOpen())
        {
            OpenDoor();
        }
    }

    public bool CanOpen()
    {
        return totalKeys == m_keys.Length;
    }

    private void OpenDoor()
    {
        if (SomeBehaviour != null)
        {
            SomeBehaviour.Invoke();
        }
    }
}
