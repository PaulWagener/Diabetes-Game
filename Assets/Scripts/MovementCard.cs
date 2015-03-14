using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MovementCard : MonoBehaviour {

	public static Vector3 OUT_OF_VIEW_POSITION = new Vector3(0, -300, 0);

	public Vector3 finalPosition;
	public Quaternion finalRotation;

	public bool isSelected = false;

	private Image image;

	public int steps;
	
	void Start () {
		image = GetComponent<Image> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnMouseDown() 
	{
		// Toggle selected state
		SetSelected (!isSelected);
	}

	public void SetSelected(bool selected) {
		if (selected) {
			image.color = new Color(1.0f, 0.5f, 0.5f, 1.0f);
		} else {
			image.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);
		}
		isSelected = selected;
	}
}
