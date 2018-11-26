using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MusicasDeFundo
{
    [SerializeField] private AudioSource[] audios;

    private int inicia = -1;
    private int termina = -1;

    private string cenaIniciada = "";
    private bool parando;
    private float volumeAlvo = 0.5f;
    private float velocidadeAtiva = 0.25f;

    private const float VELOCIDADE_DE_MUDANCA = 0.25f;

    public MusicaComVolumeConfig MusicaGuardada { get; private set; }
    public MusicaComVolumeConfig MusicaAtualAtiva { get; private set; }

    public float VelocidadeAtiva
    {
        get { return velocidadeAtiva; }
        set { velocidadeAtiva = value; }
    }

    public void ResetaVelAtiva()
    {
        velocidadeAtiva = VELOCIDADE_DE_MUDANCA;
    }



    public void IniciarMusicaGuardada()
    {
        if (MusicaGuardada != null)
        {
            Debug.Log(MusicaGuardada.Musica + " ué");
            IniciarMusica(MusicaGuardada.Musica, MusicaGuardada.Volume);
        }
    }

    public void IniciarMusicaGuardandoAtual(AudioClip esseClip, float volumeAlvo = 1)
    {
        MusicaGuardada = MusicaAtualAtiva;
        IniciarMusica(esseClip, volumeAlvo);
    }

    public void IniciarMusicaGuardandoAtual(NameMusic esseClip, float volumeAlvo = 1)
    {
        IniciarMusicaGuardandoAtual(esseClip.ToString(), volumeAlvo); ;
    }

    public void IniciarMusicaGuardandoAtual(string esseClip, float volumeAlvo = 1)
    {
        IniciarMusicaGuardandoAtual((AudioClip)Resources.Load(esseClip), volumeAlvo);
    }

    public void IniciarMusica(NameMusic esseClip, float volumeAlvo = 1)
    {
        IniciarMusica((AudioClip)Resources.Load(esseClip.ToString()), volumeAlvo);
    }

    public void IniciarMusica(AudioClip esseClip, float volumeAlvo = 1)
    {

        MusicaAtualAtiva = new MusicaComVolumeConfig()
        {
            Musica = esseClip,
            Volume = volumeAlvo
        };

        parando = false;
        this.volumeAlvo = volumeAlvo;
        AudioSource au = audios[0];

        if (au.isPlaying)
        {
            termina = 0;
            inicia = 1;
        }
        else
        {
            termina = 1;
            inicia = 0;
        }

        if (audios[termina].clip == esseClip)
        {
            int temp = inicia;
            inicia = termina;
            termina = temp;
        }
        else
        {
            audios[inicia].volume = 0;
            audios[inicia].clip = esseClip;
            audios[inicia].Play();
        }
        Debug.Log(parando + " : " + volumeAlvo + " :" + inicia + " : " + termina + " : " + audios[inicia].clip + " : " + audios[termina].clip);
    }

    public void PararMusicas(float vel)
    {
        parando = true;
    }

    public void PararMusicas()
    {
        parando = true;
    }

    public void ReiniciarMusicas(bool doZero = false)
    {
        parando = false;

        if (doZero)
        {
            audios[inicia].Stop();
            audios[inicia].Play();
        }
    }

    public void Update()
    {
        //Debug.Log(audios.Length + " : " + parando);
        if (audios.Length > 0)
        {
            if (!parando)
            {
                if (inicia != -1 && termina != -1)
                {


                    if (audios[inicia].volume < 0.9f * volumeAlvo)
                        audios[inicia].volume = Mathf.Lerp(audios[inicia].volume, volumeAlvo, Time.deltaTime * velocidadeAtiva);
                    else
                        audios[inicia].volume = volumeAlvo;

                    if (audios[termina].volume < 0.2f)
                    {
                        audios[termina].volume = 0;
                        audios[termina].Stop();
                    }
                    else
                        audios[termina].volume = Mathf.Lerp(audios[termina].volume, 0, Time.deltaTime * 3 * velocidadeAtiva);

                }
                //VerificaCena();
            }
            else
            {
                if (termina != -1)
                    audios[termina].volume = Mathf.Lerp(audios[termina].volume, 0, Time.fixedDeltaTime * 2 * velocidadeAtiva);

                if (inicia != -1)
                    audios[inicia].volume = Mathf.Lerp(audios[inicia].volume, 0, Time.fixedDeltaTime * 2 * velocidadeAtiva);
            }


        }
    }

    void MudaPara(string clip, float volume = 1)
    {
        volumeAlvo = volume;
        IniciarMusica((AudioClip)Resources.Load(clip));
        cenaIniciada = SceneManager.GetActiveScene().name;
    }

    public void Start()
    {
        if (SceneManager.GetActiveScene().name == "Inicial 1")
            IniciarMusica((AudioClip)Resources.Load(NameMusic.Field2.ToString()));
    }

    void VerificaCena()
    {
        if (cenaIniciada != SceneManager.GetActiveScene().name)
            switch (SceneManager.GetActiveScene().name)
            {
                case "Inicial":
                case "Inicial 1":
                    MudaPara("Kevin_Hartnell_-_09_-_Podcast_Theme");
                    break;
                case "cavernaIntro":
                    MudaPara("Lobo_Loco_-_03_-_Frisco_Traffic_ID_762");
                    break;
                case "equipamentos":
                case "equipamentos_plus":
                case "Tutorial":
                    //   MudaPara(equips);
                    break;
            }
    }
}

public enum NameMusic
{
    nula = -1,
    Field2,
    Mushrooms,
    Battle8
}

public enum SoundEffectID
{
    tuin_1ponto3,
    tuimParaNivel,
    XP_Heal01,
    coisaBoaRebot,
    XP_Swing03,
    rajadaDeAgua,
    Book1,
    paraBau,
    Collapse1,
    chamadaParaAcao,
    XP_Knock04,
    XP_Knock01,
    bemFeito,
    Decision1,
    XP_Heal02,
    Item,
    encontro,
    VinhetaDoEncontro
}

