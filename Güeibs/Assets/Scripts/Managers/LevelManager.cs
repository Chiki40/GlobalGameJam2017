using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public int LevelID;
    private static LevelManager m_instance;

    void Awake()
    {
		if (m_instance != null) {
			Destroy (this.gameObject);
		} else {
			m_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
    }

    public static LevelManager GetInstance()
    {
        return m_instance;
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void NextLevel()
    {
        //TODO call to gamemanager to load the next level
    }
}
