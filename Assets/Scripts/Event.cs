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
	public float value;

	Player player;

	public enum EventAction
	{
		Nothing,
		Wolf,
		GrandmaPie,
	};
	public EventAction action;

	public void Execute(Player player)
	{
		this.player = player;

		//TODO: display dialog
		string descr = description.Replace("#", value.ToString());
	}

	public void ApplyEffect(int button)
	{
		switch (action)
		{
			case EventAction.Wolf:
				player.glucoseLevel += value;
				break;
			case EventAction.GrandmaPie:
				if (button == 0)
				{
					player.glucoseLevel += value;
				}
				else
				{
					//TODO: wait for one turn
				}
				break;
		}
	}

	public void OnOkButton()
	{
		ApplyEffect(0);
	}

	public void OnOtherButton()
	{
		ApplyEffect(1);
	}
}
