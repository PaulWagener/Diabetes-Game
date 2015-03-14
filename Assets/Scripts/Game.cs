using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Game : MonoBehaviour {

	public Player[] players;

	// Whose turn is it?
	private int currentPlayerIndex = -1;
	public Player CurrentPlayer {
		get {
			return players [currentPlayerIndex];
		}
	}

	public Text whoseTurnText;
	public Text remainingMovesText;

	// Use this for initialization
	void Start () {
		StartNextPlayerTurn ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndPlayerMoveTurn() {
		Debug.Log("Einde beurt");
		StartNextPlayerTurn ();
	}

	public void StartNextPlayerTurn() {
		currentPlayerIndex++;
		if (currentPlayerIndex >= players.Length) {
			currentPlayerIndex = 0;
		}
		whoseTurnText.text = "Player " + (currentPlayerIndex + 1) + " is aan de beurt";

		// Start the card selecting phase
		// TODO

		CurrentPlayer.remainingMoves = 5;
	}

	public void OnTileClicked(Tile t) {
		CurrentPlayer.MoveToTile(t);
		remainingMovesText.text = CurrentPlayer.remainingMoves + " zetten over";
	}
}
