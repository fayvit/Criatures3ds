using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public static class EventAgregator
{
    private static Dictionary<EventKey, List<Action<IGameEvent>>> _eventDictionary 
        = new Dictionary<EventKey, List<Action<IGameEvent>>>();

    public static void AddListener(EventKey key,Action<IGameEvent> callback)
    {
        List<Action<IGameEvent>> callbackList;
        if (!_eventDictionary.TryGetValue(key, out callbackList))
        {
            callbackList = new List<Action<IGameEvent>>();
            _eventDictionary.Add(key, callbackList);
        }

        callbackList.Add(callback);

    }

    public static void RemoveListener(EventKey key, Action<IGameEvent> acao)
    {
        List<Action<IGameEvent>> callbackList;
        if (_eventDictionary.TryGetValue(key, out callbackList))
        {
            callbackList.Remove(acao);
        }
    }

    public static void Publish(EventKey key, IGameEvent umEvento)
    {
        List<Action<IGameEvent>> callbackList;
        if (_eventDictionary.TryGetValue(key, out callbackList))
        {
            //Debug.Log(callbackList.Count+" : "+umEvento.Sender+" : "+key);

            foreach (var e in callbackList)
            {
                if (e != null)
                    e(umEvento);
                else
                    Debug.LogWarning("Event agregator chamou uma função nula na key: "+key+
                        "\r\n Geralmente ocorre quando o objeto do evento foi destruido sem se retirar do listener");
            }
        }
    }

    public static void Publish(IGameEvent e)
    {
        Publish(e.Key, e);
    }

    public static void ClearListeners()
    {
        _eventDictionary = new Dictionary<EventKey, List<Action<IGameEvent>>>();
    }
}

public enum EventKey
{
    nulo = -1,
    fadeInComplete,
    fadeOutComplete,
    UiDeOpcoesChange,
    positiveUiInput,
    negativeUiInput,
    fadeOutStart,
    fadeInStart,
    encounterEvent,
    enemyPresentation,
    startFight,
    showEndFight,
    returnForFreeAfterFight,
    disparaSom
}