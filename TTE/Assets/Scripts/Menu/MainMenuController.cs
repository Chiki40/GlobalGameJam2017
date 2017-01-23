using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour {

	public float timeBeforeOpeningLeftDoor = 3.0f;
	public float leftDoorSpeed = 200.0f;
	public float leftDoorLimit = -385.0f;

	public float timeBeforeOpeningRightDoor = 1.0f;
	public float rightDoorSpeed = 200.0f;
	public float rightDoorLimit = 385.0f;

	public float timeBeforeEnableButtons = 1.0f;
	public float speedShowingButtons = 0.4f;

	private GameObject leftDoor = null;
	private GameObject rightDoor = null;
	private GameObject playButton = null;
	private GameObject levelSelectButton = null;
	private GameObject creditsButton = null;
	private GameObject exitButton = null;

	// Use this for initialization
	public void Start () {
		UtilSound.instance.StopAllSounds();

		leftDoor = transform.FindChild("Puertas").GetChild(0).gameObject;
		rightDoor = transform.FindChild("Puertas").GetChild(1).gameObject;
		playButton = transform.FindChild("Buttons").GetChild(0).gameObject;
		levelSelectButton = transform.FindChild("Buttons").GetChild(1).gameObject;
		creditsButton = transform.FindChild("Buttons").GetChild(2).gameObject;
		exitButton = transform.FindChild("Buttons").GetChild(3).gameObject;
		if (!leftDoor || !rightDoor || !playButton || !levelSelectButton || !creditsButton || !exitButton) {
			Debug.LogError("Error. One MainMenu door or button was not found by MainMenuController script");
			return;
		}

		// Have we already passed through menu?
		if (!PlayerPrefs.HasKey("PassedMenu")) {
			// If we haven't, play animation
			PlayerPrefs.SetInt("PassedMenu", 1);
			StartCoroutine(MenuAnimationStart());
		} else {
			// We have passed through menu, set doors final position
			RectTransform rectTrans = leftDoor.GetComponent<RectTransform>();
			RectTransform rectTrans2 = rightDoor.GetComponent<RectTransform>();
			rectTrans.offsetMin = new Vector2(leftDoorLimit, rectTrans.offsetMin.y);
			rectTrans.offsetMax = new Vector2(leftDoorLimit, rectTrans.offsetMax.y);
			rectTrans2.offsetMin = new Vector2(rightDoorLimit, rectTrans.offsetMin.y);
			rectTrans2.offsetMax = new Vector2(rightDoorLimit, rectTrans.offsetMax.y);
            FindObjectOfType<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(playButton);
			#if UNITY_WEB_GL
			exitButton.SetActive(false);
			#endif
		}
	}

	private IEnumerator MenuAnimationStart() {
		playButton.SetActive(false);
		levelSelectButton.SetActive(false);
		creditsButton.SetActive(false);
		exitButton.SetActive(false);

		yield return new WaitForSeconds(timeBeforeOpeningLeftDoor);
		UtilSound.instance.PlaySound("door");
		RectTransform rectTrans = leftDoor.GetComponent<RectTransform>();
		float left = rectTrans.offsetMin.x;
		float right = rectTrans.offsetMax.x;
		while (left > leftDoorLimit) {
			left -= leftDoorSpeed * Time.deltaTime;
			right -= leftDoorSpeed * Time.deltaTime;
			rectTrans.offsetMin = new Vector2(left, rectTrans.offsetMin.y);
			rectTrans.offsetMax = new Vector2(right, rectTrans.offsetMax.y);
			yield return null;
		}

		yield return new WaitForSeconds(timeBeforeOpeningRightDoor);
		UtilSound.instance.PlaySound("door");
		rectTrans = rightDoor.GetComponent<RectTransform>();
		left = rectTrans.offsetMin.x;
		right = rectTrans.offsetMax.x;
		while (right < rightDoorLimit) {
			left += rightDoorSpeed * Time.deltaTime;
			right += rightDoorSpeed * Time.deltaTime;
			rectTrans.offsetMin = new Vector2(left, rectTrans.offsetMin.y);
			rectTrans.offsetMax = new Vector2(right, rectTrans.offsetMax.y);
			yield return null;
		}

		yield return new WaitForSeconds(timeBeforeEnableButtons);
		playButton.SetActive(true);
		levelSelectButton.SetActive(true);
		creditsButton.SetActive(true);
		#if !UNITY_WEB_GL
		exitButton.SetActive(false);
		#endif
		exitButton.SetActive(true);
        FindObjectOfType<UnityEngine.EventSystems.EventSystem>().SetSelectedGameObject(playButton);
		Color color1 = playButton.GetComponent<Image>().color;
		Color color2 = levelSelectButton.GetComponent<Image>().color;
		Color color3 = creditsButton.GetComponent<Image>().color;
		Color color4 = exitButton.GetComponent<Image>().color;
		color1.a = 0.0f;
		color2.a = 0.0f;
		color3.a = 0.0f;
		color4.a = 0.0f;
		playButton.GetComponent<Image>().color = color1;
		levelSelectButton.GetComponent<Image>().color = color2;
		creditsButton.GetComponent<Image>().color = color3;
		exitButton.GetComponent<Image>().color = color4;
		while (color1.a < 1.0f || color2.a < 1.0f || color3.a < 1.0f || color4.a < 1.0f) {
			float increment = speedShowingButtons * Time.deltaTime;
			color1.a += increment;
			color2.a += increment;
			color3.a += increment;
			color4.a += increment;
			playButton.GetComponent<Image>().color = color1;
			levelSelectButton.GetComponent<Image>().color = color2;
			creditsButton.GetComponent<Image>().color = color3;
			exitButton.GetComponent<Image>().color = color4;
			yield return null;
		}
	}

	public void GoToPlay() {
		int rand = Random.Range(1, 4);
		UtilSound.instance.PlaySound("click" + rand.ToString());
		GameManager.GetInstance().NextLevel();
	}
		
	public void GoToSelectionLevel() {
		int rand = Random.Range(1, 4);
		UtilSound.instance.PlaySound("click" + rand.ToString());
		GameManager.GetInstance().GoToSelectionLevel();
	}

	public void GoToCredits() {
		int rand = Random.Range(1, 4);
		UtilSound.instance.PlaySound("click" + rand.ToString());
		GameManager.GetInstance().GoToCredits();
	}

	public void ExitGame() {
		int rand = Random.Range(1, 4);
		UtilSound.instance.PlaySound("click" + rand.ToString());
		GameManager.GetInstance().ExitGame();
	}
}
