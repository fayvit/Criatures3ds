using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeAfterFightEvent : IGameEvent
{
    private GameObject G;
    
    public AudioClip MusicaDeRetorno { get; private set; }
    public EventKey Key { get; private set; }

    public GameObject Sender
    {
        get
        {
            return G;
        }
    }

    

    public FreeAfterFightEvent(GameObject G,AudioClip musicaDeRetorno)
    {
        this.G = G;
        Key = EventKey.returnForFreeAfterFight;
        MusicaDeRetorno = musicaDeRetorno;
    }
}
