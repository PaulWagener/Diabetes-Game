using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Tile currentTile;

	float playerZ;

	// Use this for initialization
	void Start () {
		playerZ = gameObject.transform.position.z;
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector3.Lerp(gameObject.transform.position, currentTile.transform.position, 0.1f);
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, playerZ);
	}
}
