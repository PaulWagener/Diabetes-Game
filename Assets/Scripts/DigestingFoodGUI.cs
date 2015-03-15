using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DigestingFoodGUI : MonoBehaviour {
	
	public Text text;

	public Food food;
	public Image image;

	public int index = 0;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (food != null) {
			text.text = "+" + food.Increase + " bloedsuiker per beurt\nnog " + (food.Duration + 1) + " beurt" + (food.Duration == 0 ? "" : "en");
		}
		RectTransform t = GetComponent<RectTransform> ();
		t.anchoredPosition = Vector2.Lerp (t.anchoredPosition, new Vector2 (0.0f, (-100.0f * index)), 0.1f);
	
	}

	public void SetFood(Food f) {
		image.sprite = f.sprite;
		this.food = f;
	}
}
