using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[System.Serializable]
public class HudManager
{
    [SerializeField] private HudVida hudCriatureAtivo;
    [SerializeField] private HudVida hudInimigo;
    [SerializeField] private ContainerDeAtivos cDeAtivos;
    [SerializeField] private PainelMensCriature painel;
    [SerializeField] private PainelDeAcoes pAction;
    [SerializeField] private MenuBasico menuBasico;
    [SerializeField] private PainelMostrarItem mostrarItem;
    [SerializeField] private DisparaTexto disparaT;
    [SerializeField] private PainelDeConfirmacao confirmacao;
    [SerializeField] private PainelUmaMensagem umaMensagem;
    [SerializeField] private ReplaceCriatureHudManager replaceHudManager;
    [SerializeField] private PainelDeCriature pCriature;
    [SerializeField] private PainelDeGolpe pGolpe;
    [SerializeField] private TryToLearnHud tentandoAprender;
    [SerializeField] private ShopManager shopManager;
    [SerializeField] private PainelQuantidadesParaShop painelQuantidades;
    [SerializeField] private PauseMenu menuDePause;
    [SerializeField] private PainelStatus painelEscolheUsoComCriature;
    [SerializeField] private EnemyAndHeroStatusHudManager statusHud;
    [SerializeField] private ShoulderButtonsN3DS shoulder;

    public ShoulderButtonsN3DS Shoulder
    {
        get { return shoulder; }
    }

    public EnemyAndHeroStatusHudManager StatusHud
    {
        get { return statusHud; }
    }

    public PainelStatus P_EscolheUsoDeItens
    {
        get { return painelEscolheUsoComCriature; }
    }

    public TryToLearnHud H_Tenta
    {
        get { return tentandoAprender; }
    }

    public PainelDeGolpe P_Golpe
    {
        get { return pGolpe; }
    }

    public PainelDeCriature P_Criature
    {
        get { return pCriature; }
    }

    public ReplaceCriatureHudManager EntraCriatures
    {
        get { return replaceHudManager; }
    }

    public PainelUmaMensagem UmaMensagem
    {
        get { return umaMensagem; }
    }

    public PainelDeConfirmacao Confirmacao
    {
        get { return confirmacao; }
    }

    public DisparaTexto DisparaT
    {
        get { return disparaT; }
    }

    public PainelMostrarItem MostrarItem
    {
        get { return mostrarItem; }
    }

    public MenuBasico Menu_Basico
    {
        get { return menuBasico; }
    }

    public PainelDeAcoes P_Action
    {
        get { return pAction; }
    }

    public PainelMensCriature Painel
    {
        get { return painel; }
    }

    public ShopManager Shop_Manager
    {
        get { return shopManager; }
        set { shopManager = value; }
    }

    public PainelQuantidadesParaShop PainelQuantidades
    {
        get { return painelQuantidades; }
    }

    public PauseMenu MenuDePause
    {
        get { return menuDePause; }
    }

    public void ModoLimpo()
    {
        hudInimigo.container.SetActive(false);
        hudCriatureAtivo.container.SetActive(false);
        cDeAtivos.container.gameObject.SetActive(false);
    }

    public void ModoCriature(bool comInimigo)
    {
        cDeAtivos.container.gameObject.SetActive(true);
        if (comInimigo)
        {
            hudInimigo.container.SetActive(true);
            AtualizaDadosDaHudVida(true);
        }


        hudCriatureAtivo.container.SetActive(true);
        cDeAtivos.imgGolpes.transform.parent.gameObject.SetActive(true);
        /*
        cDeAtivos.container.anchoredPosition = new Vector2(
            cDeAtivos.container.anchoredPosition.x,
            cDeAtivos.posComCriature
            );*/

        statusHud.DoHeroi.IniciarHudStatus(GameController.g.Manager.CriatureAtivo.MeuCriatureBase);
        AtualizeImagemDeAtivos();

        AtualizaDadosDaHudVida(false);
    }

    public void ModoHeroi()
    {
        cDeAtivos.container.gameObject.SetActive(true);
        hudCriatureAtivo.container.SetActive(false);
        hudInimigo.container.SetActive(false);
        cDeAtivos.imgGolpes.transform.parent.gameObject.SetActive(false);
        /*
        cDeAtivos.container.anchoredPosition = new Vector2(
            cDeAtivos.container.anchoredPosition.x,
            cDeAtivos.posComHeroi
            );*/

        AtualizeImagemDeAtivos();
    }

    public void AtualizeImagemDeAtivos()
    {
        DadosDoPersonagem dados = GameController.g.Manager.Dados;

        if (cDeAtivos.imgGolpes.transform.parent.gameObject.activeSelf)
        {
            GerenciadorDeGolpes gg = GameController.g.Manager.CriatureAtivo.MeuCriatureBase.GerenteDeGolpes;
            cDeAtivos.imgGolpes.texture = GameController.g.El.RetornaMini(gg.meusGolpes[gg.golpeEscolhido].Nome);
        }

        if (dados.CriaturesAtivos.Count > 1)
        {
            cDeAtivos.imgCriature.transform.parent.gameObject.SetActive(true);
            cDeAtivos.imgCriature.texture = GameController.g.El.RetornaMini(dados.CriaturesAtivos[dados.CriatureSai + 1].NomeID);
        }else
            cDeAtivos.imgCriature.transform.parent.gameObject.SetActive(false);

        if (dados.Itens.Count > 0)
        {
            cDeAtivos.imgItens.transform.parent.gameObject.SetActive(true);
            cDeAtivos.imgItens.texture = GameController.g.El.RetornaMini(dados.Itens[dados.itemSai].ID);
            cDeAtivos.numItens.text = dados.Itens[dados.itemSai].Estoque.ToString();
        }
        else
            cDeAtivos.imgItens.transform.parent.gameObject.SetActive(false);

        cDeAtivos.cristais.text = dados.Cristais.ToString();
    }

    void AtualizaDadosDaHudVida(HudVida essaHud,CriatureBase C)
    {
        Atributos A = C.CaracCriature.meusAtributos;
        essaHud.PV.text = A.PV.Corrente + " \t/\t " + A.PV.Maximo;
        essaHud.PE.text = A.PE.Corrente + " \t/\t " + A.PE.Maximo;
        essaHud.nomeCriature.text = C.NomeID.ToString();
        essaHud.nivel.text = C.G_XP.Nivel.ToString();
        essaHud.barraDeVida.fillAmount = (float)A.PV.Corrente / A.PV.Maximo;
        essaHud.barraDeEnergia.fillAmount = (float)A.PE.Corrente / A.PE.Maximo;
    }

    public void AtualizaDadosDaHudVida(bool inimigo)
    {
        if (inimigo)
        {
            AtualizaDadosDaHudVida(hudInimigo, GameController.g.InimigoAtivo.MeuCriatureBase);
        }
        else
            AtualizaDadosDaHudVida(hudCriatureAtivo, GameController.g.Manager.CriatureAtivo.MeuCriatureBase);
    }
}


[System.Serializable]
public class HudVida
{
    public Text nomeCriature;
    public Text PV;
    public Text PE;
    public Text nivel;
    public Image barraDeVida;
    public Image barraDeEnergia;
    public GameObject container;
}


[System.Serializable]
public class ContainerDeAtivos
{
    public RectTransform container;
    public RawImage imgCriature;
    public RawImage imgItens;
    public RawImage imgGolpes;
    public Text cristais;
    public Text numItens;
    public float posComCriature;
    public float posComHeroi;
}