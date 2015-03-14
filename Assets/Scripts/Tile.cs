using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Tile : MonoBehaviour {

	public List<Tile> connectingTiles;

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
		if (game.currentPlayer.currentTile.connectingTiles.Contains(this))
			game.currentPlayer.currentTile = this;
	}

	void OnDrawGizmos()
	{
		foreach (Tile adjtile in connectingTiles)
		{
			Gizmos.DrawLine(transform.position, adjtile.transform.position);
		}
	}
}
