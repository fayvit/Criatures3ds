using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MusicasDeFundo:MonoBehaviour
{    
    [SerializeField] private AudioSource[] audios;


    private int inicia = -1;
    private int termina = -1;

    private string cenaIniciada = "";
    private bool parando;

    private const int VELOCIDADE_DE_MUDANCA = 1;

    public void IniciarMusica(AudioClip esseClip)
    {

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

    void Update()
    {
        if (audios.Length > 0)
        {
            if (!parando)
            {
                if (inicia != -1 && termina != -1)
                {
                    if (audios[inicia].volume < 0.9f)
                        audios[inicia].volume = Mathf.Lerp(audios[inicia].volume, 1, Time.deltaTime * VELOCIDADE_DE_MUDANCA);
                    else
                        audios[inicia].volume = 1;

                    if (audios[termina].volume < 0.2f)
                    {
                        audios[termina].Stop();
                    }
                    else
                        audios[termina].volume = Mathf.Lerp(audios[termina].volume, 0, Time.deltaTime * 2 * VELOCIDADE_DE_MUDANCA);

                }
                VerificaCena();
            }
            else
            {
                audios[termina].volume = Mathf.Lerp(audios[termina].volume, 0, Time.fixedDeltaTime * 2 * VELOCIDADE_DE_MUDANCA);
                audios[inicia].volume = Mathf.Lerp(audios[inicia].volume, 0, Time.fixedDeltaTime * 2 * VELOCIDADE_DE_MUDANCA);
            }


        }
    }

    void MudaPara(string clip)
    {
        IniciarMusica((AudioClip)Resources.Load(clip));
        cenaIniciada = SceneManager.GetActiveScene().name;
    }

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
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

