using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StandardSendStringAndFloatEvent : IGameEvent
{
    [System.NonSerialized] private GameObject _sender;

    public GameObject Sender
    {
        get { return _sender; }
    }

    public EventKey Key { get; private set; }

    public string MyString { get; private set; }

    public float MyFloat { get; private set; }

    public StandardSendStringAndFloatEvent(GameObject sender, float myFloat, string myString, EventKey key)
    {
        _sender = sender;
        Key = key;
        MyFloat = myFloat;
        MyString = myString;
    }
}