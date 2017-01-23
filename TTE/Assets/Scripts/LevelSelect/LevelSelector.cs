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
		int rand = Random.Range(1, 4);
		UtilSound.instance.PlaySound("click" + rand.ToString());
		GameManager.GetInstance().SelectLevel(level);
	}

	public void GoBack() {
		int rand = Random.Range(1, 4);
		UtilSound.instance.PlaySound("click" + rand.ToString());
		GameManager.GetInstance().GoToMenu();
	}
}
