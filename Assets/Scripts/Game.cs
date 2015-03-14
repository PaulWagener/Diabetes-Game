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

	public Sprite TAKE_1_STEP;
	public Sprite TAKE_2_STEPS;
	public Sprite TAKE_3_STEPS;
	
	public MovementCard prefabCard;

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

	// GUI for card selecting
	public MovementCard[] cards;
	public Text chooseCardsText;
	public Button okButton;

	// Use this for initialization
	void Start () {
		StartNextPlayerTurn ();
		foreach (Player player in GameObject.FindObjectsOfType<Player>())
			players.Add (player);

		foreach (MovementCard card in cards) {
			card.finalPosition = MovementCard.OUT_OF_VIEW_POSITION;
		}

	}

	void ShowCardsSelecting() {
		// GUI
		chooseCardsText.CrossFadeAlpha (1.0f, 0.2f, true);
		okButton.GetComponent<RectTransform>().anchoredPosition = new Vector2 (0.0f, -160.0f);

		// Position the cards to show to the user
		cards[0].finalPosition = new Vector3 (-100.0f, -50.0f, 0.0f);
		cards[0].finalRotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, 20.0f));
		cards[1].finalPosition = new Vector3 (100.0f, -50.0f, 0.0f);
		cards[1].finalRotation = Quaternion.Euler (new Vector3 (0.0f, 0.0f, -20.0f));

		int numberOfCards = 3;
		if (numberOfCards == 3) {
			cards[2].finalPosition = new Vector3 (0.0f, -20.0f, 0.0f);
			cards[2].finalRotation = Quaternion.identity;
		}
		cards [0].SetSelected (false);
		cards [1].SetSelected (false);
		cards [2].SetSelected (false);

		// Assign random values to the cards
		foreach (MovementCard card in cards) {
			card.steps = Random.Range(1, 4); 
			if(card.steps == 1) {
				card.GetComponent<Image>().sprite = TAKE_1_STEP;
			} else if(card.steps == 2) {
				card.GetComponent<Image>().sprite = TAKE_2_STEPS;
			} else if(card.steps == 3) {
				card.GetComponent<Image>().sprite = TAKE_3_STEPS;
			}
		}
	}

	/**
	 * When the user presses "OK"
	 */
	public void OnCardsSelected() {
		int totalMovement = 0;
		foreach(MovementCard card in cards) {
			card.finalPosition = MovementCard.OUT_OF_VIEW_POSITION;
			card.finalRotation = Quaternion.identity;
			if (card.isSelected) {
				totalMovement += card.steps;
			}
		}

		CurrentPlayer.remainingMoves = totalMovement;
		chooseCardsText.CrossFadeAlpha (0.0f, 0.2f, true);
		okButton.transform.position = new Vector3 (1000.0f, 1000.0f, 0.0f);

		remainingMovesText.text = CurrentPlayer.remainingMoves + " zetten over";
	}

	void Update () {
		// Animate the position of the cards
		foreach (MovementCard c in cards) {
			RectTransform t = c.GetComponent<RectTransform> ();
			t.localPosition = Vector3.Lerp(t.localPosition, c.finalPosition, 0.1f);
			t.localRotation = Quaternion.Lerp(t.localRotation, c.finalRotation, 0.1f);
		}

	}

	public void EndPlayerMoveTurn() {
		Debug.Log("Einde beurt");
		StartNextPlayerTurn ();
	}

	public void OnOkButton() {
		CurrentPlayer.glucoseLevel -= popup.SliderVal;
		CurrentPlayer.Nom();
		CurrentPlayer.remainingMoves = 5;

		ShowCardsSelecting ();
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
