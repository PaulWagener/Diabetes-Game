using UnityEngine;
using System.Collections;

public class Camera : MonoBehaviour {

	public Game game;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 position = game.CurrentPlayer.gameObject.transform.position;
		transform.position = Vector3.Lerp (transform.position, new Vector3 (position.x, position.y, -10.0f), 0.05f);
	}
}
