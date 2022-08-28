using UnityEngine;
using UnityEngine.UI;

// This class is created for the example scene. There is no support for this script.
public class SliderHandler : MonoBehaviour
{
	private Slider slider;
	private ThirdPersonOrbitCam cam;

	public void Start()
	{
		cam = Camera.main.GetComponent<ThirdPersonOrbitCam>();
		slider = GetComponent<Slider>();
		float speed = cam.horizontalAimingSpeed * 10f;
		transform.Find("value").GetComponent<Text>().text = speed.ToString();
		slider.value = speed;
		//Adds a listener to the main slider and invokes a method when the value changes.
		slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
	}

	public void ValueChangeCheck()
	{
		float value = slider.value;
		cam.horizontalAimingSpeed = cam.verticalAimingSpeed = value / 10f;
		PlayerPrefs.SetFloat("aimingSpeed", value);
		transform.Find("value").GetComponent<Text>().text = value.ToString();
	}
}
