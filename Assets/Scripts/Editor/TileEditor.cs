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
	}
}
