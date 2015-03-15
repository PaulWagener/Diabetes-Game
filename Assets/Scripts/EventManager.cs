using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
	Object[] eventobjects;
	List<Event> events;

	// Use this for initialization
	void Start()
	{
		events = new List<Event>();
		eventobjects = Resources.LoadAll("Events");
		foreach (Object o in eventobjects)
		{
			if (o as GameObject && (o as GameObject).GetComponent<Event>())
				events.Add((o as GameObject).GetComponent<Event>());
		}
	}

	public void DisplayRandomEvent(Player player)
	{
		Debug.Log(Random.Range(0, events.Count));
		events[Random.Range(0, events.Count)].Execute(player);
	}
}
