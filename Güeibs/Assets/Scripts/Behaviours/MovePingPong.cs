using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePingPong : MonoBehaviour {

    public Transform P1;
    public Transform P2;
    public float TotalTime;

    private Vector3 actualP1;
    private Vector3 actualP2;
    private float _timeAcum;

	// Use this for initialization
	void Start () {
        actualP1 = P1.position;
        actualP2 = P2.position;
        transform.position = actualP1;
	}
	
	// Update is called once per frame
	void Update () {
        _timeAcum += Time.deltaTime;

        if( _timeAcum >= TotalTime)
        {
            SwitchOriginAndDestiny();
            _timeAcum = 0;
        }

        float currentTime = _timeAcum / TotalTime;

        transform.position = Vector3.Lerp(actualP1, actualP2, currentTime);
	}

    private void SwitchOriginAndDestiny()
    {
        Vector3 taux = actualP1;
        actualP1 = actualP2;
        actualP2 = taux;
    }
}
