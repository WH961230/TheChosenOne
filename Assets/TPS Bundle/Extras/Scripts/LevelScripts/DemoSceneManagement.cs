using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// This class is created for the example scene. There is no support for this script.
public class DemoSceneManagement : MonoBehaviour
{
	public Texture2D cursorTexture;
	public GameObject headshotHUD;

	private string message = "";
	private bool showMsg = false;

	private int w = 550;
	private int h = 100;
	private Rect textArea;
	private GUIStyle style;
	private Color textColor;

	private GameObject mainMenuUI, infoUI, settingsUI, keyboardUI, gamepadUI, adjustSensitivityUI;

	public void HeadShotCallback()
	{
		Instantiate(headshotHUD, transform.Find("ScreenHUD/InfoHUD"));
	}

	void Awake()
	{
		transform.Find("ScreenHUD/Fader").gameObject.SetActive(true);
		style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;
		style.fontSize = 36;
		style.wordWrap = true;
		textColor = Color.white;
		textColor.a = 0;
		textArea = new Rect((Screen.width - w) / 2, 0, w, h);

		ThirdPersonOrbitCam cam = Camera.main.GetComponent<ThirdPersonOrbitCam>();
		float speed = PlayerPrefs.GetFloat("aimingSpeed", cam.horizontalAimingSpeed * 10f);
		if (cam.horizontalAimingSpeed != speed / 10f)
			cam.horizontalAimingSpeed = cam.verticalAimingSpeed = speed / 10f;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Cursor.SetCursor(cursorTexture, Vector2.zero, CursorMode.Auto);

		mainMenuUI = transform.Find("ScreenHUD/MainMenuUI").gameObject;
		infoUI = transform.Find("ScreenHUD/AboutUI").gameObject;
		settingsUI = transform.Find("ScreenHUD/SettingsUI").gameObject;
		keyboardUI = transform.Find("ScreenHUD/KeyboardUI").gameObject;
		gamepadUI = transform.Find("ScreenHUD/GamepadUI").gameObject;
		adjustSensitivityUI = transform.Find("ScreenHUD/SensitivityUI").gameObject;
	}

	void Update()
	{
		if (Input.GetKeyDown("escape"))
		{
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			MainMenu();
			Time.timeScale = 0;
		}
		else if (Input.GetKeyDown("joystick button 7"))
		{
			MainMenu();
			Time.timeScale = 0;
		}
	}

	void OnGUI()
	{
		if (showMsg)
		{
			if (textColor.a <= 1)
				textColor.a += 0.5f * Time.deltaTime;
		}
		// no hint to show
		else
		{
			if (textColor.a > 0)
				textColor.a -= 0.5f * Time.deltaTime;
		}

		style.normal.textColor = textColor;

		GUI.Label(textArea, message, style);
	}

	public void ExitGame()
	{
#if UNITY_EDITOR
		EditorApplication.ExecuteMenuItem("Edit/Play");
#else
		Application.Quit();
#endif
	}

	public void Resume()
	{
		mainMenuUI.SetActive(false);
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		Time.timeScale = 1;
	}

	public void RestartScene()
	{
		Time.timeScale = 1;
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ShowCustomUI(GameObject ui)
	{
		MainMenu();
		ui.SetActive(true);
		ui.transform.Find("Buttons").GetChild(0).GetComponent<Button>().Select();
		mainMenuUI.SetActive(false);
	}

	public void MainMenu()
	{
		mainMenuUI.SetActive(true);
		mainMenuUI.transform.Find("Buttons").GetChild(0).GetComponent<Button>().Select();
		infoUI.SetActive(false);
		settingsUI.SetActive(false);
		keyboardUI.SetActive(false);
		gamepadUI.SetActive(false);
		adjustSensitivityUI.SetActive(false);
	}
}
