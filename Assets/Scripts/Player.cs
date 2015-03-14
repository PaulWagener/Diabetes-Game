using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Player : MonoBehaviour {
	
	public Tile currentTile;

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

		EatFood(Food.STUFF);
		EatFood(Food.COLA);


		Nom ();
		Debug.Log(glucoseLevel);
		EatFood (Food.COLA);
		Nom ();
		Debug.Log(glucoseLevel);

		
		
	}
	
	// Update is called once per frame
	void Update () {
		gameObject.transform.position = Vector3.Slerp (gameObject.transform.position, currentTile.transform.position, 0.1f);

	}
}
