using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    Collider m_collider;
    Animator m_animator;

	// Use this for initialization
	void Start () {
        m_collider = GetComponentInChildren<Collider>();
        m_animator = GetComponentInChildren<Animator>();
	}

    public void Open()
    {
		UtilSound.instance.PlaySound("door");
        m_animator.SetTrigger("Open");
        m_collider.isTrigger = true;
        m_collider.gameObject.AddComponent<EndLevelTrigger>();
        BoxCollider b = (BoxCollider)m_collider;
        Vector3 size = b.size;
        size.z = 2;
        b.size = size;
        if(b.center.z < 0)
        {
            b.center = new Vector3(b.center.x, b.center.y, 1);
        }
        else
        {
            b.center = new Vector3(b.center.x, b.center.y, -1); 
        }
    }
}
