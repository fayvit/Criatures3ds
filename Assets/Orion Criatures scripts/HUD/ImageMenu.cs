using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ImageMenu : FerramentasDeHud
{
    [SerializeField] private GameObject umContainerDeItem;
    [SerializeField] private Text textoDoItem;
    [SerializeField] private RectTransform paiDoContainer;
    [SerializeField] private RectTransform containerDosItens;

    private List<RawImage> raw = new List<RawImage>();
    private List<Text> contadorDeItens = new List<Text>();
    private List<Image> imagensDePainel = new List<Image>();

    private DadosDoPersonagem dados;
    private GameObject dono;
    private LayoutElementQuadrado layQ;
    private TipoHud tipo = TipoHud.items;

    private float posAlvo;
    private float deslocamento = 0;
    private float tempoDeslocando = 0f;
    private bool estouDeslocando = false;
    private int numeroDeElementos;

    private const float TEMPO_PARA_SAIR = 5;
    private const float TEMPO_DE_DESLOCAMENTO = 0.13F;

    private void Awake()
    {
        posAlvo = paiDoContainer.anchorMax.y;
        entrando = false;
    }
    // Use this for initialization
    void Start()
    {
        if (ExistenciaDoController.AgendaExiste(Start, this))
        {
            
            dados = GameController.g.Manager.Dados;
            /*
            if (Dono)
            {
                Camera Cam = Dono.GetComponent<AplicadorDeComandos>().MinhaCamera.GetComponent<Camera>();
                if (Cam)
                    GetComponent<Canvas>().worldCamera = Cam;
            }*/

           // GetComponent<Canvas>().worldCamera = Camera.main;

            switch (tipo)
            {
                case TipoHud.criatures:
                    numeroDeElementos = dados.CriaturesAtivos.Count - 1;
                break;
                case TipoHud.golpes:
                    numeroDeElementos = GameController.g.Manager.CriatureAtivo.MeuCriatureBase.GerenteDeGolpes.meusGolpes.Count;
                break;
                case TipoHud.items:
                    numeroDeElementos = dados.Itens.Count;
                break;
            }

            if (numeroDeElementos > 0)
            {

                for (int i = 0; i < numeroDeElementos; i++)
                {
                    AdicionaUmElementoNoContainer();
                }
            }
            else if (numeroDeElementos == 0)
            {
                umContainerDeItem.SetActive(false);
            }

            //numeroDeElementosAnterior = numeroDeElementos;

            layQ = new LayoutElementQuadrado(containerDosItens.gameObject);

            containerDosItens.anchoredPosition = new Vector2(0, containerDosItens.anchoredPosition.y);

            /*
             Não sei por que o Preinicio carregava com o Canvas.enabled == false
             */
            GetComponent<Canvas>().enabled = true;
        }
    }

    public void AtualizaModificacaoNaHud()
    {
        if (layQ != null)
        {
            layQ.AjustamentoDeLadoFixo(LayoutElementQuadrado.FocoDoAjustamento.altura);

            //AtualizaPosHud(paiDoContainer, escondeHudPara.Yneg, posAlvo, entrando);
            AtualizaPosInternaDaHUD();
            VerificaTempoAtiva(TEMPO_PARA_SAIR);
            AtualizaDadosDeHUD();
        }
    }

    // Update is called once per frame
    void Update()
    {
        AtualizaModificacaoNaHud();
    }

    void AdicionaUmElementoNoContainer()
    {
        GameObject G = (GameObject)Instantiate(umContainerDeItem, containerDosItens.transform);
        G.name = "sou o " + G.transform.GetSiblingIndex();
        G.SetActive(true);      

        raw.Add(G.GetComponentInChildren<RawImage>());
        imagensDePainel.Add(G.GetComponent<Image>());
        contadorDeItens.Add(G.GetComponentInChildren<Text>());

        if(layQ!=null)
            layQ.TamanhoDoVetorDeLayouts++;
    }

    void RetiraUmElementoNoContainer()
    {
        GameObject G = raw[raw.Count - 1].transform.parent.gameObject;
        raw.RemoveAt(raw.Count - 1);
        imagensDePainel.RemoveAt(imagensDePainel.Count - 1);
        contadorDeItens.RemoveAt(contadorDeItens.Count - 1);

        Debug.Log(G);
        Destroy(G);
        layQ.TamanhoDoVetorDeLayouts--;
        
    }

    
    void VerificaNumeroDeElementos()
    {
        if (tipo == TipoHud.items)
        {
            if (numeroDeElementos > dados.Itens.Count)
            {
                if (dados.Itens.Count > 0)
                    RetiraUmElementoNoContainer();
                else
                    umContainerDeItem.SetActive(false);
            }
            else if (numeroDeElementos < dados.Itens.Count)
            {
                if (numeroDeElementos > 0)
                    AdicionaUmElementoNoContainer();
                else
                    umContainerDeItem.SetActive(true);
            }
            numeroDeElementos = dados.Itens.Count;
        }
    }
    
    void AtualizaPosInternaDaHUD()
    {
        if (imagensDePainel.Count > 0)
        {
            int indice = 0;
            switch (tipo)
            {
                case TipoHud.criatures:
                    indice = dados.CriatureSai;
                break;
                case TipoHud.golpes:
                    indice = GameController.g.Manager.CriatureAtivo.MeuCriatureBase.GerenteDeGolpes.golpeEscolhido;
                break;
                case TipoHud.items:
                    indice = dados.itemSai;
                break;
            }

            float tamanhoVisivel = imagensDePainel[indice].rectTransform.sizeDelta.x + layQ.Espaco;
            /*
            print(imagensDePainel[dados.itemSai].rectTransform.anchoredPosition.x + " : " +
                (paiDoContainer.rect)+" : "+tamanhoVisivel+" : "+deslocamento+" : "+
                containerDosItens.anchoredPosition
                );*/

            if (imagensDePainel[indice].rectTransform.anchoredPosition.x + tamanhoVisivel
                >
                paiDoContainer.rect.width + deslocamento * tamanhoVisivel
                &&
                !estouDeslocando
                )
            {
                while (imagensDePainel[indice].rectTransform.anchoredPosition.x + tamanhoVisivel
                >
                paiDoContainer.rect.width + deslocamento * tamanhoVisivel)
                    deslocamento++;

                estouDeslocando = true;
                tempoDeslocando = 0;
            }
            else if (
               imagensDePainel[indice].rectTransform.anchoredPosition.x
               <
               deslocamento * tamanhoVisivel
               && !estouDeslocando
               )
            {
                while (imagensDePainel[indice].rectTransform.anchoredPosition.x
               <
               deslocamento * tamanhoVisivel)
                    deslocamento--;

                estouDeslocando = true;
                tempoDeslocando = 0;
            }

            if (estouDeslocando)
            {
                tempoDeslocando += Time.deltaTime;
                Vector2 alvo = new Vector2(-deslocamento * tamanhoVisivel, containerDosItens.anchoredPosition.y);
                containerDosItens.anchoredPosition = Vector2.Lerp(
                    containerDosItens.anchoredPosition,
                    alvo,
                    tempoDeslocando / TEMPO_DE_DESLOCAMENTO
                    );

                if (Vector2.Distance(containerDosItens.anchoredPosition, alvo) < 0.1f)
                {
                    containerDosItens.anchoredPosition = alvo;
                    estouDeslocando = false;
                }
            }
        }
        
    }

    void AtualizaDadosDeHUD()
    {
        if (contadorDeItens.Count > 0)
        {
            VerificaNumeroDeElementos();


            int indiceEscolhido = -1;
            string nome = "";

            if (dados != null)
            {
                for (int i = 0; i < numeroDeElementos; i++)
                {

                    Texture2D textura = null;
                    contadorDeItens[i].transform.parent.gameObject.SetActive(false);
                    switch (tipo)
                    {
                        case TipoHud.criatures:
                            textura = GameController.g.El.RetornaMini(dados.CriaturesAtivos[i+1].NomeID);
                            if (i == dados.CriatureSai)
                            {
                                indiceEscolhido = i;
                                nome = dados.CriaturesAtivos[i+1].NomeEmLinguas;
                            }
                        break;
                        case TipoHud.golpes:
                            GerenciadorDeGolpes gB = GameController.g.Manager.CriatureAtivo.MeuCriatureBase.GerenteDeGolpes;
                            textura = GameController.g.El.RetornaMini(gB.meusGolpes[i].Nome);
                            if (i == gB.golpeEscolhido)
                            {
                                indiceEscolhido = i;
                                nome = gB.meusGolpes[i].NomeEmLinguas();
                            }
                        break;
                        case TipoHud.items:
                            textura = GameController.g.El.RetornaMini(dados.Itens[i].ID);
                            if (i == dados.itemSai)
                            {
                                nome = MbItens.NomeEmLinguas(dados.Itens[i].ID);
                                indiceEscolhido = i;
                            }
                            contadorDeItens[i].transform.parent.gameObject.SetActive(true);
                            contadorDeItens[i].text = dados.Itens[i].Estoque.ToString();
                        break;
                    }

                    if (numeroDeElementos > i)
                    {
                        raw[i].texture = textura;
                        raw[i].color = new Color(1, 1, 1, 1);

                    }
                    else
                        raw[i].color = new Color(1, 1, 1, 0);

                    if (i == indiceEscolhido)
                    {
                        imagensDePainel[i].sprite = GameController.g.El.uiDestaque;
                    }
                    else
                        imagensDePainel[i].sprite = GameController.g.El.uiDefault;
                }
            }

            textoDoItem.text = nome;
        }
    }

    public void Acionada(TipoHud tipo)
    {
        if (this.tipo != tipo || raw.Count==0)
        {
            Esconde();
            this.tipo = tipo;
            entrando = true;
            Start();
        }

        entrando = true;
        tempoAtiva = 0;
    }

    public override void Esconde()
    {
        entrando = false;
        RemoverItensDoContainer();
    }

    void RemoverItensDoContainer()
    {
        if (raw.Count > 0)
        {
            Transform G = raw[0].transform.parent.parent;

            int childCount = G.childCount;
            for (int i = childCount - 1; i > 0; i--)
                Destroy(G.GetChild(i).gameObject);

            raw = new List<RawImage>();
            imagensDePainel = new List<Image>();
            contadorDeItens = new List<Text>();
            layQ.TamanhoDoVetorDeLayouts = 0;
        }
        /*
        for (int i = 0; i < raw.Count; i++)
        {
            GameObject G = raw[raw.Count - 1].transform.parent.gameObject;
            raw.RemoveAt(raw.Count - 1);
            imagensDePainel.RemoveAt(imagensDePainel.Count - 1);
            contadorDeItens.RemoveAt(contadorDeItens.Count - 1);

            Debug.Log(G);
            Destroy(G);
            layQ.TamanhoDoVetorDeLayouts--;
        }*/
    }

    public void Destruir()
    {
        Destroy(gameObject);
    }
}

public enum TipoHud
{
    nulo=-1,
    items,
    criatures,
    golpes
}