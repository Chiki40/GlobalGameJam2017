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

    private float timeAcumBetweenWaves;
    private List<GameObject> listWaves;
    private List<float> listTimeAcumWave;
    private static int count = 0;
    void Start()
    {
        timeAcumBetweenWaves = TimeBetweenWaves;// with this hack we have a initial wave
        listWaves = new List<GameObject>();
        listTimeAcumWave = new List<float>();
    }

    void Update()
    {
        timeAcumBetweenWaves += Time.deltaTime;

        if (timeAcumBetweenWaves >= TimeBetweenWaves)
        {
            GameObject go = Instantiate(m_wave);
            go.transform.position = this.transform.position;
            go.transform.localScale = Vector3.zero;
            Color emptyColor = WaveColor;
            emptyColor.a = 0;
            go.GetComponent<Renderer>().material.color = emptyColor;
            go.transform.SetParent(this.transform);
            listWaves.Add(go);
            go.name = count.ToString();
            ++count;
            listTimeAcumWave.Add(0);
            timeAcumBetweenWaves = 0;
        }
        List<GameObject> listToDestroy = new List<GameObject>();
        for (int i = 0; i < listWaves.Count; ++i)
        {
            listTimeAcumWave[i] += Time.deltaTime;
            float timeLerp = listTimeAcumWave[i] / TimeWave;
            GameObject go = listWaves[i];
            //Destroy
            if (listTimeAcumWave[i] >= TimeWave)
            {
                listToDestroy.Add(go);
            }
            else
            {
                if(go == null)
                {
                    listToDestroy.Add(go);
                    continue;
                }
                //scale
                if (InsideToOutside)
                {
                    go.transform.localScale = Vector3.Lerp(Vector3.zero, Vector3.one * TotalScale, timeLerp);
                }
                else
                {
                    go.transform.localScale = Vector3.Lerp(Vector3.one * TotalScale, Vector3.zero, timeLerp);
                }
                //alpha
                Color c = go.GetComponent<Renderer>().material.color;
                if (InsideToOutside)
                {
                    c.a = Mathf.Lerp(1, 0, timeLerp);
                }
                else
                {
                    c.a = Mathf.Lerp(0, 1, timeLerp);
                }
                go.GetComponent<Renderer>().material.color = c;
            }
        }

        for (int i = 0; i < listToDestroy.Count; ++i)
        {
            int index = listWaves.IndexOf(listToDestroy[i]);
            listTimeAcumWave.RemoveAt(index);
            GameObject go = listWaves[i];
            Destroy(go);
            listWaves.RemoveAt(index);
        }
    }
}
