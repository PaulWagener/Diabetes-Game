using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventManager : MonoBehaviour
{
	List<Event> events;

	// Use this for initialization
	void Start()
	{
		events = new List<Event>();
		Object[] eventobjects = Resources.LoadAll("Events");
		foreach (Object o in eventobjects)
		{
			if (o as GameObject && (o as GameObject).GetComponent<Event>())
				events.Add((o as GameObject).GetComponent<Event>());
		}
	}

	public void DisplayRandomEvent()
	{
		Event e = events[Random.Range(0, events.Count)];
	}
}
