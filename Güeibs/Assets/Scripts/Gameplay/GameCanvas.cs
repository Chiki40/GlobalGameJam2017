using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCanvas : MonoBehaviour {

	public void GoToMenu() {
		int rand = Random.Range(1, 4);
		UtilSound.instance.PlaySound("click" + rand.ToString());
		GameManager.GetInstance().GoToMenu();
	}
}
