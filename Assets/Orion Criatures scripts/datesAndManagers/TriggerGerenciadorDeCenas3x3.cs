using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerGerenciadorDeCenas3x3 : MonoBehaviour {

    [SerializeField]private string[] cenasLigadasString;
    [SerializeField]private  NomesCenas cenaAtivaNoDesligar = NomesCenas.cavernaIntro;

    private NomesCenas[] cenasLigadas;
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
                
            }
        }
    }

    private void SetarCenaAtiva(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0 == SceneManager.GetSceneByName(cenaAtivaNoDesligar.ToString()))
        {
            TriggerGerenciadorDeCena.InvocarSetScene(arg0);
        }
    }
}
