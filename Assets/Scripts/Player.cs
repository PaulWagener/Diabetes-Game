using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public Tile currentTile;
	
	public int remainingMoves = 5;
	private Game game;

	float playerZ;

	// Use this for initialization
	void Start () {
		game = FindObjectOfType<Game> ();
		playerZ = gameObject.transform.position.z;
	}
	
	// Animate to the current tile
	void Update () {
		gameObject.transform.position = Vector3.Slerp (gameObject.transform.position, currentTile.transform.position, 0.1f);
		gameObject.transform.position = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, playerZ);
	}

	/**
	 * Moves the player to a new tile
	 */
	public void MoveToTile(Tile newTile) {
		// Clicking on the current tile is a no-op
		if (newTile == currentTile) {
			return;
		}

		currentTile = newTile;
		remainingMoves--;

		if (remainingMoves == 0) {
			game.EndPlayerMoveTurn();
		}
	}
}
