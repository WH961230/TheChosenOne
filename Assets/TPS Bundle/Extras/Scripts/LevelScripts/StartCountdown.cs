using UnityEngine;
using UnityEngine.UI;

// This class is created for the example scene. There is no support for this script.
public class StartCountdown : MonoBehaviour
{
	public Text countdown;
	public float timeToStart = 3f;
	public AudioClip countdownTick, startTick;

	private int currentTime;
	private GameObject player;
	private Color fullAlpha, noAlpha;
	void Awake()
	{
		currentTime = (int)timeToStart;
		player = GameObject.FindGameObjectWithTag("Player");
		foreach (GenericBehaviour behaviour in player.GetComponentsInChildren<GenericBehaviour>())
		{
			behaviour.enabled = false;
		}
		player.GetComponent<BasicBehaviour>().enabled = false;
		fullAlpha = noAlpha = Color.white;
		noAlpha.a = 0f;
	}

	void Update()
	{
		if (currentTime == timeToStart)
			AudioSource.PlayClipAtPoint(countdownTick, player.transform.position);
		timeToStart -= Time.deltaTime;
		if (timeToStart < (currentTime - 1))
		{
			currentTime--;
			if (timeToStart > 0)
				AudioSource.PlayClipAtPoint(countdownTick, player.transform.position);
			else
				AudioSource.PlayClipAtPoint(startTick, player.transform.position);
		}
		if (timeToStart <= 0)
		{
			foreach (GenericBehaviour behaviour in player.GetComponentsInChildren<GenericBehaviour>())
			{
				behaviour.enabled = true;
			}
			player.GetComponent<BasicBehaviour>().enabled = true;
			GetComponent<TPSBundleTimeTrial>().enabled = true;
			this.enabled = false;
		}
	}

	private Color UpdateColorAlpha()
	{
		return Color.Lerp(fullAlpha, noAlpha, currentTime - timeToStart);
	}

	public void OnGUI()
	{
		countdown.color = UpdateColorAlpha();
		countdown.text = currentTime.ToString();
	}
}
