using UnityEngine;

public class Food : MonoBehaviour {


	public int Duration;
	public float Increase;
	public string Name;

	public Sprite sprite;

	private Food(int increase, int duration) {
		this.Duration = duration;
		this.Increase = increase;
	}
}
