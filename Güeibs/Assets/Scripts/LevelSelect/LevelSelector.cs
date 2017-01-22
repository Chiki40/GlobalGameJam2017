using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSelector : MonoBehaviour {

	public void SelectLevel(int level) {
		GameManager.GetInstance().SelectLevel(level);
	}
}
