using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class EventService
{
    private static EventService _instance;
    public static EventService Instance
    {
        get
        {
            if(_instance == null)
            {
                _instance = new EventService();
            }
            return _instance;
        }
    }

    public EventController<List<int>> OnSpawnItems { get; private set; }   
    public EventController<int> OnSetAnswer { get; private set; }

    public EventService()
    {
        OnSpawnItems = new EventController<List<int>>();
        OnSetAnswer = new EventController<int>();
    }
}
