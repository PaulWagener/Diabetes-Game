﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class Player : MonoBehaviour {
	
	public Tile currentTile;
	
	public int remainingMoves = 5;
	private Game game;

	float playerZ;

	private List<Food> eatenFood = new List<Food>();
	private void EatFood(Food food) {
		eatenFood.Add(food);
	}

	private int _glucoLevel = 0;
	private Slider glucoSlider;

	public int glucoseLevel {
		get {
			return _glucoLevel;
		}
		set {
			glucoSlider.value = value;
			_glucoLevel = value;
		}}

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
		glucoSlider = FindObjectOfType<Slider>();
		playerZ = gameObject.transform.position.z;

		EatFood(Food.STUFF);
		EatFood(Food.COLA);

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
				Destroy(newTile.food.gameObject);
			}

			game.EndPlayerMoveTurn();
		}
	}
}
