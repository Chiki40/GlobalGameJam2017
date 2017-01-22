using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemarkObject : MonoBehaviour {
    public bool pingpong = true;
    public Color baseColor = Color.white;
    Renderer m_renderer;
	// Use this for initialization
	void Start () {
        m_renderer = GetComponentInChildren<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float emission = 1;
        if (pingpong)
             emission = Mathf.PingPong(Time.time, 1.0f);
        Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);
        m_renderer.material.SetColor("_EmissionColor", finalColor);
	}
}
