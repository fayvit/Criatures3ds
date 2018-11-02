using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour {

    [SerializeField] private Controlador control = Controlador.N3DS;
    [SerializeField] private AudioSource[] audios;
    [SerializeField] private FadeViewLoad fadeV;

    private List<AudioSource> ativos = new List<AudioSource>();
    public static GlobalController g;

    public FadeViewLoad FadeV
    {
        get { return fadeV; }
    }

    public Controlador Control
    {
        get { return control; }
    }

    void Awake()
    {
        GlobalController[] g = FindObjectsOfType<GlobalController>();

        if (g.Length > 1)
            Destroy(gameObject);

        transform.parent = null;
        DontDestroyOnLoad(gameObject);

    }

    private void Start()
    {
        GlobalController.g = this;

        //EventAgregator.AddListener(EventKey.novoHeroiSpawnado, OnNewHeroSpawned);
        EventAgregator.AddListener(EventKey.UiDeOpcoesChange, OnChangeOptionUi);
        EventAgregator.AddListener(EventKey.positiveUiInput, OnPositiveUiInput);
        EventAgregator.AddListener(EventKey.negativeUiInput, OnNegativeUiInput);
    }

    private void OnDestroy()
    {
        EventAgregator.RemoveListener(EventKey.UiDeOpcoesChange, OnChangeOptionUi);
        EventAgregator.RemoveListener(EventKey.positiveUiInput, OnPositiveUiInput);
        EventAgregator.RemoveListener(EventKey.negativeUiInput, OnNegativeUiInput);
    }

    void DisparaAudio(string s)
    {
        AudioSource a = RetornaMelhorCandidato();
        a.clip = (AudioClip)Resources.Load(s);
        a.Play();
    }

    void OnNegativeUiInput(IGameEvent e)
    {
        DisparaAudio("Book1");
    }

    void OnPositiveUiInput(IGameEvent e)
    {
        DisparaAudio("Decision2");
    }

    void OnChangeOptionUi(IGameEvent e)
    {
        DisparaAudio("Cursor1");
    }

    void VerificaAudioAtivo()
    {
        for (int i = 0; i < audios.Length; i++)
        {
            if (!audios[i].isPlaying)
            {
                ativos.Remove(audios[i]);
            }
        }
    }

    AudioSource RetornaMelhorCandidato()
    {
        VerificaAudioAtivo();
        for (int i = 0; i < audios.Length; i++)
        {
            if (!ativos.Contains(audios[i]))
                return audios[i];
        }

        return ativos[0];
    }
}
