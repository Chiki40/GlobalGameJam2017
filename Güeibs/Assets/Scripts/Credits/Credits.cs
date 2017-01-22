using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Credits : MonoBehaviour {

	public float velocity = 10.0f;
	public float timeBeforeGoToMenu = 4.0f;

	void Start() {
		Image creditsImage = this.gameObject.GetComponent<Image>();
		if (!creditsImage) {
			Debug.LogError("Error. Credits object does not have an Image associated");
		}
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			GoToMenu();
			return;
		}
		RectTransform rectTrans = this.gameObject.GetComponent<RectTransform>();		
		float bottom = rectTrans.offsetMin.y;
		float top = rectTrans.offsetMax.y;		
		if (bottom >= 0.0f) {
			StartCoroutine(WaitBeforeGoToMenu());
		} else {		
			bottom += velocity * Time.deltaTime;
			top += velocity * Time.deltaTime;
			rectTrans.offsetMin = new Vector2(rectTrans.offsetMin.x, bottom);
			rectTrans.offsetMax = new Vector2(rectTrans.offsetMax.x, top);
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
