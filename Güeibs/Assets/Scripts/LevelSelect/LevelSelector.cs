using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {

	public void Update() {
		if (Input.GetKeyDown(KeyCode.Escape)) {
			GoBack();
			return;
		}
	}

	public void SelectLevel(int level) {
		GameManager.GetInstance().SelectLevel(level);
	}

	public void GoBack() {
		GameManager.GetInstance().GoToMenu();
	}
}
