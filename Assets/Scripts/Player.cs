using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	public Tile currentTile;
	
	public int remainingMoves = 5;
	private Game game;

	public int turnstoskip = 0;

	float playerZ;
	
	public List<Food> eatenFood = new List<Food>();

	public void EatFood(Food food) {
		eatenFood.Add(food);
		game.AddDigestingUI (this, food, eatenFood.Count);
	}

	private float _glucoLevel = 6f;

	public float glucoseLevel {
		get {
			return _glucoLevel;
		}
		set {
			_glucoLevel = value;
		}}

	public void Nom() {
		List<Food> toRemove = new List<Food>();

		foreach(Food food in eatenFood) {
			glucoseLevel += food.Increase;
			food.Duration--;

			if(food.Duration == 0) {
				toRemove.Add(food);
			}
		}
		foreach(Food food in toRemove) {
			eatenFood.Remove(food);
			Destroy(food.gameObject);
		}

	}

	// Use this for initialization
	void Start () {

		game = FindObjectOfType<Game> ();
		playerZ = gameObject.transform.position.z;

		Tile[] tiles = GameObject.FindObjectsOfType<Tile>();
		foreach (Tile tile in tiles)
		{
			if (tile.starttile)
			{
				currentTile = tile;
				break;
			}
		}

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
			// Eat the food on the final tile
			if (newTile.food != null) {
				EatFood(newTile.food);
				newTile.food.gameObject.SetActive(false);
			}

			if (newTile.type == Tile.TileType.EVENT)
			{
				FindObjectOfType<EventManager>().DisplayRandomEvent(this);
			}
			else
			{
				game.EndPlayerMoveTurn();
			}
		}
	}
}
