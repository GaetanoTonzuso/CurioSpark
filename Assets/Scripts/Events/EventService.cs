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

    //Mathematic Game Events
    public EventController<List<int>> OnSpawnItems { get; private set; }   
    public EventController<int> OnSetAnswer { get; private set; }
    public EventController<string> OnUpdateFeedback { get; private set; }
    public EventController OnUpdateScore { get; private set; }
    public EventController <int> OnUpdateScoreUI { get; private set; }
    public EventController OnGenerateNewQuestion { get; private set; }
    public EventController <int> OnEndGame{ get; private set; }
    public EventController OnPauseGame { get; private set; }
    public EventController OnUnpauseGame { get; private set; }

    //Tile Game
    public EventController<TileItem> OnUncoverItem {  get; private set; }
    public EventController OnItemCover {  get; private set; }
    public EventController OnItemsMatch { get; private set; }
    public EventController OnGameOver { get; private set; }


    public EventService()
    {
        //Mathematic Game Events
        OnSpawnItems = new EventController<List<int>>();
        OnSetAnswer = new EventController<int>();
        OnUpdateFeedback = new EventController<string>();
        OnUpdateScore = new EventController();
        OnUpdateScoreUI = new EventController<int>();
        OnGenerateNewQuestion = new EventController();
        OnEndGame = new EventController<int>();
        OnPauseGame = new EventController();
        OnUnpauseGame = new EventController();

        //Match Tile Game Events
        OnUncoverItem = new EventController<TileItem>();
        OnItemCover = new EventController();
        OnItemsMatch = new EventController();
        OnGameOver = new EventController();
    }
}
