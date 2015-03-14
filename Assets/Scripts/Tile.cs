using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	public List<Tile> connectingTiles;

	public Food food; 

	private Game game;

	// Use this for initialization
	void Start () {
		game = FindObjectOfType<Game> ();
	}
	
	// Update is called once per frame
	void Update () {

	}
	
	void OnMouseDown()
	{
		game.OnTileClicked (this);
	}

	void OnDrawGizmos()
	{
		foreach (Tile adjtile in connectingTiles)
		{
			Gizmos.DrawLine(transform.position, adjtile.transform.position);
		}
	}
}
