using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public Animator otherAnimator;
    private Animator m_animatorComponent;

	// Use this for initialization
	void Start () 
    {
        m_animatorComponent = GetComponent<Animator>();
        if (!otherAnimator)
            otherAnimator = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<Animator>();
	}
	
	// Update is called once per frame
	void Update () 
    {
        for (int i = 0; i<otherAnimator.parameterCount;++i)
        {
            string name = otherAnimator.parameters[i].name;
            m_animatorComponent.SetFloat(name,otherAnimator.GetFloat(name));
        }
	}
}
