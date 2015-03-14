using UnityEngine;
using System.Collections;

public class Tile : MonoBehaviour {

	public Tile[] connectingTiles;

	private Game game;

	// Use this for initialization
	void Start () {
		game = FindObjectOfType<Game> ();
	}
	
	// Update is called once per frame
	void Update () {

	}

	void OnMouseDown() {
		game.currentPlayer.currentTile = this;
	}
}
