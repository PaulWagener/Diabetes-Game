using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Tile))]
public class TileEditor : Editor
{
	const float TileDistance = 2.5f;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Tile tile = target as Tile;

		if (GUILayout.Button("Align to grid"))
		{
			float x = tile.transform.position.x;
			float y = tile.transform.position.y;
			float z = tile.transform.position.z;

			int ty = Mathf.RoundToInt(y / (Mathf.Sin(Mathf.PI / 3.0f) * TileDistance));
			x -= 0.5f * TileDistance * (ty % 2);
			int tx = Mathf.RoundToInt(x / TileDistance);

			tile.transform.position = new Vector3((tx + 0.5f * (ty % 2)) * TileDistance, ty * (Mathf.Sin(Mathf.PI / 3.0f) * TileDistance), z);
		}

		if (GUILayout.Button("Add nearby adjacency"))
		{
			Tile[] tiles = GameObject.FindObjectsOfType<Tile>();
			foreach (Tile adjtile in tiles)
			{
				if (adjtile != tile && Vector3.Distance(adjtile.transform.position, tile.transform.position) < TileDistance * 1.5f)
				{
					if (!tile.connectingTiles.Contains(adjtile))
						tile.connectingTiles.Add(adjtile);
				}
			}
			EditorUtility.SetDirty(tile);
		}

		if (GUILayout.Button("Add all nearby adjacency"))
		{
			Tile[] tiles = GameObject.FindObjectsOfType<Tile>();
			foreach (Tile tile2 in tiles)
			{
				foreach (Tile adjtile in tiles)
				{
					if (adjtile != tile2 && Vector3.Distance(adjtile.transform.position, tile2.transform.position) < TileDistance * 1.5f)
					{
						if (!tile2.connectingTiles.Contains(adjtile))
							tile2.connectingTiles.Add(adjtile);
					}
					EditorUtility.SetDirty(tile2);
				}
			}
		}

		if (GUILayout.Button("Set as start tile") && !tile.starttile)
		{
			Tile[] tiles = GameObject.FindObjectsOfType<Tile>();
			foreach (Tile tile2 in tiles)
			{
				if (tile2.starttile)
				{
					tile2.starttile = false;
					EditorUtility.SetDirty(tile2);
				}
			}
			tile.starttile = true;
			EditorUtility.SetDirty(tile);
		}

		if (tile.starttile)
			GUILayout.Label("This is the starting tile");

		
		// Make sure that tiles have the correct sprite associated with them
		// TODO: the below method is supremely inefficient
		Game game = FindObjectOfType<Game> ();
		foreach (Tile t in GameObject.FindObjectsOfType<Tile>()) {
			Sprite sprite = null;
			switch(t.type) {
			case Tile.TileType.NORMAL:
				sprite = game.NORMAL_SPRITE;
				break;

			case Tile.TileType.EVENT:
				sprite = game.EVENT_SPRITE;
				break;

			case Tile.TileType.HOSPITAL:
				sprite = game.HOSPITAL_SPRITE;
				break;

			case Tile.TileType.PHARMACY:
				sprite = game.PHARMACY_SPRITE;
				break;
			}
			t.GetComponentInChildren<SpriteRenderer>().sprite = sprite;
		}
	}
}
