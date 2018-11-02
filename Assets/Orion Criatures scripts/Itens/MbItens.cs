using UnityEngine;
using System.Collections;

[System.Serializable]

public class MbItens:System.ICloneable
{
    [SerializeField]private nomeIDitem nomeID;
    [SerializeField]private bool usavel;
    [SerializeField]private int acumulavel;
    [SerializeField]private int estoque;
    [SerializeField]private int valor;

    //private GameObject gAlvoDoItem;
    [System.NonSerialized] private CharacterManager manager;
    [System.NonSerialized] private AnimaBraco animaB;
    [System.NonSerialized] private FluxoDeRetorno fluxo;
    [System.NonSerialized] private EstadoDeUsoDeItem estado = EstadoDeUsoDeItem.nulo;

    [System.NonSerialized] private float tempoDecorrido = 0;

    public const float INTERVALO_DO_USO_DE_ITEM = 15;

    public MbItens(ContainerDeCaracteristicasDeItem cont)
    {
        this.nomeID = cont.NomeID;
        this.usavel = cont.consumivel;
        this.acumulavel = cont.acumulavel;
        this.estoque = cont.estoque;
        this.valor = cont.valor;
    }

    public nomeIDitem ID
    {
        get { return nomeID; }
    }

    public bool Consumivel
    {
        get { return usavel; }
    }

    public int Acumulavel
    {
        get { return acumulavel; }
    }

    public int Estoque
    {
        get { return estoque; }
        set { estoque = value; }
    }

    public int Valor
    {
        get { return valor; }
    }

    public EstadoDeUsoDeItem Estado
    {
        get { return estado; }
        set { estado = value; }
    }

    public float TempoDecorrido
    {
        get { return tempoDecorrido; }
        set { tempoDecorrido = value; }
    }

    public CharacterManager Manager
    {
        get { return manager; }
        set { manager = value; }
    }

    public AnimaBraco AnimaB
    {
        get { return animaB; }
        set { animaB = value; }
    }

    public virtual void IniciaUsoDeMenu(GameObject dono)
    {
        throw new System.NotImplementedException();
    }

    public virtual bool AtualizaUsoDeMenu()
    {
        throw new System.NotImplementedException();
        //return false;
    }

    public virtual void IniciaUsoComCriature(GameObject dono)
    {
        throw new System.NotImplementedException();
    }

    public virtual bool AtualizaUsoComCriature()
    {
        throw new System.NotImplementedException();
    }

    public virtual void IniciaUsoDeHeroi(GameObject dono)
    {
        throw new System.NotImplementedException();
    }

    public virtual bool AtualizaUsoDeHeroi()
    {
        throw new System.NotImplementedException();
    }

    public static bool RetirarUmItem(
        CharacterManager gerente,
        MbItens nomeItem,
        int quantidade = 1,
        FluxoDeRetorno fluxo = FluxoDeRetorno.heroi)
    {
        bool retorno = true;

        for (int i = 0; i < quantidade; i++)
        {
            retorno &= RetirarUmItem(ProcuraItemNaLista(nomeItem.ID),gerente,fluxo);
            
        }

        return retorno;
    }

    public static bool RetirarUmItem(
        CharacterManager gerente,
        nomeIDitem nomeItem,
        int quantidade = 1,
        FluxoDeRetorno fluxo = FluxoDeRetorno.heroi)
    {
        bool retorno = true;

        for (int i = 0; i < quantidade; i++)
        {
            retorno &= RetirarUmItem(ProcuraItemNaLista(nomeItem), gerente, fluxo);

        }

        return retorno;
    }

    static MbItens ProcuraItemNaLista(nomeIDitem nome)
    {
        MbItens retorno = new MbItens(new ContainerDeCaracteristicasDeItem());
        for (int i = GameController.g.Manager.Dados.Itens.Count - 1; i > -1;i--)
        {
            if (GameController.g.Manager.Dados.Itens[i].ID == nome)
                retorno = GameController.g.Manager.Dados.Itens[i];
        }
        return retorno;

    }

    public static bool RetirarUmItem(
        MbItens nomeItem,
        CharacterManager gerente,         
        FluxoDeRetorno fluxo = FluxoDeRetorno.heroi)
    {
        int indice = gerente.Dados.Itens.IndexOf(nomeItem);
        if (indice > -1)
            if (gerente.Dados.Itens[indice].Estoque >= 1)
            {
                gerente.Dados.Itens[indice].Estoque--;
                GameController g = GameController.g;
                if(g.UsarTempoDeItem==UsarTempoDeItem.sempre ||(g.UsarTempoDeItem==UsarTempoDeItem.emLuta && g.estaEmLuta))
                    gerente.Dados.TempoDoUltimoUsoDeItem = Time.time;

                Debug.Log("remove ai vai");
                if (gerente.Dados.Itens[indice].Estoque == 0)
                {
                    Debug.Log("Tira daí");
                    g.FinalizaHuds();
                    gerente.Dados.Itens.Remove(gerente.Dados.Itens[indice]);

                    if (gerente.Dados.itemSai > gerente.Dados.Itens.Count-1)
                        gerente.Dados.itemSai = 0;

                    if (fluxo == FluxoDeRetorno.menuCriature || fluxo == FluxoDeRetorno.menuHeroi)
                    {
                        GameController.g.StartCoroutine(VoltarDosItensQuandoNaoTemMais());
                    }
                }
                return true;
            }

        return false;
    }

    protected void InicializacaoComum(GameObject dono,Transform alvoDoItem)
    {
        Manager = GameController.g.Manager;
        TempoDecorrido = 0;

        Manager.Estado = EstadoDePersonagem.parado;
        Manager.CriatureAtivo.PararCriatureNoLocal();
        //Manager.Mov.Animador.PararAnimacao();

        if (GameController.g.estaEmLuta)
            GameController.g.InimigoAtivo.PararCriatureNoLocal();

        AnimaB = new AnimaBraco(Manager.transform, alvoDoItem);

    }

    protected void FecharMensagem()
    {
        Estado = EstadoDeUsoDeItem.finalizaUsaItem;
        GameController.g.HudM.MenuDePause.ReligarBotoesDoPainelDeItens();
    }

    static IEnumerator VoltarDosItensQuandoNaoTemMais()
    {
        yield return new WaitForSecondsRealtime(1f);
        //GameController.g.HudM.P_EscolheUsoDeItens.VoltarDosItens();
    }

    public static string NomeEmLinguas(nomeIDitem ID)
    {
        return BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.listaDeItens)[(int)ID];
    }

    public object Clone()
    {
        return PegaUmItem.Retorna(nomeID, estoque);
    }
}

public struct ContainerDeCaracteristicasDeItem
{
    public nomeIDitem NomeID;
    public bool consumivel;
    public int acumulavel;
    public int estoque;
    public int valor;

    /// <summary>
    /// Ao construir com o parametro nome, os campos recebem valores padrões
    /// </summary>
    /// <param name="nome">Nome do item a ser construido</param>
    public ContainerDeCaracteristicasDeItem(nomeIDitem nome)
    {
        this.NomeID = nome;
        this.consumivel = true;
        this.acumulavel = 99;
        this.estoque = 1;
        this.valor = 1;
    }
}

public enum EstadoDeUsoDeItem
{
    nulo = -1,
    animandoBraco,
    aplicandoItem,
    selecaoDeItem,
    finalizaUsaItem,
    updateParticular,
    emEspera
}

public enum nomeIDitem
{
    generico = -1,
    maca,
    burguer,
    cartaLuva,
    gasolina,
    aguaTonica,
    regador,
    estrela,
    quartzo,
    adubo,
    seiva,
    inseticida,
    aura,
    repolhoComOvo,
    ventilador,
    pilha,
    geloSeco,
    pergaminhoDeFuga,
    segredo,
    estatuaMisteriosa,
    cristais,
    pergaminhoDePerfeicao,
    antidoto,
    amuletoDaCoragem,
    tonico,
    pergDeRajadaDeAgua,
    pergSaida,
    condecoracaoAlpha,
    pergArmagedom,
    pergSabre,
    pergGosmaDeInseto,
    pergGosmaAcida,
    pergMultiplicar,
    pergVentania,
    pergVentosCortantes,
    pergOlharEnfraquecedor,
    pergOlharMal,
    condecoracaoBeta,
    pergFuracaoDeFolhas,
    explosivos,
    medalhaoDasAguas,
    tinteiroSagradoDeLog,
    pergaminhoDeLaurense,
    pergaminhoDeBoutjoi,
    pergaminhoDeAnanda,
    canetaSagradaDeLog,
    pergSinara,
    pergAlana
}