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

		Popup popup = FindObjectOfType<Game>().popup;
		popup.ShowPopup(gameObject);
		popup.slider.gameObject.SetActive(false);
		popup.Description = title + "\n\n" + description.Replace("#", value.ToString());
		if (options.Length == 0)
		{
			popup.OkText = "OK";
			popup.OtherText = "";
		}
		else if (options.Length == 1)
		{
			popup.OkText = options[0];
			popup.OtherText = "";
		}
		else if (options.Length == 2)
		{
			popup.OkText = options[0];
			popup.OtherText = options[1];
		}
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

		FindObjectOfType<Game>().EndPlayerMoveTurn();
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
