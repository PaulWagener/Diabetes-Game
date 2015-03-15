using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BloodSugar : MonoBehaviour {

	public Text levelText;
	public RectTransform Indicator;

	public Player linkedPlayer;

	// Use this for initialization
	void Start () {
	
	}

	public static float Map(float x, float in_min, float in_max, float out_min, float out_max) {
		return (x - in_min) * (out_max - out_min) / (in_max - in_min) + out_min;
	}
	
	// Update is called once per frame
	void Update () {
		levelText.text = "Bloedsuiker: " + linkedPlayer.glucoseLevel;
		float x = Map (linkedPlayer.glucoseLevel, 0.0f, 15.0f, -208.0f, 208.0f);
		Indicator.anchoredPosition = Vector2.Lerp(Indicator.anchoredPosition, new Vector3 (x, 123), 0.05f);
	}
}
