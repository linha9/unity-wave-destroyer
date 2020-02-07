using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public enum EventType
{
    EnemyDeath, WaveComplete
}

public class EventHandler : MonoBehaviour
{
    public static EventHandler Instance { get; private set; }

    private Dictionary<EventType, UnityEvent> eventRegistry;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            eventRegistry = new Dictionary<EventType, UnityEvent>();
        }
        else
        {
            Destroy(this);
        }
    }

    public void AddListener(EventType eName, UnityAction action)
    {
        if (!eventRegistry.ContainsKey(eName))
        {
            eventRegistry.Add(eName, new UnityEvent());
        }

        eventRegistry[eName].AddListener(action);
    }

    public void DeleteListener(EventType eName, UnityAction action)
    {
        if (eventRegistry.ContainsKey(eName))
        {
            eventRegistry[eName].RemoveListener(action);
        }
    }

    public void Invoke(EventType eName)
    {
        if (eventRegistry.ContainsKey(eName))
        {
            eventRegistry[eName].Invoke();
        }
    }
}