using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

	public float velocity = 10.0f;
	public float timeBeforeGoToMenu = 4.0f;

	public ScrollRect scroll = null;

	void Start() {
		UtilSound.instance.StopAllSounds();
		Image creditsImage = this.gameObject.GetComponent<Image>();
		if (!scroll ||!creditsImage) {
			Debug.LogError("Error. Not found an Image on Credits script gameobject, or a ScrollRect assigned in editor");
		}
		UtilSound.instance.PlaySound("creditsmusic", 0.15f, true);
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			GoToMenu();
			return;
		}
		float pos = scroll.verticalNormalizedPosition;
		if (pos <= 0.0f) {
			return;
		}
		pos = Mathf.Clamp01(pos - velocity * Time.deltaTime);
		scroll.verticalNormalizedPosition = pos;
		if (pos <= 0.0f) {
			StartCoroutine(WaitBeforeGoToMenu());
		}
	}

	private IEnumerator WaitBeforeGoToMenu() {
		yield return new WaitForSeconds(timeBeforeGoToMenu);
		GoToMenu();
	}

	public void GoToMenu() {
		int rand = Random.Range(1, 4);
		UtilSound.instance.PlaySound("click" + rand.ToString());
		GameManager.GetInstance().GoToMenu();
	}
}
