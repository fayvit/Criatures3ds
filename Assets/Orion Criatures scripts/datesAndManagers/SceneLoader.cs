using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

[System.Serializable]
public class SceneLoader : MonoBehaviour
{

    [SerializeField] private LoadBar loadBar;

    private SaveDates S;
    private AsyncOperation[] a2;
    private FasesDoLoad fase = FasesDoLoad.emEspera;
    private bool podeIr = false;
    private int indiceDoJogo = 0;
    private float tempo = 0;

    private const float tempoMin = 0.25f;

    private enum FasesDoLoad
    {
        emEspera,
        carregando,
        escurecendo,
        clareando,
        eventInProgress
    }

    public void CenaDoCarregamento(int indice)
    {
        ColetorDeLixo.Coleta();

        DontDestroyOnLoad(gameObject);
        indiceDoJogo = indice;

        if (SceneManager.GetSceneByName("comunsDeFase").isLoaded)
        {

            Debug.Log("pelo sim");
            SceneManager.LoadSceneAsync("CenaDeCarregamento", LoadSceneMode.Additive);
            SceneManager.sceneLoaded += IniciarCarregamentoComComuns;
            Camera.main.farClipPlane = 0.4f;
            Time.timeScale = 0;
        }
        else
        {
            Debug.Log("pelo não");
            SceneManager.LoadScene("CenaDeCarregamento");
            SceneManager.sceneLoaded += IniciarCarregamento;
        }
    }

    void IniciarCarregamentoComComuns(Scene cena, LoadSceneMode mode)
    {
        Debug.Log("SCENE EVENT");
        SceneManager.sceneLoaded -= IniciarCarregamentoComComuns;
        loadBar = FindObjectOfType<LoadBar>();
        ComunsCarregado();
    }

    void IniciarCarregamento(Scene cena, LoadSceneMode mode)
    {

        loadBar = FindObjectOfType<LoadBar>();

        SceneManager.LoadSceneAsync("comunsDeFase", LoadSceneMode.Additive);
        SceneManager.sceneLoaded -= IniciarCarregamento;
        SceneManager.sceneLoaded += CarregouComuns;
    }

    void CarregouComuns(Scene cena, LoadSceneMode mode)
    {
        ComunsCarregado();
    }

    void ComunsCarregado()
    {
        if (ExistenciaDoController.AgendaExiste(ComunsCarregado, this))
        {

            SceneManager.sceneLoaded -= CarregouComuns;
            SceneManager.sceneLoaded += SetarCenaPrincipal;
            SceneManager.sceneLoaded += TatudoCarregado;

            Debug.Log("carregou comuns chamado");


            //S = new LoadAndSaveGame().Load(indiceDoJogo);

            if (SaveDatesForJolt.s.SavedGames.Count > indiceDoJogo)
                S = SaveDatesForJolt.s.SavedGames[indiceDoJogo];
            else
                S = null;

            //Debug.Log(S);
            if (S == null)
            {
                fase = FasesDoLoad.carregando;
                aSerCarregado = 1;
                a2 = new AsyncOperation[1];
                a2[0] = SceneManager.LoadSceneAsync("cavernaIntro", LoadSceneMode.Additive);
                //a2[1] = SceneManager.LoadSceneAsync(NomesCenas.katidsVsTempleZone.ToString(), LoadSceneMode.Additive);
            }
            else
            {
                NomesCenas[] N2 = DescarregarCenasDesnecessarias_b(S.VariaveisChave.CenasAtivas.ToArray());



                numCarregador = 0;
                aSerCarregado = N2.Length;
                SceneManager.sceneUnloaded += TatudoDescarregado;

                for (int i = 0; i < N2.Length; i++)
                {
                    SceneManager.UnloadSceneAsync(N2[i].ToString());
                }

                FuncaoCarregadora();
            }


        }
    }

    void FuncaoCarregadora()
    {
        fase = FasesDoLoad.carregando;
        NomesCenas[] N = PegueAsCenasPorCarregar_b(S.VariaveisChave.CenasAtivas.ToArray());


        aSerCarregado = N.Length;

        a2 = new AsyncOperation[N.Length];
        for (int i = 0; i < N.Length; i++)
        {
            a2[i] = SceneManager.LoadSceneAsync(N[i].ToString(), LoadSceneMode.Additive);
            //   Debug.Log(a2[i]+": "+N[i]);
        }



        if (N.Length == 0 || SceneManager.GetSceneByName(GameController.g.MyKeys.CenaAtiva.ToString()).isLoaded)
        {
            SetarCenaPrincipal(SceneManager.GetSceneByName(GameController.g.MyKeys.CenaAtiva.ToString()), LoadSceneMode.Single);
            TatudoCarregado(default(Scene), LoadSceneMode.Single);
        }
        Time.timeScale = 0;
    }

    private void TatudoDescarregado(Scene arg0)
    {
        // numCarregador++;
        //if (numCarregador >= aSerCarregado)
        {
            //aSerCarregado = 0;
            //numCarregador = 0;
            //FuncaoCarregadora();
            ColetorDeLixo.Coleta();
            SceneManager.sceneUnloaded -= TatudoDescarregado;
        }
    }

    int aSerCarregado = 0;
    int numCarregador = 0;

    private void TatudoCarregado(Scene arg0, LoadSceneMode arg1)
    {
        numCarregador++;
        if (aSerCarregado <= numCarregador)
        {
            podeIr = true;
            SceneManager.sceneLoaded -= TatudoCarregado;
        }
    }

    NomesCenas[] PegueAsCenasPorCarregar()
    {
        NomesCenas[] N = S.VariaveisChave.CenasAtivas.ToArray();
        System.Collections.Generic.List<NomesCenas> retorno = new System.Collections.Generic.List<NomesCenas>();
        for (int i = 0; i < N.Length; i++)
        {
            if (!SceneManager.GetSceneByName(N[i].ToString()).isLoaded)
            {
                retorno.Add(N[i]);
            }
        }

        return retorno.ToArray();
    }

    void DescarregarCenasDesnecessarias()
    {

        //System.Collections.Generic.List<NomesCenas> retorno = new System.Collections.Generic.List<NomesCenas>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene S = SceneManager.GetSceneAt(i);

            if (S.isLoaded && S.name != "comunsDeFase" && S.name != "CenaDeCarregamento")
            {

                //  Debug.Log("nomesCenas" + S.name);

                if (S.isLoaded)
                {
                    SceneManager.UnloadSceneAsync(S.name);
                    SceneManager.sceneUnloaded += MySceneUnloaded;
                }
            }
        }
    }

    public static NomesCenas[] PegueAsCenasPorCarregar_b(NomesCenas[] N)
    {

        System.Collections.Generic.List<NomesCenas> retorno = new System.Collections.Generic.List<NomesCenas>();
        for (int i = 0; i < N.Length; i++)
        {

            if (!SceneManager.GetSceneByName(N[i].ToString()).isLoaded)
            {
                retorno.Add(N[i]);
            }
        }

        return retorno.ToArray();
    }

    public static NomesCenas[] DescarregarCenasDesnecessarias_b(NomesCenas[] N)
    {
        System.Collections.Generic.List<NomesCenas> retorno = new System.Collections.Generic.List<NomesCenas>();
        for (int i = 0; i < SceneManager.sceneCount; i++)
        {
            Scene S = SceneManager.GetSceneAt(i);

            if (S.isLoaded && S.name != "comunsDeFase" && S.name != "CenaDeCarregamento" && S.name != "carregamentoEmPasseio")
            {

                //  Debug.Log("nomesCenas" + S.name);

                if (S.isLoaded)
                {
                    bool foi = false;
                    for (int j = 0; j < N.Length; j++)
                    {

                        if (S.name == N[j].ToString())
                            foi = true;

                        //   Debug.Log(S.name + " : " + N[j] + " : " + foi);
                    }

                    if (!foi)
                        retorno.Add(StringParaEnum.ObterEnum<NomesCenas>(S.name));
                }
            }
        }

        return retorno.ToArray();
    }

    private void MySceneUnloaded(Scene arg0)
    {
        ColetorDeLixo.Coleta();
        SceneManager.sceneUnloaded -= MySceneUnloaded;
    }

    void ComoPode()
    {

        if (ExistenciaDoController.AgendaExiste(ComoPode, this))
        {
            //Debug.Log(GameController.g+"  segunda vez");
            CharacterManager manager = GameController.g.Manager;
            manager.eLoad = true;
            AplicadorDeCamera.cam.transform.position = S.Posicao + new Vector3(0, 12, -10);//new Vector3(483, 12f, 745);
            manager.transform.position = S.Posicao;//new Vector3(483,1.2f,755);  
            manager.transform.rotation = S.Rotacao;
            manager.Dados = S.Dados;
            GameController.g.ReiniciarContadorDeEncontro();

            GameObject[] Gs = GameObject.FindGameObjectsWithTag("Criature");

            for (int i = 0; i < Gs.Length; i++)
                MonoBehaviour.Destroy(Gs[i]);

            Debug.Log("me diga se estou no tuto: " + GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto));
            if (GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto))
            {
                //   MonoBehaviour.Destroy(manager.CriatureAtivo.gameObject);
                manager.InserirCriatureEmJogo();
                manager.CriatureAtivo.transform.position = S.Posicao + new Vector3(0, 0, 1);//new Vector3(483, 1.2f, 756);
            }


            manager.Dados.ZeraUltimoUso();
            GameController.g.MyKeys = S.VariaveisChave;
            GameController.g.Salvador.SetarJogoAtual(indiceDoJogo);

            // podeIr = true;

            GameController.g.StartCoroutine(Status());
        }
    }

    IEnumerator Status()
    {
        yield return new WaitForEndOfFrame();
        RecolocadorDeStatus.VerificaStatusDosAtivos();
    }

    void SetarCenaPrincipal(Scene scene, LoadSceneMode mode)
    {
        if (S != null)
        {
            Debug.Log(S.VariaveisChave.CenaAtiva.ToString() + " : " + scene.name);
            if (scene.name == S.VariaveisChave.CenaAtiva.ToString())
            {
                InvocarSetScene(scene);
                SceneManager.sceneLoaded -= SetarCenaPrincipal;

                ComoPode();

                if (scene.name == NomesCenas.cavernaIntro.ToString())
                {
                    //Debug.Log("cavernaInicial");
                }


            }
        }
        else
        if (scene.name != "comunsDeFase")
        {
            //podeIr = true;
            InvocarSetScene(scene);
            SceneManager.sceneLoaded -= SetarCenaPrincipal;


            CharacterManager manager = GameController.g.Manager;

            /*
                novo jogo inicia sem itens e sem criatures
            */
            manager.Dados.CriaturesAtivos = new System.Collections.Generic.List<CriatureBase>();
            manager.Dados.CriaturesArmagedados = new System.Collections.Generic.List<CriatureBase>();
            manager.Dados.Itens = new System.Collections.Generic.List<MbItens>();

            GameController.g.ComCriature = false;
            /***************************/


            AplicadorDeCamera.cam.transform.position = new Vector3(49, 15f, 155); //new Vector3(411, 15f, 1569);
            manager.transform.position = new Vector3(-164f, 15f, 260); /****************new Vector3(39, 5.4f, 155);*/ //new Vector3(519, 5.4f, 1894);
            manager.transform.rotation = Quaternion.LookRotation(Vector3.left);
            GameController.g.ReiniciarContadorDeEncontro();

            if (manager.CriatureAtivo != null)
            {
                Debug.Log("Arquivo de save era nulo");
                MonoBehaviour.Destroy(manager.CriatureAtivo.gameObject);
                manager.InserirCriatureEmJogo();
                manager.CriatureAtivo.transform.position = new Vector3(49, 6, 155); //new Vector3(411, 5.101f, 1560);
            }

            GameController.g.MyKeys.SetarCenasAtivas(new NomesCenas[1] { NomesCenas.cavernaIntro });
            GameController.g.Salvador.SetarJogoAtual(indiceDoJogo);
        }

        GameController.g.Manager.SeletaDeCriatures();
        AplicadorDeCamera.cam.FocarDirecionavel();
    }

    static IEnumerator setarScene(Scene scene)
    {
        yield return new WaitForSeconds(0.5f);
        InvocarSetScene(scene);
    }

    public static void InvocarSetScene(Scene scene)
    {
        //Debug.Log(scene.name);
        SceneManager.SetActiveScene(scene);

        //Debug.Log(GameController.g+" : "+scene.name);
        if (SceneManager.GetActiveScene() != scene)
            GameController.g.StartCoroutine(setarScene(scene));

        //Debug.Log("nomeAtiva: " + SceneManager.GetActiveScene().name);
    }

    public void Update()
    {
        switch (fase)
        {
            case FasesDoLoad.carregando:

                tempo += Time.fixedDeltaTime;

                float progresso = 0;
                if (a2.Length > 0)
                {
                    for (int i = 0; i < a2.Length; i++)
                    {
                        progresso += a2[i].progress;
                        //Debug.Log(progresso+" : "+a2[i].progress + " : " + i + " progresso e indice");
                    }

                    //Debug.Log(a2.Length);
                    progresso /= a2.Length;

                }
                else
                    progresso = 1;


                //Debug.Log(progresso + " : " + (tempo / tempoMin) + " : " + Mathf.Min(progresso, tempo / tempoMin, 1));

                //Debug.Log(loadBar + " : " + progresso + " : " + tempo + " : " + tempoMin);
                loadBar.ValorParaBarra(Mathf.Min(progresso, tempo / tempoMin, 1));

                if (podeIr && tempo >= tempoMin)
                {
                    Debug.Log("ponto x1");
                    GameObject go = GameObject.Find("EventSystem");
                    if (go)
                        SceneManager.MoveGameObjectToScene(go, SceneManager.GetSceneByName("comunsDeFase"));

                    GlobalController.g.FadeV.IniciarFadeOut();
                    EventAgregator.AddListener(EventKey.fadeOutComplete, OnFadeOutComplete);
                    fase = FasesDoLoad.eventInProgress;

                }

                break;
            case FasesDoLoad.escurecendo:
                tempo += Time.fixedDeltaTime;
                if (tempo > 0.95f)
                {
                    /*Só para fade antigo*/
                    GameObject.FindObjectOfType<FadeView>().entrando = false;
                    FindObjectOfType<Canvas>().enabled = false;
                    fase = FasesDoLoad.clareando;
                    SceneManager.SetActiveScene(
                       SceneManager.GetSceneByName(GameController.g.MyKeys.CenaAtiva.ToString()));
                    InformacoesDeCarregamento.FacaModificacoes();
                    GameController.g.Salvador.SalvarAgora();
                    Time.timeScale = 1;
                    SceneManager.UnloadSceneAsync("CenaDeCarregamento");
                    tempo = 0;
                }
                break;
            case FasesDoLoad.clareando:
                tempo += Time.fixedDeltaTime;
                if (tempo > 0.5f)
                {
                    Destroy(gameObject);
                }
                break;
        }
    }

    private void OnFadeOutComplete(IGameEvent obj)
    {
        Camera.main.farClipPlane = 1000;
        GlobalController.g.FadeV.IniciarFadeIn();
        EventAgregator.AddListener(EventKey.fadeInComplete, OnFadeInComplete);

        //FindObjectOfType<Canvas>().enabled = false;
        Debug.Log("nome da cena ativa no save: " + GameController.g.MyKeys.CenaAtiva);
        Debug.Log("nome da cena ativa atual: " + SceneManager.GetActiveScene().name);

        SceneManager.SetActiveScene(
           SceneManager.GetSceneByName(GameController.g.MyKeys.CenaAtiva.ToString()));
        //InformacoesDeCarregamento.FacaModificacoes();
        GameController.g.Salvador.SalvarAgora();
        fase = FasesDoLoad.eventInProgress;

        SceneManager.UnloadSceneAsync("CenaDeCarregamento");

        Time.timeScale = 1;

        GameController.g.ModificacoesDaCena();
    }

    private void OnFadeInComplete(IGameEvent obj)
    {
        Debug.Log("cena ativa para musica: " + SceneManager.GetActiveScene().name);
        // GameController.g.ModificacoesDaCena();
        GameController.g.ContarPassos = true;
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        EventAgregator.RemoveListener(EventKey.fadeInComplete, OnFadeInComplete);
        EventAgregator.RemoveListener(EventKey.fadeOutComplete, OnFadeOutComplete);
    }
}
