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
			text.text = "+" + food.Increase + " bloedsuiker per beurt\nnog " + food.Duration + " beurten";
		}
		RectTransform t = GetComponent<RectTransform> ();
		t.anchoredPosition = Vector2.Lerp (t.anchoredPosition, new Vector2 (150.0f, 50.0f * index), 0.5f);
	
	}

	public void SetFood(Food f) {
		image.sprite = f.sprite;
		this.food = f;
	}
}
