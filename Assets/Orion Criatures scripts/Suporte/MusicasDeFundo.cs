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

    private const int VELOCIDADE_DE_MUDANCA = 1;

    public void IniciarMusica(NameMusic esseClip,float volumeAlvo = 1)
    {
        IniciarMusica((AudioClip)Resources.Load(esseClip.ToString()),volumeAlvo);
    }

    public void IniciarMusica(AudioClip esseClip, float volumeAlvo = 1)
    {
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


                    if (audios[inicia].volume < 0.9f*volumeAlvo)
                        audios[inicia].volume = Mathf.Lerp(audios[inicia].volume, volumeAlvo, Time.deltaTime * VELOCIDADE_DE_MUDANCA);
                    else
                        audios[inicia].volume = volumeAlvo;

                    if (audios[termina].volume < 0.2f)
                    {
                        audios[termina].volume = 0;
                        audios[termina].Stop();
                    }
                    else
                        audios[termina].volume = Mathf.Lerp(audios[termina].volume, 0, Time.deltaTime * 3 * VELOCIDADE_DE_MUDANCA);

                }
                //VerificaCena();
            }
            else
            {
                if(termina!=-1)
                    audios[termina].volume = Mathf.Lerp(audios[termina].volume, 0, Time.fixedDeltaTime * 2 * VELOCIDADE_DE_MUDANCA);

                if(inicia!=-1)
                    audios[inicia].volume = Mathf.Lerp(audios[inicia].volume, 0, Time.fixedDeltaTime * 2 * VELOCIDADE_DE_MUDANCA);
            }


        }
    }

    void MudaPara(string clip,float volume = 1)
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
    nula=-1,
    Field2,
    Mushrooms
}

