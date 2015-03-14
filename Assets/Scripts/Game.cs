using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Game : MonoBehaviour {
	public Sprite NORMAL_SPRITE;
	public Sprite EVENT_SPRITE;
	public Sprite HOSPITAL_SPRITE;
	public Sprite PHARMACY_SPRITE;

	public Popup popup;

	[HideInInspector]
	public List<Player> players;

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
		foreach (Player player in GameObject.FindObjectsOfType<Player>())
			players.Add(player);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void EndPlayerMoveTurn() {
		Debug.Log("Einde beurt");
		StartNextPlayerTurn ();
	}

	public void OnOkButton() {
		CurrentPlayer.glucoseLevel -= popup.SliderVal;
		CurrentPlayer.Nom();
		CurrentPlayer.remainingMoves = 5;
	}

	public void StartNextPlayerTurn() {
		currentPlayerIndex++;
		if (currentPlayerIndex >= players.Count) {
			currentPlayerIndex = 0;
		}

		popup.Description = "Set Insuline!";
		popup.OtherText = "";
		popup.slider.gameObject.SetActive(true);
		popup.ShowPopup(gameObject);

		// Start the card selecting phase
		// TODO

		whoseTurnText.text = "Player " + (currentPlayerIndex + 1) + " is aan de beurt";
		remainingMovesText.text = CurrentPlayer.remainingMoves + " zetten over";
	}

	public void OnTileClicked(Tile t) {
		if (CurrentPlayer.currentTile.connectingTiles.Contains(t))
		{
			CurrentPlayer.MoveToTile(t);
			remainingMovesText.text = CurrentPlayer.remainingMoves + " zetten over";
		}
	}
}
