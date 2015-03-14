﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	public Tile currentTile;
	
	public int remainingMoves = 5;
	private Game game;

	float playerZ;

	private List<Food> eatenFood = new List<Food>();
	private void EatFood(Food food) {
		eatenFood.Add(food);
	}

	public int glucoseLevel = 0;

	private void Nom() {
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
		}

	}

	// Use this for initialization
	void Start () {
		game = FindObjectOfType<Game> ();
		playerZ = gameObject.transform.position.z;

		EatFood(Food.STUFF);
		EatFood(Food.COLA);


		Nom ();
		Debug.Log(glucoseLevel);
		EatFood (Food.COLA);
		Nom ();
		Debug.Log(glucoseLevel);
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
			if (newTile.food) {
				EatFood(newTile.food);
				Destroy(newTile.food.gameObject);
			}

			game.EndPlayerMoveTurn();
		}
	}
}
