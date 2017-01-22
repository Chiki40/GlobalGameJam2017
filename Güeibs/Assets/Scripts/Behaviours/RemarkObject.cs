using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemarkObject : MonoBehaviour {
    Renderer m_renderer;
	// Use this for initialization
	void Start () {
        m_renderer = GetComponentInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float emission = Mathf.PingPong(Time.time, 1.0f);
        Color baseColor = Color.white; //Replace this with whatever you want for your base color at emission level '1'
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        m_renderer.material.SetColor("_EmissionColor", finalColor);
	}
}
