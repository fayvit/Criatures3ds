using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ItemModificadorDeAtributoBase : ItemConsumivelBase
{
    private int opcaoEscolhida = -1;
    //    private FluxoDeRetorno fluxo;
    private string[] textoDaMensInicial;//ChaveDeTexto chave = ChaveDeTexto.usarPergaminhoDeLaurense;
    private DoJogo particula = DoJogo.particulaDoAtaquePergaminhoFora;

    private const float TEMPO_DE_ANIMA_PARTICULAS = 1;

    public string[] TextoDaMensagemInicial
    {
        get { return textoDaMensInicial; }
        protected set { textoDaMensInicial = value; }
    }

    public DoJogo Particula
    {
        get { return particula; }
        protected set { particula = value; }
    }

    public ItemModificadorDeAtributoBase(ContainerDeCaracteristicasDeItem cont) : base(cont){ }

    protected override void EscolhiEmQuemUsar(int indice)
    {
        
        Atributos A = GameController.g.Manager.Dados.CriaturesAtivos[indice].CaracCriature.meusAtributos;

        EscolhiEmQuemUsar(indice,A.PV.Corrente > 0,true);
    }

    public override void AcaoDoItemConsumivel(CriatureBase C)
    {
        AplicaEfeito(C);
        if (!GameController.g.estaEmLuta)
            GameController.g.Salvador.SalvarAgora();
    }

    public void InicioComum()
    {

        GameController.g.HudM.Painel.AtivarNovaMens(textoDaMensInicial[0], 25);
        GameController.g.HudM.Menu_Basico.IniciarHud(OpcaoEscolhida, NomesDosCriaturesAtivos());
        Estado = EstadoDeUsoDeItem.selecaoDeItem;

    }


    public override void IniciaUsoDeHeroi(GameObject dono)
    {
        GameController.g.Manager.Estado = EstadoDePersonagem.parado;
        InicioComum();
    }

    public override bool AtualizaUsoComCriature()
    {
        return AtualizaUsoDoPergaminho();
    }

    public override bool AtualizaUsoDeHeroi()
    {
        return AtualizaUsoDoPergaminho();
    }

    protected virtual bool AtualizaUsoDoPergaminho()
    {
        switch (Estado)
        {
            case EstadoDeUsoDeItem.selecaoDeItem:
                GameController.g.HudM.Menu_Basico.MudarOpcao();
                if (CommandReader.ButtonDown(1, GameController.g.Manager.Control))
                {
                    Debug.Log("dispara cancel");
                    ActionManager.ModificarAcao(GameController.g.transform, null);
                    GameController.g.HudM.Painel.EsconderMensagem();
                    GameController.g.HudM.Menu_Basico.FinalizarHud();
                    Estado = EstadoDeUsoDeItem.finalizaUsaItem;
                } else
                 if (CommandReader.ButtonDown(0, GameController.g.Manager.Control))
                {
                    Debug.Log("disparaacao");
                    OpcaoEscolhida(GameController.g.HudM.Menu_Basico.OpcaoEscolhida);
                }
            break;
            case EstadoDeUsoDeItem.animandoBraco:
                if (!AnimaB.AnimaTroca(true))
                {
                    Estado = EstadoDeUsoDeItem.aplicandoItem;
                    Manager.Mov.Animador.ResetaTroca();
                    AuxiliarDeInstancia.InstancieEDestrua(Particula,
                        GameController.g.Manager.CriatureAtivo.transform.position, 1);

                    AplicaEfeito(GameController.g.Manager.Dados.CriaturesAtivos[opcaoEscolhida]);
                }
            break;
            case EstadoDeUsoDeItem.aplicandoItem:
                TempoDecorrido += Time.deltaTime;
                if (TempoDecorrido > TEMPO_DE_ANIMA_PARTICULAS)
                {
                    AplicaEfeito(GameController.g.Manager.Dados.CriaturesAtivos[opcaoEscolhida]);
                }
            break;
            case EstadoDeUsoDeItem.finalizaUsaItem:
                return false;
        }
        return true;
    }

    public override void IniciaUsoComCriature(GameObject dono)
    {
        //fluxo = FluxoDeRetorno.heroi;
        if (GameController.g.estaEmLuta)
        {
            GameController.g.HudM.Painel.AtivarNovaMens(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.mensLuta)[0], 25, 2);
            Estado = EstadoDeUsoDeItem.finalizaUsaItem;
        }
        else
        {
            GameController.g.Manager.CriatureAtivo.Estado = CreatureManager.CreatureState.parado;
            InicioComum();
        }

    }

    protected string[] NomesDosCriaturesAtivos()
    {
        List<string> nomes = new List<string>();
        List<CriatureBase> meusCriatures = GameController.g.Manager.Dados.CriaturesAtivos;
        for (int i = 0; i < meusCriatures.Count; i++)
        {
            nomes.Add(meusCriatures[i].NomeEmLinguas + "\t Lv: " + meusCriatures[i].G_XP.Nivel);
        }

        return nomes.ToArray();
    }

    protected virtual void OpcaoEscolhida(int escolha)
    {
        if(Consumivel)
            RetirarUmItem(GameController.g.Manager, this, 1);
        
        GameController.g.HudM.Menu_Basico.FinalizarHud();
        GameController.g.HudM.Painel.EsconderMensagem();
        opcaoEscolhida = escolha;

        if (escolha == 0)
        {
            InicializacaoComum(GameController.g.Manager.gameObject, GameController.g.Manager.CriatureAtivo.transform);
            Estado = EstadoDeUsoDeItem.animandoBraco;
        }
        else
        {
            TempoDecorrido = 0;
            AuxiliarDeInstancia.InstancieEDestrua(Particula,
                        GameController.g.Manager.transform.position, 1);
            Estado = EstadoDeUsoDeItem.aplicandoItem;
        }
    }

    protected virtual void AplicaEfeito(CriatureBase C) { }

    protected void Finaliza()
    {
        Estado = EstadoDeUsoDeItem.finalizaUsaItem;
    }

    protected virtual void EntraNoModoFinalizacao(CriatureBase C)
    {

        Estado = EstadoDeUsoDeItem.emEspera;

        if (GameController.g.HudM.MenuDePause.EmPause)
        {
            Finaliza();
        }else
            GameController.g.StartCoroutine(MensComAtraso(C));
    }

    System.Collections.IEnumerator MensComAtraso(CriatureBase C)
    {
        yield return new WaitForSeconds(1f);
        GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(Finaliza,
            string.Format(
            textoDaMensInicial[1],
            C.NomeEmLinguas, C.G_XP.Nivel));
    }
}
