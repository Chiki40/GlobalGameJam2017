using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour {
    Collider m_collider;
    Animator m_animator;

	// Use this for initialization
	void Start () {
        m_collider = GetComponentInChildren<Collider>();
        m_animator = GetComponentInChildren<Animator>();	}

    public void Open()
    {
        m_animator.SetTrigger("Open");
        m_collider.isTrigger = true;
        m_collider.gameObject.AddComponent<EndLevelTrigger>().enabled = true;
        m_collider.gameObject.AddComponent<RemarkObject>();
        BoxCollider b = (BoxCollider)m_collider;
        b.size = b.size + Vector3.one * 3;
    }
}
