using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

	private static GameManager m_instance = null;

	private uint m_currentLevel = 1;

	private const string LEVEL_PREFIX = "Level";

	public void Awake() {
		if (m_instance) {
			Destroy(this.gameObject);
		} else {
			m_instance = this;
			DontDestroyOnLoad(this.gameObject);
			m_currentLevel = PlayerPrefs.HasKey(LEVEL_PREFIX) ? (uint)PlayerPrefs.GetInt(LEVEL_PREFIX) : 1;
		}
	}

	public static GameManager GetInstance() {
		return m_instance;
	}

	public string ReturnNextLevelName() {
		return LEVEL_PREFIX + (++m_currentLevel).ToString();
	}

	public void SaveGame() {
		PlayerPrefs.SetInt(LEVEL_PREFIX, (int)m_currentLevel);
	}

}
