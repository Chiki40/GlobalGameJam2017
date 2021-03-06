﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public float gravityModule = 9.8f;

	private static GameManager m_instance = null;

	private uint m_currentLevel = 1;

	private const string LEVEL_PREFIX = "Level";

	public void Awake() {
		if (m_instance) {
			Destroy(this.gameObject);
		} else {
			m_instance = this;
			DontDestroyOnLoad(this.gameObject);

			string[] chapuza = new string[1];
			chapuza[0] = LEVEL_PREFIX;
			string[] splittedStr = SceneManager.GetActiveScene().name.Split(chapuza, System.StringSplitOptions.None);
			if (splittedStr.Length > 1) {
				m_currentLevel = System.Convert.ToUInt32(splittedStr[1]);
			} else {
				m_currentLevel = 0;
			}
		}
	}

    void Update()
    {
		if (Input.GetKeyUp(KeyCode.P)) {
			int rand = Random.Range(1, 4);
			UtilSound.instance.PlaySound("click" + rand.ToString());
			NextLevel();
		}
    }

	public static GameManager GetInstance() {
		return m_instance;
	}

	public void LoadGame() {
		m_currentLevel = PlayerPrefs.HasKey(LEVEL_PREFIX) ? (uint)PlayerPrefs.GetInt(LEVEL_PREFIX) : 1;
		SceneManager.LoadScene(LEVEL_PREFIX + (m_currentLevel).ToString());
	}

	public void SelectLevel(int level) {
		m_currentLevel = (uint)level;
		SceneManager.LoadScene(LEVEL_PREFIX + (m_currentLevel).ToString());
	}

	public void RestartLevel()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void NextLevel()
	{
		string nextSceneName = LEVEL_PREFIX + (++m_currentLevel).ToString();
		if (m_currentLevel <= 15) {
			SceneManager.LoadScene(nextSceneName);
		} else {
			SceneManager.LoadScene("credits");
		}
	}

	public void SaveGame() {
		PlayerPrefs.SetInt(LEVEL_PREFIX, (int)m_currentLevel);
	}

	public void GoToMenu() {
		m_currentLevel = 0;
		SceneManager.LoadScene("Menu");
	}

	public void GoToSelectionLevel() {
		SceneManager.LoadScene("LvlSelection");
	}

	public void GoToCredits() {
		SceneManager.LoadScene("Credits");
	}

	public void ExitGame() {
		Application.Quit();
	}

	public void OnDestroy() {
		// If we are not being deleted for duplicated singleton
		if (this == m_instance) {
			if (PlayerPrefs.HasKey("PassedMenu")) {
				PlayerPrefs.DeleteKey("PassedMenu");
			}
		}
	}

}
