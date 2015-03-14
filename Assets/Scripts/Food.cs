using UnityEngine;
using System.Collections;

public class Food : MonoBehaviour {

	public static Food COLA = new Food(5, 3);
	public static Food STUFF = new Food(3, 2);


	public int Duration, Increase;

	private Food(int increase, int duration) {
		this.Duration = duration;
		this.Increase = increase;
	}
}
