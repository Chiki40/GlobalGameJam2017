using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour {
    float TimeWave;
    float TotalScale;
    Color WaveColor;
    bool InsideOutside;
    float TimeAcumWave;

	// Use this for initialization
	void Start () 
    {
        TimeAcumWave = 0;
        destroySpeed = 1;
	}
	
	// Update is called once per frame
	void Update () 
    {
        TimeAcumWave += Time.deltaTime;
        float timeLerp = TimeAcumWave / TimeWave;
        if (InsideOutside)
        {
            transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * TotalScale, timeLerp);
        }
        else
        {
            transform.localScale = Vector3.Lerp(Vector3.one * TotalScale, Vector3.zero, timeLerp);
        }
        //alpha
        Color c = GetComponent<Renderer>().material.color;
        if (destroySpeed == 1)
        {
            if (InsideOutside)
            {
                c.a = Mathf.Lerp(1, 0, timeLerp);
            }
            else
            {
                c.a = Mathf.Lerp(0, 1, timeLerp);
            }
        }
        else
        {
            c.a = Mathf.Lerp(c.a, 0, destroySpeed);
        }
        GetComponent<Renderer>().material.color = c;
	}

    public void SetValues(float timeWave, float totalScale, Color waveColor, bool insideOutside)
    {
        TimeWave = timeWave;
        TotalScale = totalScale;
        WaveColor = waveColor;
        InsideOutside = insideOutside;
    }


    public float destroySpeed { get; set; }
}
