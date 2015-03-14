using UnityEngine;
using System.Collections;

public class Game : MonoBehaviour {

	public Player[] players;

	public Player currentPlayer;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndPlayerMoveTurn() {
		Debug.Log("Einde beurt");
	}

	public void OnTileClicked(Tile t) {
		currentPlayer.MoveToTile(t);
	}
}
