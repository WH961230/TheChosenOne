using System.Collections;
using UnityEngine;
using UnityEngine.UI;

// This class is created for the example scene. There is no support for this script.
public class PlayerHealth : HealthManager
{
	public float health = 100f;
	public float criticalHealth = 30f;
	public Transform healthHUD;
	public AudioClip deathClip;
	public AudioClip[] hitClips;
	public GameObject hurtPrefab;
	public float decayFactor = 0.8f;

	private float totalHealth;
	private BlinkHUD criticalHud;
	private RectTransform healthBar, placeHolderBar;
	private Text healthLabel;
	private float originalBarScale;
	private bool critical;
	private HurtHUD hurtHUD;

	void Awake()
    {
		totalHealth = health;
		criticalHud = healthHUD.Find("Bloodframe").GetComponent<BlinkHUD>();
		healthBar = healthHUD.Find("HealthBar/Bar").GetComponent<RectTransform>();
		placeHolderBar = healthHUD.Find("HealthBar/Placeholder").GetComponent<RectTransform>();
		healthLabel = healthHUD.Find("HealthBar/Label").GetComponent<Text>();
		originalBarScale = healthBar.sizeDelta.x;
		healthLabel.text = "" + (int)health;

		hurtHUD = this.gameObject.AddComponent<HurtHUD>();
		hurtHUD.Setup(healthHUD, hurtPrefab, decayFactor, this.transform);
	}

	void Update()
	{
		if(placeHolderBar.sizeDelta.x > healthBar.sizeDelta.x)
		{
			placeHolderBar.sizeDelta = Vector2.Lerp(placeHolderBar.sizeDelta, healthBar.sizeDelta, 2f * Time.deltaTime);
		}
	}

	public bool IsFullLife()
	{
		return health == totalHealth;
	}

	public override void TakeDamage(Vector3 location, Vector3 direction, float damage, Collider bodyPart = null, GameObject origin = null)
	{
		health -= damage;

		UpdateHealthBar();

		if (hurtPrefab && healthHUD)
			hurtHUD.DrawHurtUI(origin.transform, origin.GetHashCode());

		if (health <= 0)
		{
			Kill();
		}
		else if (health <= criticalHealth && !critical)
		{
			critical = true;
			criticalHud.StartBlink();
		}

		AudioSource.PlayClipAtPoint(hitClips[Random.Range(0, hitClips.Length)], location, 0.1f);
	}
	private void UpdateHealthBar()
	{
		healthLabel.text = "" + (int)health;

		float scaleFactor = health / totalHealth;
		healthBar.sizeDelta = new Vector2(scaleFactor * originalBarScale, healthBar.sizeDelta.y);
	}

	private void Kill()
	{
		dead = true;
		gameObject.layer = LayerMask.NameToLayer("Default");
		gameObject.tag = "Untagged";
		healthHUD.gameObject.SetActive(false);
		healthHUD.parent.Find("WeaponHUD").gameObject.SetActive(false);
		GetComponent<Animator>().SetBool("Aim", false);
		GetComponent<Animator>().SetBool("Cover", false);
		GetComponent<Animator>().SetFloat("Speed", 0);
		foreach (GenericBehaviour behaviour in GetComponentsInChildren<GenericBehaviour>())
		{
			behaviour.enabled = false;
		}
		this.GetComponentInChildren<SpawnEffect>().enabled = true;
		AudioSource.PlayClipAtPoint(deathClip, transform.position, 5);
	}

}
