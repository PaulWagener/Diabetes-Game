using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(Food))]
public class FoodEditor : Editor
{
	const float TileDistance = 2.5f;

	public override void OnInspectorGUI()
	{
		DrawDefaultInspector();

		Food food = target as Food;

		if (GUILayout.Button("Align to tile"))
		{
			float minDistance = float.MaxValue;
			Tile minTile = null;

			foreach (Tile t in FindObjectsOfType<Tile>()) {
				// Remove from old tiles
				if(t.food == food) {
					t.food = null;
				}

				// Find closest tile
				float distance = Vector3.Distance(t.transform.position, food.transform.position);
				if(distance < minDistance) {
					minDistance = distance;
					minTile = t;
				}
			}
			minTile.food = food;
			food.transform.position = minTile.transform.position;
		}
	}
}
