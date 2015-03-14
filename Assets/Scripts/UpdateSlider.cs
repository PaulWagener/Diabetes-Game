using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UpdateSlider : MonoBehaviour {

	public Text sliderText;

	public void ListenerMethod(float value)
	{
		sliderText.text = (value/2).ToString();
	}

	// Use this for initialization
	void Start () {
		Slider s = this.GetComponent<Slider>();
		s.onValueChanged.AddListener(ListenerMethod);
	}
}
