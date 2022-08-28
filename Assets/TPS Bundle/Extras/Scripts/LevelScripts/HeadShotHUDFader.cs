using UnityEngine;
using UnityEngine.UI;

// This class is created for the example scene. There is no support for this script.
public class HeadShotHUDFader : MonoBehaviour
{
	private Image headshotCrosshair;

    void Start()
    {
		headshotCrosshair = GetComponent<Image>();
    }

    void Update()
    {
		headshotCrosshair.color = GetUpdatedAlpha(headshotCrosshair.color);
		if (headshotCrosshair.color.a <= 0)
		{
			Destroy(gameObject);
		}
	}

	private Color GetUpdatedAlpha(Color currentColor, bool reset = false)
	{
		if (reset)
		{
			currentColor.a = 1f;
		}
		else
		{
			currentColor.a -= Time.deltaTime;
		}

		return currentColor;
	}
}
