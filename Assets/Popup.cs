using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Popup : MonoBehaviour {

	public Button OkButton;
	public Button OtherButton;
	public Text DescriptionText;
	public Slider slider;

	private GameObject origin;
	private Text sliderTextField;

	private string description;
	private string okText;
	private string otherText;

	public float SliderVal {
		get {
			return Convert.ToSingle(sliderTextField.text);
		}
	}

	public string OkText {
		get {return okText;}
		set {
			okText = value;
			OkButton.GetComponentInChildren<Text>().text = value;
		}
	}

	public string OtherText {
		get {return otherText;}
		set {
			otherText = value;
			OtherButton.gameObject.SetActive(value != "");
			if(value != "") {
				OtherButton.GetComponentInChildren<Text>().text = value;
			}

		}
	}

	public string Description {
		get {return description;}
		set {
			description = value;
			DescriptionText.text = value;
		}
	}

	public void ClickOK() {
		if (origin.GetComponent<Event>())
			origin.GetComponent<Event>().OnOkButton();
		if (origin.GetComponent<Game>())
			origin.GetComponent<Game>().OnOkButton();
		gameObject.SetActive(false);
	}

	public void ClickOther()
	{
		if (origin.GetComponent<Event>())
			origin.GetComponent<Event>().OnOtherButton();
		gameObject.SetActive(false);
	}

	// Use this for initialization
	void Start () {
		sliderTextField = slider.GetComponent<UpdateSlider>().sliderText;
	}

	public void ShowPopup(GameObject origin) {
		this.origin = origin;
		gameObject.SetActive(true);
	}
}
