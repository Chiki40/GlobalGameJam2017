using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour {

	private void Start() {
		// Enable back button if we are not in Android
		#if !UNITY_ANDROID
		Transform backButton = this.transform.FindChild("Back");
		Transform restartButton = this.transform.FindChild("Restart");
		if (!backButton || !restartButton) {
			Debug.LogError("Error. Back or Restart children not found in " + gameObject.name + " object");
			return;
		}
		backButton.gameObject.SetActive(true);
		restartButton.gameObject.SetActive(true);
		#endif
	}

	private void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			GoToMenu();
		} else if (Input.GetKeyDown(KeyCode.R)) {
			Restart();
		}
	}

	public void GoToMenu() {
		int rand = Random.Range(1, 4);
		UtilSound.instance.PlaySound("click" + rand.ToString());
		GameManager.GetInstance().GoToMenu();
	}

	public void Restart() {
		int rand = Random.Range(1, 4);
		UtilSound.instance.PlaySound("click" + rand.ToString());
		GameManager.GetInstance().RestartLevel();
	}
}
