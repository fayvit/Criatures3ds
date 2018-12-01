using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class TriggerGerenciadorDeCena_b : MonoBehaviour
{
    [Header("A cena ativa será a de indice zero")]
    public NomesCenas[] cenasAlvo;

    private AsyncOperation[] ao;
    private LoadBar loadBar;
    private EstadoLocal estado = EstadoLocal.emEspera;

    private enum EstadoLocal
    {
        emEspera,
        carregamentoIniciado
    }

    private void Update()
    {
        switch (estado)
        {
            case EstadoLocal.carregamentoIniciado:
                float progresso = 0;
                if (ao.Length > 0)
                {
                    for (int i = 0; i < ao.Length; i++)
                    {
                        progresso += ao[i].progress;

                    }


                    progresso /= ao.Length;

                    loadBar.ValorParaBarra(Mathf.Min(progresso, 1));

                }
                else
                    progresso = 1;

                if (progresso >= 1)
                {
                    SceneLoader.InvocarSetScene(
                        SceneManager.GetSceneByName(cenasAlvo[0].ToString())
                        );


                    estado = EstadoLocal.emEspera;

                    SceneManager.UnloadSceneAsync("carregamentoEmPasseio");
                    SceneManager.sceneUnloaded += OnUnloadBar;
                    GameController.g.ModificacoesDaCena();
                }

                break;
        }
    }

    private void OnUnloadBar(Scene arg0)
    {
        Debug.Log("passei Aqui");

        Time.timeScale = 1;
        SceneManager.sceneUnloaded -= OnUnloadBar;
    }

    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Criature" && !GameController.g.estaEmLuta)
        {
            GameController.g.Manager.AoHeroi();
        }

        if (col.gameObject.tag == "Player" && !GameController.g.estaEmLuta)
        {
            if (ExistemCenasParaCarregar())
            {
                Time.timeScale = 0;
                SceneManager.LoadSceneAsync("carregamentoEmPasseio", LoadSceneMode.Additive);
                SceneManager.sceneLoaded += OnLoadBar;
            }
            else
            {

            }
        }
    }

    private void OnLoadBar(Scene arg0, LoadSceneMode arg1)
    {
        SceneManager.sceneLoaded -= OnLoadBar;
        NomesCenas[] N = SceneLoader.PegueAsCenasPorCarregar_b(cenasAlvo);
        NomesCenas[] N2 = SceneLoader.DescarregarCenasDesnecessarias_b(cenasAlvo);

        for (int i = 0; i < N2.Length; i++)
        {
            SceneManager.UnloadSceneAsync(N2[i].ToString());
        }

        ao = new AsyncOperation[N.Length];

        for (int i = 0; i < N.Length; i++)
        {
            ao[i] = SceneManager.LoadSceneAsync(N[i].ToString(), LoadSceneMode.Additive);
            // SceneManager.sceneLoaded -= OnLoadTargetScene;
        }

        loadBar = FindObjectOfType<LoadBar>();
        estado = EstadoLocal.carregamentoIniciado;
    }

    /*
    private void OnLoadTargetScene(Scene arg0, LoadSceneMode arg1)
    {
        
    }*/

    private bool ExistemCenasParaCarregar()
    {
        NomesCenas[] N = SceneLoader.PegueAsCenasPorCarregar_b(cenasAlvo);
        if (N.Length > 0)
            return true;

        return false;
    }
}