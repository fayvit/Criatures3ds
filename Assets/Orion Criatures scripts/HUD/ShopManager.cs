using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class ShopManager
{
    [SerializeField] private MenuDeShop menuDeShop;
    [SerializeField] private PainelQuantidadesParaShop painelQuantidades;

    private Sprite fotoDoNPC;
    private DisparaTexto dispara;
    private MenuBasico menuBasico;
    private FasesDoShop fase = FasesDoShop.emEspera;
    private nomeIDitem[] itensAVenda;
    private string[] t = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.shopBasico).ToArray();
    private string[] frasesDeShoping = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.frasesDeShoping).ToArray();

    private enum FasesDoShop
    {
        emEspera,
        iniciouConversaNoShop,
        escolhaInicial,
        esperandoEscolhaInicial,
        esperandoEscolhaDeCompraVenda,
        fraseDeVenda,
        fraseDeCompra,
        quantidadesAbertas,
        saindoDoShop,
        esperandoFim

    }

    public void Update()
    {
        switch (fase)
        {
            case FasesDoShop.iniciouConversaNoShop:
                AplicadorDeCamera.cam.FocarPonto(2, 8,-1,true);
                if (dispara.UpdateDeTextos(t, fotoDoNPC)
                    ||
                    dispara.IndiceDaConversa > t.Length - 2
                    )
                {
                    EntraFrasePossoAjudar();
                }
            break;
            case FasesDoShop.escolhaInicial:

                if (!dispara.LendoMensagemAteOCheia())
                {
                    fase = FasesDoShop.esperandoEscolhaInicial;
                    menuBasico.IniciarHud(ComprarVender, BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.comprarOuVender).ToArray());
                    ActionManager.ModificarAcao(GameController.g.transform,()=> {
                        ComprarVender(menuBasico.OpcaoEscolhida);
                        ActionManager.ModificarAcao(GameController.g.transform, null);
                    });
                }
            break;
            case FasesDoShop.esperandoEscolhaInicial:
                if (Input.GetButtonDown("Cancel"))
                {
                    ActionManager.ModificarAcao(GameController.g.transform, null);
                    ActionManager.useiCancel = true;
                    SairDoShop();
                }

                menuBasico.MudarOpcao();
            break;
            case FasesDoShop.fraseDeVenda:
                if (!dispara.LendoMensagemAteOCheia())
                {
                    fase = FasesDoShop.esperandoEscolhaDeCompraVenda;
                    string[] opcoes = new string[itensAVenda.Length];
                    for (int i = 0; i < itensAVenda.Length; i++)
                    {
                        opcoes[i] = itensAVenda[i].ToString();
                    }

                    
                    ActionManager.ModificarAcao(GameController.g.transform, () => {                        
                        ActionManager.ModificarAcao(GameController.g.transform, null);
                        GameController.g.HudM.DisparaT.DesligarPaineis();
                        OpcaoEscolhidaParaCompra(menuDeShop.OpcaoEscolhida);
                    });

                    menuDeShop.IniciarHud(true, OpcaoEscolhidaParaCompra, opcoes);
                    menuDeShop.SetActive(true);
                }
            break;
            case FasesDoShop.esperandoEscolhaDeCompraVenda:
                if (Input.GetButtonDown("Cancel"))
                {
                    ActionManager.ModificarAcao(GameController.g.transform, null);
                    VoltarParaAPerguntaInicial();
                }

                menuDeShop.MudarOpcao();
            break;
            case FasesDoShop.fraseDeCompra:
                if (!dispara.LendoMensagemAteOCheia())
                {
                    fase = FasesDoShop.esperandoEscolhaDeCompraVenda;
                    List<string> opcoes2 = new List<string>();
                    List<MbItens> meusItens = GameController.g.Manager.Dados.Itens;

                    for (int i = 0; i < meusItens.Count; i++)
                    {
                        if(meusItens[i].Valor>0)
                            opcoes2.Add(meusItens[i].ID.ToString());
                    }
                    
                    ActionManager.ModificarAcao(GameController.g.transform, () => {
                        OpcaoEscolhidaParaVenda(menuDeShop.OpcaoEscolhida);
                    });

                    menuDeShop.IniciarHud(false, OpcaoEscolhidaParaVenda, opcoes2.ToArray());
                    menuDeShop.SetActive(true);
                }
            break;
            case FasesDoShop.quantidadesAbertas:
                if (!painelQuantidades.gameObject.activeSelf)
                {
                    menuDeShop.FinalizarHud();
                    if (painelQuantidades.Comprar)
                    {
                        ComprarVender(0);
                    }
                    else
                        ComprarVender(1);
                }
            break;
            case FasesDoShop.saindoDoShop:
                if (!dispara.LendoMensagemAteOCheia())
                {
                    fase = FasesDoShop.esperandoFim;
                    ActionManager.ModificarAcao(GameController.g.transform, Finalizacao);
                }
            break;
            case FasesDoShop.esperandoFim:
                if (Input.GetButtonDown("Cancel"))
                {
                    Finalizacao();
                }
            break;
        }
    }

    void Finalizacao()
    {
        GameController.g.Manager.AoHeroi();
        //g.HudM.ligarControladores();
        dispara.DesligarPaineis();
        fase = FasesDoShop.emEspera;
        ActionManager.ModificarAcao(GameController.g.transform, null);
    }

    void ComprarVender(int i)
    {
        dispara.ReligarPaineis();
        if (i == 0)
        {
            dispara.Dispara(frasesDeShoping[0], fotoDoNPC);
            fase = FasesDoShop.fraseDeVenda;
        }
        else if (i == 1)
        {
            dispara.Dispara(frasesDeShoping[1], fotoDoNPC);
            fase = FasesDoShop.fraseDeCompra;
        }

        /*BotaoZaoExterno btn = GameController.g.HudM.Botaozao;
        btn.FinalizarBotao();
        btn.IniciarBotao(VoltarParaAPerguntaInicial);*/

        menuBasico.FinalizarHud();
    }

    void EntraFrasePossoAjudar()
    {
        dispara.ReligarPaineis();
        dispara.Dispara(t[t.Length - 1], fotoDoNPC);
        fase = FasesDoShop.escolhaInicial;
    }

    public void IniciarShop(nomeIDitem[] itensAVenda, Sprite fotoDoNPC)
    {
        this.fotoDoNPC = fotoDoNPC;

        this.itensAVenda = itensAVenda;

        //GameController.g.HudM.Botaozao.IniciarBotao(SairDoShop);

        fase = FasesDoShop.iniciouConversaNoShop;

        dispara = GameController.g.HudM.DisparaT;
        menuBasico = GameController.g.HudM.Menu_Basico;
        dispara.IniciarDisparadorDeTextos();

    }

    void DesligarQuantidades()
    {
        painelQuantidades.gameObject.SetActive(false);
        //GameController.g.HudM.Botaozao.FinalizarBotao();
    }

    void OpcaoEscolhidaParaCompra(int qual)
    {
        painelQuantidades.IniciarEssaHud(PegaUmItem.Retorna(itensAVenda[qual]));
        
        BtnsManager.DesligarBotoes(menuDeShop.gameObjectDoMenu);
        /*GameController.g.HudM.Botaozao.FinalizarBotao();
        GameController.g.HudM.Botaozao.IniciarBotao(DesligarQuantidades);*/
        fase = FasesDoShop.quantidadesAbertas;
    }

    void OpcaoEscolhidaParaVenda(int qual)
    {
        int indice = qual;
        MbItens[] itens = GameController.g.Manager.Dados.Itens.ToArray();
        if (itens.Length > 0)
        {
            for (int i = -1; i < qual; i++)
            {
                if (itens[i + 1].Valor <= 0)
                    indice++;
            }



            painelQuantidades.IniciarEssaHud(PegaUmItem.Retorna(itens[indice].ID), false);

            BtnsManager.DesligarBotoes(menuDeShop.gameObjectDoMenu);
            /*GameController.g.HudM.Botaozao.FinalizarBotao();
            GameController.g.HudM.Botaozao.IniciarBotao(DesligarQuantidades);*/
            fase = FasesDoShop.quantidadesAbertas;
        }
        else
        {

        }
        
    }

    void VoltarParaAPerguntaInicial()
    {
       /* BotaoZaoExterno btn = GameController.g.HudM.Botaozao;
        btn.FinalizarBotao();
        btn.IniciarBotao(SairDoShop);*/
        menuDeShop.FinalizarHud();
        menuBasico.FinalizarHud();
        EntraFrasePossoAjudar();
    }

    void SairDoShop()
    {
        //GameController g = GameController.g;
//        AndroidController.a.LigarControlador();
        fase = FasesDoShop.saindoDoShop;
        menuBasico.FinalizarHud();
        menuDeShop.FinalizarHud();
        dispara.ReligarPaineis();
        dispara.Dispara(frasesDeShoping[4], fotoDoNPC);

        //g.HudM.Botaozao.FinalizarBotao();

        GameController.g.Salvador.SalvarAgora();

    }
}