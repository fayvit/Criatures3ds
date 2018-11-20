using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerGerenciadorDeCenas3x3 : MonoBehaviour {

    [SerializeField] private string[] cenasLigadasString;
    [SerializeField] private NomesCenas cenaAtivaNoDesligar = NomesCenas.cavernaIntro;
    [SerializeField] private MusicaLocal musica = MusicaLocal.daRegiao;

    private NomesCenas[] cenasLigadas;
    private bool permitirLimpeza = false;

    private enum MusicaLocal
    {
        daCidade,
        daRegiao
    }
    // Use this for initialization
    void Start () {
        cenasLigadas = new NomesCenas[cenasLigadasString.Length];

        for (int i = 0; i < cenasLigadas.Length; i++)
        {
            cenasLigadas[i] = StringParaEnum.ObterEnum<NomesCenas>(cenasLigadasString[i]);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Criature" && !GameController.g.estaEmLuta)
        {
            GameController.g.Manager.AoHeroi();
        }

        if (col.gameObject.tag == "Player")
        {
            permitirLimpeza = true;
            NomesCenas[] N = SceneLoader.PegueAsCenasPorCarregar_b(cenasLigadas);
            NomesCenas[] N2 = SceneLoader.DescarregarCenasDesnecessarias_b(cenasLigadas);
            //a2 = new AsyncOperation[N.Length];
            for (int i = 0; i < N.Length; i++)
            {
                SceneManager.LoadSceneAsync(N[i].ToString(), LoadSceneMode.Additive);
                SceneManager.sceneLoaded += SetarCenaAtiva;
            }

            for (int i = N.Length; i < N.Length + N2.Length; i++)
            {
                SceneManager.UnloadSceneAsync(N2[i - N.Length].ToString());
                SceneManager.sceneUnloaded += OnSceneUnloaded;
                
            }

            if (SceneManager.GetActiveScene().name == cenaAtivaNoDesligar.ToString())
                SetarMusica();

            //ColetorDeLixo.Coleta();

        }
    }

    private void OnSceneUnloaded(Scene arg0)
    {
        SceneManager.sceneUnloaded += OnSceneUnloaded;

        Invoke("LimpaLixo", 1.5f);
    }

    void LimpaLixo()
    {
        if (permitirLimpeza)
        {
            ColetorDeLixo.Coleta();
            permitirLimpeza = false;
        }
    }

    void SetarMusica()
    {
         MusicaComVolumeConfig mvc = null;
            switch (musica)
            {
                case MusicaLocal.daRegiao:
                    mvc = LocalResources.l.Mlocal;
                break;
                case MusicaLocal.daCidade:
                    mvc = LocalResources.l.MDaCidade;
                break;
            }

        if (mvc != null)
        {
           
            GlobalController.g.Musica.IniciarMusicaGuardandoAtual(mvc.Musica,mvc.Volume);
            GlobalController.g.Musica.ReiniciarMusicas(true);
            
        }

        ColetorDeLixo.Coleta();
    }

    private void SetarCenaAtiva(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0 == SceneManager.GetSceneByName(cenaAtivaNoDesligar.ToString()))
        {
            ColetorDeLixo.Coleta();
            TriggerGerenciadorDeCena.InvocarSetScene(arg0);
        }
    }
}
