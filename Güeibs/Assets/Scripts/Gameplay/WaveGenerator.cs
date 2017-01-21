using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveGenerator : MonoBehaviour
{

    public GameObject m_wave;
    public float TimeWave;
    public float TotalScale;
    public float TimeBetweenWaves;
    public Color WaveColor;
    public bool InsideToOutside;
    public float DestroySpeed = 4;

    private float timeAcumBetweenWaves;
    private static int count = 0;
    void Start()
    {
        timeAcumBetweenWaves = TimeBetweenWaves;// with this hack we have a initial wave
    }

    void Update()
    {
        timeAcumBetweenWaves += Time.deltaTime;

        if (timeAcumBetweenWaves >= TimeBetweenWaves)
        {
            GameObject go = Instantiate(m_wave);
            Color emptyColor = WaveColor;
            emptyColor.a = 0;
            go.GetComponent<Renderer>().material.color = emptyColor;
            go.transform.SetParent(this.transform);
            go.transform.localScale = Vector3.zero;
			go.transform.localPosition = Vector3.zero;
            go.name = count.ToString();
            ++count;
            timeAcumBetweenWaves = 0;
            go.GetComponent<Wave>().SetValues(TimeWave, TotalScale, WaveColor, InsideToOutside);
            Destroy(go, TimeWave);
        }
    }

    public void OnDisable()
    {
        foreach (Transform t in transform)
        {
            t.GetComponent<Wave>().destroySpeed = DestroySpeed;
        }
    }


}
