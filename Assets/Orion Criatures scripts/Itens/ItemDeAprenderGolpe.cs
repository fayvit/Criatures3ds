using UnityEngine;
using System.Collections;

[System.Serializable]
public class ItemDeAprenderGolpe : ItemModificadorDeAtributoBase
{
    protected nomesGolpes[] golpeDoPergaminho;
    protected bool esqueceu = false;
    
    protected int indiceDoEscolhido = -1;
    protected EstadoDoAprendeGolpe estadoDoAprendeGolpe = EstadoDoAprendeGolpe.baseUpdate;

    protected enum EstadoDoAprendeGolpe
    {
        baseUpdate,
        esperandoConfirmacaoDeEsquecimento,
        esperandoConfirmacaoDoNaoAprender,
        aprendiSemEsquecer
    }

    public ItemDeAprenderGolpe(ContainerDeCaracteristicasDeItem cont) : base(cont) { }

    protected virtual string NomeBasico
    {
        get { return GolpeBase.NomeEmLinguas(golpeDoPergaminho[0]); }
    }

    protected override bool AtualizaUsoDoPergaminho()
    {
        Debug.Log("aqui");
        switch (estadoDoAprendeGolpe)
        {
            case EstadoDoAprendeGolpe.baseUpdate:
                return base.AtualizaUsoDoPergaminho();
            case EstadoDoAprendeGolpe.esperandoConfirmacaoDeEsquecimento:
                if (CommandReader.ButtonDown(0, GameController.g.Manager.Control))
                {
                    Debug.Log("disparaacao");
                    GameController.g.HudM.P_Golpe.gameObject.SetActive(false);
                    GameController.g.HudM.Painel.EsconderMensagem();
                    base.OpcaoEscolhida(indiceDoEscolhido);
                    estadoDoAprendeGolpe = EstadoDoAprendeGolpe.baseUpdate;
                }
                break;
            case EstadoDoAprendeGolpe.aprendiSemEsquecer:
            case EstadoDoAprendeGolpe.esperandoConfirmacaoDoNaoAprender:
                if (CommandReader.ButtonDown(0, GameController.g.Manager.Control))
                {
                    Debug.Log("disparaacao");
                    GameController.g.HudM.P_Golpe.gameObject.SetActive(false);
                    GameController.g.HudM.Painel.EsconderMensagem();
                    estadoDoAprendeGolpe = EstadoDoAprendeGolpe.baseUpdate;
                    Estado = EstadoDeUsoDeItem.finalizaUsaItem;
                }
            break;
        }

        return true;

    }

    private struct Usara
    {
        public bool jaTem;
        public bool diferenteDeNulo;
    }

    Usara VerificaUso(CriatureBase C)
    {
        nomesGolpes golpePorAprender = GolpePorAprender(C);
        

        return new Usara() {
            diferenteDeNulo = golpePorAprender != nomesGolpes.nulo,
            jaTem = C.GerenteDeGolpes.TemEsseGolpe(golpePorAprender)
        };
    }

    protected override void OpcaoEscolhida(int escolha)
    {
        GameController g = GameController.g;
        CriatureBase C = g.Manager.Dados.CriaturesAtivos[escolha];
        nomesGolpes golpePorAprender = GolpePorAprender(C);
        indiceDoEscolhido = escolha;
        g.HudM.Painel.EsconderMensagem();
        /*
        bool foi = false;
        g.HudM.Painel.EsconderMensagem();

        if (golpePorAprender != nomesGolpes.nulo)
            foi = true;

        bool jaTem = C.GerenteDeGolpes.TemEsseGolpe(golpePorAprender);
        */

        Usara usara = VerificaUso(C);
        if (usara.diferenteDeNulo && !usara.jaTem)
        {
            if (C.GerenteDeGolpes.meusGolpes.Count >= 4)
                VerificaQualEsquecer(C,UsarOuNaoUsar);
            else
                base.OpcaoEscolhida(escolha);

        }
        else if (usara.jaTem)
        {
            g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(() => { Estado = EstadoDeUsoDeItem.finalizaUsaItem; },
                string.Format(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.itens)[4],
                C.NomeEmLinguas, MbItens.NomeEmLinguas(ID), GolpeBase.NomeEmLinguas(golpePorAprender)
                )
                );
        }
        else if (!usara.diferenteDeNulo)
        {
            g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(() => { Estado = EstadoDeUsoDeItem.finalizaUsaItem; },
                string.Format(BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.itens)[5],
                C.NomeEmLinguas, NomeBasico
                )
                );
        }

        g.HudM.Menu_Basico.FinalizarHud();

    }

    protected override void EscolhiEmQuemUsar(int indice)
    {
        indiceDoEscolhido = indice;
        CriatureBase C = GameController.g.Manager.Dados.CriaturesAtivos[indice];
        Atributos A = C.CaracCriature.meusAtributos;
        Usara usara = VerificaUso(C);

        if ((usara.diferenteDeNulo && !usara.jaTem) || A.PV.Corrente <= 0)
        {
            if (C.GerenteDeGolpes.meusGolpes.Count >= 4 && A.PV.Corrente > 0)
            {

                VerificaQualEsquecer(C, UsarOuNaoUsarMenu);
                GameController.g.HudM.P_EscolheUsoDeItens.gameObject.SetActive(false);
                GameController.g.HudM.MenuDePause.EsconderPainelDeItens();
            }
            else
                EscolhiEmQuemUsar(indice, A.PV.Corrente > 0, true);
        }
        else if (!usara.diferenteDeNulo)
        {
            MensDeUsoDeItem.MensNaoPodeAprenderGolpe(NomeBasico,C.NomeEmLinguas);
        }
        else if (usara.jaTem)
        {
            MensDeUsoDeItem.MensjaConheceGolpe(C.NomeEmLinguas,MbItens.NomeEmLinguas(ID),GolpeBase.NomeEmLinguas(GolpePorAprender(C)));
        }
        
    }

    void VerificaQualEsquecer(CriatureBase C,System.Action<bool> acao)
    {
        Estado = EstadoDeUsoDeItem.emEspera;
        HudManager hudM = GameController.g.HudM;
        nomesGolpes nomeDoGolpe = GolpePorAprender(C);
        hudM.H_Tenta.Aciona(C, nomeDoGolpe,
            string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.precisaEsquecer), C.NomeEmLinguas)
            , acao);

        hudM.Painel.AtivarNovaMens(
            string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.tentandoAprenderGolpe),
            C.NomeEmLinguas,
            GolpeBase.NomeEmLinguas(nomeDoGolpe))
            , 24
            );

        hudM.P_Golpe.Aciona(PegaUmGolpeG2.RetornaGolpe(nomeDoGolpe));
    }

    void UsarOuNaoUsarMenu(bool usou)
    {
        CriatureBase C = GameController.g.Manager.Dados.CriaturesAtivos[indiceDoEscolhido];
        esqueceu = usou;
        if (usou)
        {
            GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(TrocouComMenu, 
                string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.aprendeuGolpeEsquecendo),
                        C.NomeEmLinguas,
                        "",
                        GolpeBase.NomeEmLinguas(GolpePorAprender(C))));
        }
        else
        {
            GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(DesistiuDeAprenderComMenu,
            string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.naoAprendeuGolpeNovo),
                        C.NomeEmLinguas,
                        GolpeBase.NomeEmLinguas(GolpePorAprender(C))));
        }

    }

    void RetornoComumDeMenu()
    {
        GameController.g.HudM.P_EscolheUsoDeItens.gameObject.SetActive(true);
        GameController.g.HudM.P_Golpe.gameObject.SetActive(false);
        //GameController.g.HudM.MenuDePause.BotaoItens();
    }

    void DesistiuDeAprenderComMenu()
    {

        RetornoComumDeMenu();
    }

    void TrocouComMenu()
    {
        RetornoComumDeMenu();
        esqueceu = true;
        //EscolhiEmQuemUsar(indiceDoEscolhido);
    }

    /*
     
         case EstadoDoAprendeGolpe.esperandoConfirmacaoDeEsquecimentoMenu:
                if (GameController.g.CommandR.DisparaAcao())
                {
                    GameController.g.HudM.P_Golpe.gameObject.SetActive(false);
                    GameController.g.HudM.Painel.EsconderMensagem();
                    GameController.g.HudM.P_EscolheUsoDeItens.gameObject.SetActive(true);
                    EscolhiEmQuemUsar(indiceDoEscolhido);                    
                    return false;
                }
                break;
            case EstadoDoAprendeGolpe.esperandoConfirmacaoDoNaoAprenderMenu:
                if (GameController.g.CommandR.DisparaAcao())
                {
                    GameController.g.HudM.P_EscolheUsoDeItens.gameObject.SetActive(true);
                    GameController.g.HudM.P_Golpe.gameObject.SetActive(false);
                    GameController.g.HudM.Painel.EsconderMensagem();
                    return false;
                }
                break;*/

    void UsarOuNaoUsar(bool usou)
    {
        esqueceu = usou;
        if (usou)
        {
            estadoDoAprendeGolpe = EstadoDoAprendeGolpe.esperandoConfirmacaoDeEsquecimento;
            Debug.Log(estadoDoAprendeGolpe);
        }
        else
        {
            estadoDoAprendeGolpe = EstadoDoAprendeGolpe.esperandoConfirmacaoDoNaoAprender;
        }

    }

    protected virtual nomesGolpes GolpePorAprender(CriatureBase C)
    {
        return C.GerenteDeGolpes.ProcuraGolpeNaLista(C.NomeID, golpeDoPergaminho[0]).Nome;
    }

    protected override void AplicaEfeito(CriatureBase C)
    {
        if (!esqueceu)
        {
            C.GerenteDeGolpes.meusGolpes.Add(PegaUmGolpeG2.RetornaGolpe(GolpePorAprender(C)));
        }
        EntraNoModoFinalizacao(C);

    }

    protected override void EntraNoModoFinalizacao(CriatureBase C)
    {
        Estado = EstadoDeUsoDeItem.emEspera;

        if (GameController.g.HudM.MenuDePause.EmPause)
        {
            Finaliza();
        }
        else if (!esqueceu)
        {
            nomesGolpes nomeDoGolpe = GolpePorAprender(C);
            GameController.g.HudM.Painel.AtivarNovaMens(
                        string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.aprendeuGolpe),
                        C.NomeEmLinguas,
                        GolpeBase.NomeEmLinguas(nomeDoGolpe))
                        , 30
                        );
            GameController.g.HudM.P_Golpe.Aciona(PegaUmGolpeG2.RetornaGolpe(nomeDoGolpe));
            estadoDoAprendeGolpe = EstadoDoAprendeGolpe.aprendiSemEsquecer;
        }
        else if (esqueceu)
        {
            GameController.g.StartCoroutine(TerminaDaquiAPouco());
        }
    }

    IEnumerator TerminaDaquiAPouco()
    {
        yield return new WaitForSeconds(1.5f);
        Finaliza();
    }

}