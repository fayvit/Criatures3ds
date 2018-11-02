using UnityEngine;
using System.Collections;

public class ApresentaFim
{
    private bool apresentouFim = false;
    private bool ligouMensagemDeFim = false;
    private float contadorDeTempo = 0;

    private CreatureManager criatureDoJogador;
    private CreatureManager inimigoDerrotado;
    private AplicadorDeCamera cam;

    private const float TEMPO_PARA_MOSTRAR_VITORIA = 1.3f;
    private const float TEMPO_PARA_FECHAR_APRESENTA_FIM = 10;

    public ApresentaFim(CreatureManager criatureDoJogador,CreatureManager inimigoDerrotado,AplicadorDeCamera cam)
    {
        this.criatureDoJogador = criatureDoJogador;
        this.inimigoDerrotado = inimigoDerrotado;
        this.cam = cam;
    }
    

    void ColocaCameraEmPOsicao()
    {
        
        float alturaParaCamera = criatureDoJogador.Mov.Controle.height;
        //Deve verificar o tipo de movimentação da camera
        cam.transform.position = (criatureDoJogador.transform.position
            + 8 * criatureDoJogador.transform.forward
            + (5 + alturaParaCamera) * Vector3.up);
        cam.transform.LookAt(criatureDoJogador.transform);

    }

    public bool EstouApresentando(bool treinador)
    {
        contadorDeTempo += Time.deltaTime;
        if (contadorDeTempo > TEMPO_PARA_MOSTRAR_VITORIA)
        {
            if (!apresentouFim)
            {
                ColocaCameraEmPOsicao();
                cam.InicializaCameraExibicionista(criatureDoJogador.transform, criatureDoJogador.Mov.Controle.height);
                //GameController.g.HudM.DesligaContainerDoInimigo();
                apresentouFim = true;
            }
            else
            {
                if (!ligouMensagemDeFim)
                {
                    Atributos A = inimigoDerrotado.MeuCriatureBase.CaracCriature.meusAtributos;
                    GameController.g.HudM.Painel.AtivarNovaMens(
                        string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.apresentaFim),
                        criatureDoJogador.MeuCriatureBase.NomeID,
                        treinador?A.PV.Maximo:(int)((float)A.PV.Maximo / 2),
                        treinador?2*A.PV.Maximo:A.PV.Maximo), 32);
                    ligouMensagemDeFim = true;
                }
                else if (
                    CommandReader.ButtonDown(0,GameController.g.Manager.Control) 
                    || 
                    contadorDeTempo > TEMPO_PARA_FECHAR_APRESENTA_FIM)
                {
                    GameController.g.HudM.Painel.EsconderMensagem();
                    return false;
                }

            }
        }
        return true;
    }
}
