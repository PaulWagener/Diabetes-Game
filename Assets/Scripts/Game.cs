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

	List<Player> players;

	bool boarddisabled = false;

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
	public GameObject chooseCardsPanel;
	public MovementCard[] cards;
	public Text chooseCardsText;
	public Button okButton;
	public BloodSugar glucoSlider;

	float closeCardsAfter = 0.0f;

	void Awake()
	{
		players = new List<Player>();
		foreach (Player player in GameObject.FindObjectsOfType<Player>())
			players.Add(player);
	}

	// Use this for initialization
	void Start () {
		StartNextPlayerTurn ();

		foreach (MovementCard card in cards) {
			card.finalPosition = MovementCard.OUT_OF_VIEW_POSITION;
		}
		chooseCardsPanel.SetActive(false);
	}

	void ShowCardsSelecting() {
		// GUI
		boarddisabled = true;
		chooseCardsPanel.SetActive(true);
		closeCardsAfter = 0.0f;
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

		remainingMovesText.text = CurrentPlayer.remainingMoves + " zetten over";

		closeCardsAfter = 0.5f;
		boarddisabled = false;
	}

	void Update () {
		// Animate the position of the cards
		foreach (MovementCard c in cards) {
			RectTransform t = c.GetComponent<RectTransform> ();
			t.localPosition = Vector3.Lerp(t.localPosition, c.finalPosition, 0.1f);
			t.localRotation = Quaternion.Lerp(t.localRotation, c.finalRotation, 0.1f);
		}

		if (closeCardsAfter > 0.0f)
		{
			closeCardsAfter -= Time.deltaTime;
			if (closeCardsAfter <= 0.0f)
			{
				closeCardsAfter = 0.0f;

				chooseCardsPanel.SetActive(false);
			}
		}
	}

	public void EndPlayerMoveTurn() {
		StartNextPlayerTurn ();
	}

	public void OnOkButton() {
		CurrentPlayer.glucoseLevel -= popup.SliderVal;
		CurrentPlayer.Nom();
		CurrentPlayer.remainingMoves = 5;

		ShowCardsSelecting ();
	}

	public void StartNextPlayerTurn()
	{
		bool skipped = false;
		do
		{
			skipped = false;
			currentPlayerIndex++;
			if (currentPlayerIndex >= players.Count) {
				currentPlayerIndex = 0;
			}

			if (CurrentPlayer.turnstoskip > 0)
			{
				--CurrentPlayer.turnstoskip;
				skipped = true;
			}
		}
		while (skipped);

		popup.ShowPopup(gameObject);
		popup.Description = "Set Insuline!";
		popup.OkText = "OK";
		popup.OtherText = "";
		popup.slider.gameObject.SetActive(true);
		boarddisabled = true;

		// Start the card selecting phase
		whoseTurnText.text = "Player " + (currentPlayerIndex + 1) + " is aan de beurt";
		remainingMovesText.text = CurrentPlayer.remainingMoves + " zetten over";
		glucoSlider.linkedPlayer = CurrentPlayer;

	}

	public void OnTileClicked(Tile t) {
		if (boarddisabled)
			return;
		if (CurrentPlayer.currentTile.connectingTiles.Contains(t))
		{
			CurrentPlayer.MoveToTile(t);
			remainingMovesText.text = CurrentPlayer.remainingMoves + " zetten over";
		}
	}
}
