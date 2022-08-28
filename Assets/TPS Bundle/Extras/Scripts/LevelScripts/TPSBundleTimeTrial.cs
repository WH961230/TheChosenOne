using UnityEngine;
using UnityEngine.UI;

// This class is created for the example scene. There is no support for this script.
public class TPSBundleTimeTrial : MonoBehaviour
{
	public Text enemiesInfo, currentInfo, bestInfo, perfectInfo;

	private GameObject[] allEnemies;
	private int enemies;
	private bool isValidPerfectTime;

	private float bestTime, perfectTime, totalTime, startTime = 0;
	private string bestTimeLabel, perfectTimeLabel, currentTimeLabel;
	private bool isTimerRunning = false;
	private GameObject player;

	private void Start()
	{
		player = GameObject.FindGameObjectWithTag("Player");
		isValidPerfectTime = true;
		allEnemies = GameObject.FindGameObjectsWithTag("Enemy");

		if (PlayerPrefs.HasKey("bestTime"))
		{
			bestTime = PlayerPrefs.GetFloat("bestTime");
			bestTimeLabel = bestTime.ToString("n2").Replace(",", ".");
		}
		else
		{
			bestTimeLabel = "-";
		}
		if (PlayerPrefs.HasKey("perfectTime"))
		{
			perfectTime = PlayerPrefs.GetFloat("perfectTime");
			perfectTimeLabel = perfectTime.ToString("n2").Replace(",", ".");
		}
		else
		{
			perfectTimeLabel = "-";
		}
		currentTimeLabel = "0.00";
		StartTimer();
	}

	void Update()
	{
		enemies = 0;
		foreach (GameObject enemy in allEnemies)
			if (enemy.activeSelf && !enemy.GetComponent<HealthManager>().dead)
				enemies++;

		if (isTimerRunning)
		{
			currentTimeLabel = (Time.time - startTime).ToString("n2").Replace(",", ".");
		}

		if (enemies == 0 && isTimerRunning)
		{
			EndTimer();
		}

		if (isValidPerfectTime && player.GetComponent<PlayerHealth>())
			isValidPerfectTime = player.GetComponent<PlayerHealth>().IsFullLife();
		else
			isValidPerfectTime = false;
	}

	private void StartTimer()
	{
		isTimerRunning = true;
		startTime = Time.time;
	}

	private void EndTimer()
	{
		isTimerRunning = false;
		totalTime = Time.time - startTime;
		startTime = 0;

		if (bestTime == 0 || (bestTime > 0 && totalTime < bestTime))
		{
			bestTime = totalTime;
			currentTimeLabel = bestTimeLabel = bestTime.ToString("n2").Replace(",", ".");
			PlayerPrefs.SetFloat("bestTime", bestTime);
		}
		if ((isValidPerfectTime) && (perfectTime == 0 || (perfectTime > 0 && totalTime < perfectTime)))
		{
			perfectTime = totalTime;
			currentTimeLabel = perfectTimeLabel = perfectTime.ToString("n2").Replace(",", ".");
			PlayerPrefs.SetFloat("perfectTime", perfectTime);
		}
	}

	public void OnGUI()
	{
		enemiesInfo.text = "Enemies left: " + enemies.ToString();
		currentInfo.text = currentTimeLabel;
		bestInfo.text = bestTimeLabel;
		perfectInfo.text = perfectTimeLabel;
	}
}
