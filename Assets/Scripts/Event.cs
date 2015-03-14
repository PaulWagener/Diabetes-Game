using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Event : MonoBehaviour
{
	public string title;
	[Multiline]
	public string description;
	public Image image;
	public string[] options;

	public enum EventAction
	{
		Nothing,
		Wolf
	};
	public EventAction action;

	public void Execute(Player player)
	{
		switch (action)
		{
			case EventAction.Wolf:
				player.glucoseLevel += 2;
				break;
		}
	}
}
