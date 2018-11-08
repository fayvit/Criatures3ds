using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PainelQuantidadesParaShop : MonoBehaviour
{

    [SerializeField] private Text labelCristais;
    [SerializeField] private Text mensagem;
    [SerializeField] private Text quantidadeTXt;
    [SerializeField] private Text valorAPagar;
    [SerializeField] private Text labelValorAPagar;
    [SerializeField] private Text infos;
    [SerializeField] private Text labelDoBotaoComprar;
    [SerializeField] private Text labelDoBotaoVoltar;
    [SerializeField] private RawImage itemImage;

    private int quantidade = 1;
    private bool comprar;
    //private CommandReader commandR = new CommandReader();
    private MbItens esseItem;
    private DadosDoPersonagem dados;
    private EstadoDaqui estado = EstadoDaqui.botoesAtivos;
    private string[] textos = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.textosParaQuantidadesEmShop).ToArray();

    private enum EstadoDaqui
    {
        botoesAtivos,
        fraseAgradecer,
        finalizacao,
        emEspera
    }

    public bool Comprar
    {
        get { return comprar; }
    }

    private void Update()
    {
        if (gameObject.activeSelf)
        {
            switch (estado)
            {
                case EstadoDaqui.botoesAtivos:
                    if (ActionManager.ButtonUp(1, GameController.g.Manager.Control))
                    {
                        BtnVoltar();
                    }
                    else if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                        ActionManager.VerificaAcao();

                        int val = CommandReader.ValorDeGatilhos("HDpad",GameController.g.Manager.Control);
                    if (val == 0)
                        val = CommandReader.ValorDeGatilhosTeclado("horizontal",GameController.g.Manager.Control);

                    if (val == 1)
                        BotaoMaisUm();
                    else if (val == -1)
                        BotaoMenosUm();
                    else
                    {
                        val = CommandReader.ValorDeGatilhos("VDpad", GameController.g.Manager.Control);
                        if (val == 0)
                            val = CommandReader.ValorDeGatilhosTeclado("vertical", GameController.g.Manager.Control);

                        if (val == 1)
                            BotaoMaisDez();
                        else if (val == -1)
                            BotaoMenosDez();
                    }
                break;
                case EstadoDaqui.fraseAgradecer:
                    if (!GameController.g.HudM.DisparaT.LendoMensagemAteOCheia())
                    {
                        estado = EstadoDaqui.finalizacao;
                        ActionManager.ModificarAcao(transform, () => { gameObject.SetActive(false); });
                    }
                break;
                case EstadoDaqui.finalizacao:
                    if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                    {
                        gameObject.SetActive(false);
                    }
                break;
            }
        }
    }

    void ReligarBotoes()
    {
        estado = EstadoDaqui.botoesAtivos;
        BtnsManager.ReligarBotoes(gameObject);
        ActionManager.ModificarAcao(transform, BotaoComprar);
        //BtnsManager.ReligarBotoes(GameController.g.HudM.Botaozao.gameObject);

    }

    void AtualizaQuantidade(int quantidade, int valor)
    {
        this.quantidade = quantidade;
        if(comprar)
            quantidadeTXt.text = quantidade.ToString();
        else
            quantidadeTXt.text = quantidade+" / "+ dados.TemItem(esseItem.ID);
        valorAPagar.text = (quantidade * valor).ToString();
    }

    void DesligaBotoes()
    {
        
        BtnsManager.DesligarBotoes(gameObject);
        /*BtnsManager.DesligarBotoes(GameController.g.HudM.Botaozao.gameObject);*/
    }

    void VerificaMais(int tanto)
    {
        if (comprar)
        {
            if ((quantidade + tanto) * esseItem.Valor > dados.Cristais && dados.Cristais >= esseItem.Valor)
            {
                DesligaBotoes();
                estado = EstadoDaqui.emEspera;
                GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(ReligarBotoes,
                    string.Format(textos[7], dados.Cristais / esseItem.Valor, MbItens.NomeEmLinguas(esseItem.ID))
                    );
                AtualizaQuantidade(Mathf.Max(dados.Cristais / esseItem.Valor, 1), esseItem.Valor);
            }
            else if (dados.Cristais < esseItem.Valor)
            {
                DesligaBotoes();
                estado = EstadoDaqui.emEspera;
                GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(ReligarBotoes,
                    string.Format(textos[11], dados.Cristais / esseItem.Valor, MbItens.NomeEmLinguas(esseItem.ID))
                    );
                AtualizaQuantidade(1, esseItem.Valor);
            }
            else
            {
                AtualizaQuantidade(quantidade + tanto, esseItem.Valor);
            }
        }
        else
        {
            if (quantidade + tanto > dados.TemItem(esseItem.ID))
            {
                DesligaBotoes();
                estado = EstadoDaqui.emEspera;
                GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(ReligarBotoes,
                    string.Format(textos[8], dados.TemItem(esseItem.ID), MbItens.NomeEmLinguas(esseItem.ID))
                    );
                AtualizaQuantidade(dados.TemItem(esseItem.ID), Mathf.Max(1,esseItem.Valor / 4));
            }
            else
            {
                AtualizaQuantidade(quantidade + tanto, Mathf.Max(1, esseItem.Valor / 4));
            }
        }
    }

    void VerificaMenos(int tanto)
    {
        if (comprar)
        {
            if (quantidade - tanto <= 0)
            {
                DesligaBotoes();
                estado = EstadoDaqui.emEspera;
                GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(ReligarBotoes, textos[9]);
                AtualizaQuantidade(1, esseItem.Valor);
            }
            else
                AtualizaQuantidade(quantidade - tanto, esseItem.Valor);
        }
        else
        {
            if (quantidade - tanto <= 0)
            {
                DesligaBotoes();
                estado = EstadoDaqui.emEspera;
                GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(ReligarBotoes, textos[10]);
                AtualizaQuantidade(1, Mathf.Max(1, esseItem.Valor / 4));
            }
            else
                AtualizaQuantidade(quantidade - tanto, Mathf.Max(1, esseItem.Valor / 4));
        }
    }

    public void IniciarEssaHud(MbItens itemAlvo, bool comprar = true)
    {
        this.comprar = comprar;
        this.itemImage.texture = GameController.g.El.RetornaMini(itemAlvo.ID);
        BtnsManager.ReligarBotoes(gameObject);
        ActionManager.ModificarAcao(transform, BotaoComprar);
        estado = EstadoDaqui.botoesAtivos;
        gameObject.SetActive(true);
        esseItem = itemAlvo;
        dados = GameController.g.Manager.Dados;
        quantidade = 1;

        labelCristais.text = textos[0] + dados.Cristais;
        mensagem.text = string.Format(comprar ? textos[3] : textos[4], MbItens.NomeEmLinguas(itemAlvo.ID));
        infos.text = BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.shopInfoItem)[(int)itemAlvo.ID];
        quantidadeTXt.text = comprar ? quantidade.ToString() : quantidade + " / " + dados.TemItem(esseItem.ID);

        valorAPagar.text = (itemAlvo.Valor / (comprar ? 1 : 4)).ToString();
        labelValorAPagar.text = comprar ? textos[1] : textos[2];
        labelDoBotaoComprar.text = comprar ? textos[5] : textos[6];
    }

    public void BotaoMaisUm()
    {
        VerificaMais(1);
    }

    public void BotaoMaisDez()
    {
        VerificaMais(10);
    }

    public void BotaoMenosUm()
    {
        VerificaMenos(1);
    }

    public void BotaoMenosDez()
    {
        VerificaMenos(10);
    }

    void EntrarNaFraseAgradecer()
    {
        estado = EstadoDaqui.fraseAgradecer;
        BtnsManager.DesligarBotoes(gameObject);
        DisparaTexto dispara = GameController.g.HudM.DisparaT;
        ActionManager.ModificarAcao(transform, null);
        dispara.ReligarPaineis();
        dispara.Dispara(comprar
            ?
            BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.frasesDeShoping)[2]
            :
            BancoDeTextos.RetornaListaDeTextoDoIdioma(ChaveDeTexto.frasesDeShoping)[3]);
    }

    public void BotaoComprar()
    {
        if (comprar)
        {
            if (dados.Cristais >= quantidade * esseItem.Valor)
            {
                dados.AdicionaItem(esseItem.ID, quantidade);
                dados.Cristais -= quantidade * esseItem.Valor;
                GameController.g.HudM.AtualizeImagemDeAtivos();
                EntrarNaFraseAgradecer();
                //gameObject.SetActive(false);
            }
            else if (dados.Cristais < esseItem.Valor)
            {
                DesligaBotoes();
                GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(ReligarBotoes,
                    string.Format(textos[11], dados.Cristais / esseItem.Valor, MbItens.NomeEmLinguas(esseItem.ID))
                    );
                AtualizaQuantidade(1, esseItem.Valor);
            }
            else
            {
                DesligaBotoes();
                GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(ReligarBotoes,
                    string.Format(textos[7], dados.Cristais / esseItem.Valor, MbItens.NomeEmLinguas(esseItem.ID))
                    );
                AtualizaQuantidade(dados.Cristais / esseItem.Valor, esseItem.Valor);
            }
        }
        else
        {
            if (quantidade <= dados.TemItem(esseItem.ID))
            {
                MbItens.RetirarUmItem(GameController.g.Manager, esseItem, quantidade, FluxoDeRetorno.armagedom);
                dados.Cristais += (quantidade * Mathf.Max(1, esseItem.Valor / 4));
                GameController.g.HudM.AtualizeImagemDeAtivos();
                EntrarNaFraseAgradecer();//gameObject.SetActive(false);
            }
            else
            {
                DesligaBotoes();
                GameController.g.HudM.UmaMensagem.ConstroiPainelUmaMensagem(ReligarBotoes,
                    string.Format(textos[8], dados.TemItem(esseItem.ID), MbItens.NomeEmLinguas(esseItem.ID))
                    );
                AtualizaQuantidade(dados.TemItem(esseItem.ID), Mathf.Max(1, esseItem.Valor / 4));
            }
        }
    }

    public void BtnVoltar()
    {
        ActionManager.useiCancel = true;
        gameObject.SetActive(false);
    }
}