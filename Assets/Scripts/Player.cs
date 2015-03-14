using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Tile currentTile;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector3.Slerp (gameObject.transform.position, currentTile.transform.position, 0.1f);

	}
}
