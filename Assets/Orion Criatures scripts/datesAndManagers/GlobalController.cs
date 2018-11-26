using System;
using System.Collections.Generic;
using UnityEngine;

public class GlobalController : MonoBehaviour
{

    [SerializeField] private Controlador control = Controlador.N3DS;
    [SerializeField] private AudioSource[] audios;
    [SerializeField] private MusicasDeFundo musica;
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

    public MusicasDeFundo Musica
    {
        get { return musica; }
        private set { musica = value; }
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
        Musica.Start();
        //EventAgregator.AddListener(EventKey.novoHeroiSpawnado, OnNewHeroSpawned);
        EventAgregator.AddListener(EventKey.UiDeOpcoesChange, OnChangeOptionUi);
        EventAgregator.AddListener(EventKey.positiveUiInput, OnPositiveUiInput);
        EventAgregator.AddListener(EventKey.negativeUiInput, OnNegativeUiInput);
        EventAgregator.AddListener(EventKey.fadeOutStart, OnFadeOutStart);
        EventAgregator.AddListener(EventKey.fadeInStart, OnFadeInStart);
        EventAgregator.AddListener(EventKey.encounterEvent, OnEncounterStart);
        EventAgregator.AddListener(EventKey.enemyPresentation, OnEnemyPresentation);
        EventAgregator.AddListener(EventKey.startFight, OnStartFight);
        EventAgregator.AddListener(EventKey.showEndFight, OnShowEndFight);
        EventAgregator.AddListener(EventKey.returnForFreeAfterFight, IniciarMusicaGuardada);
        EventAgregator.AddListener(EventKey.disparaSom, OnRequestStarterSound);
        EventAgregator.AddListener(EventKey.requestMusicWithBackup, OnRequestMusicWithbackup);
        EventAgregator.AddListener(EventKey.requestMusicBackupReturn, IniciarMusicaGuardada);
        EventAgregator.AddListener(EventKey.enterInPause, PararMusica);
        EventAgregator.AddListener(EventKey.stopTheMusic, PararMusica);
        EventAgregator.AddListener(EventKey.exitPause, RetornaMusicaParada);
    }

    private void OnDestroy()
    {
        EventAgregator.RemoveListener(EventKey.UiDeOpcoesChange, OnChangeOptionUi);
        EventAgregator.RemoveListener(EventKey.positiveUiInput, OnPositiveUiInput);
        EventAgregator.RemoveListener(EventKey.negativeUiInput, OnNegativeUiInput);
        EventAgregator.RemoveListener(EventKey.fadeOutStart, OnFadeOutStart);
        EventAgregator.RemoveListener(EventKey.fadeInStart, OnFadeInStart);
        EventAgregator.RemoveListener(EventKey.encounterEvent, OnEncounterStart);
        EventAgregator.RemoveListener(EventKey.enemyPresentation, OnEnemyPresentation);
        EventAgregator.RemoveListener(EventKey.startFight, OnStartFight);
        EventAgregator.RemoveListener(EventKey.showEndFight, OnShowEndFight);
        EventAgregator.RemoveListener(EventKey.returnForFreeAfterFight, IniciarMusicaGuardada);
        EventAgregator.RemoveListener(EventKey.requestMusicWithBackup, OnRequestMusicWithbackup);
        EventAgregator.RemoveListener(EventKey.requestMusicBackupReturn, IniciarMusicaGuardada);
        EventAgregator.RemoveListener(EventKey.disparaSom, OnRequestStarterSound);
        EventAgregator.RemoveListener(EventKey.enterInPause, PararMusica);
        EventAgregator.RemoveListener(EventKey.stopTheMusic, PararMusica);
        EventAgregator.RemoveListener(EventKey.exitPause, RetornaMusicaParada);
    }

    private void RetornaMusicaParada(IGameEvent obj)
    {
        musica.ReiniciarMusicas();
    }

    private void PararMusica(IGameEvent obj)
    {
        musica.PararMusicas();
    }

    private void OnRequestMusicWithbackup(IGameEvent obj)
    {
        StandardSendStringAndFloatEvent s = obj as StandardSendStringAndFloatEvent;
        musica.IniciarMusicaGuardandoAtual(s.MyString, s.MyFloat);
    }

    private void OnRequestStarterSound(IGameEvent obj)
    {
        StandardSendStringEvent s = obj as StandardSendStringEvent;
        DisparaAudio(s.StringContent);
    }

    private void IniciarMusicaGuardada(IGameEvent obj)
    {
        musica.IniciarMusicaGuardada();
    }

    void OnShowEndFight(IGameEvent e)
    {
        DisparaAudio(SoundEffectID.VinhetaDoEncontro.ToString());
    }

    void OnStartFight(IGameEvent e)
    {
        musica.IniciarMusicaGuardandoAtual(NameMusic.Battle8);
    }

    private void OnEnemyPresentation(IGameEvent obj)
    {
        //DisparaAudio("VinhetaDoEncontro");
    }

    private void OnEncounterStart(IGameEvent e)
    {
        musica.PararMusicas();
        DisparaAudio(SoundEffectID.encontro.ToString());
    }

    private void OnFadeOutStart(IGameEvent obj)
    {
        Musica.PararMusicas();
    }

    private void OnFadeInStart(IGameEvent obj)
    {
        Musica.ReiniciarMusicas();
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
            {
                Debug.Log("audio: " + i);
                ativos.Add(audios[i]);
                return audios[i];
            }
        }

        return ativos[0];
    }

    private void Update()
    {
        Musica.Update();
    }
    /*
        public void IniciarMusica(NameMusic n,float volumeAlvo=1)
        {
            musica.IniciarMusica(n,volumeAlvo);
        }

        public void IniciarMusica(AudioClip n, float volumeAlvo = 1)
        {
            musica.IniciarMusica(n, volumeAlvo);
        }

        public void IniciarMusicaDoZero(AudioClip n, float volumeAlvo = 1)
        {
            musica.IniciarMusica(n, volumeAlvo);
            musica.ReiniciarMusicas(true);
        }*/
}
