using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalResources : MonoBehaviour {

    public static LocalResources l;
    [Header("Musicas")]
    [SerializeField] private MusicaComVolumeConfig mlocal;
    [SerializeField] private MusicaComVolumeConfig mDaCidade;

    public MusicaComVolumeConfig Mlocal
    {
        get { return mlocal; }
        private set { mlocal = value; }
    }

    public MusicaComVolumeConfig MDaCidade
    {
        get { return mDaCidade; }
        private set { mDaCidade = value; }
    }

    // Use this for initialization
    void Start () {
        l = this;
	}
	
}

[System.Serializable]
public class MusicaComVolumeConfig
{
    [SerializeField] private AudioClip musica;
    [SerializeField] private float volume = 1;

    public AudioClip Musica
    {
        get { return musica; }
        set { musica = value; }
    }

    public float Volume
    {
        get { return volume; }
        set { volume = value; }
    }
}
