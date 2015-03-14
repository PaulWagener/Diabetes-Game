using UnityEngine;

public class Food : MonoBehaviour {


	public int Duration, Increase;
	public string Name;

	private Food(int increase, int duration) {
		this.Duration = duration;
		this.Increase = increase;
	}
}
