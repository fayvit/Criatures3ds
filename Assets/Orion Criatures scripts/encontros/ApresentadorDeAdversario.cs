using UnityEngine;
using System.Collections;

public class ApresentadorDeAdversario
{
    private bool foiApresentado = false;
    private bool painelAberto = false;
    private bool treinador = false;
    private string nomeTreinador = "";
    private CreatureManager inimigo;

    public ApresentadorDeAdversario(CreatureManager inimigo,bool treinador = false,string nomeTreinador="")
    {
        this.treinador = treinador;
        this.nomeTreinador = nomeTreinador;
        this.inimigo = inimigo;
    }

    public bool Apresenta(float contadorDeTempo,AplicadorDeCamera cam)
    {
        if (contadorDeTempo > 0.5f)
            if (!foiApresentado)
            {
                cam.transform.position = inimigo.transform.position + 8 * inimigo.transform.forward + 5 * Vector3.up;
                cam.transform.LookAt(inimigo.transform);
                cam.InicializaCameraExibicionista(inimigo.transform, inimigo.GetComponent<CharacterController>().height);
                foiApresentado = true;
            }
            else
            {
                
                //cam.transform.RotateAround(inimigo.transform.position, Vector3.up, 15 * Time.deltaTime);
                if (!painelAberto)
                {
                    painelAberto = true;
                   // bugDoNivel1();

                    iniciaApresentaInimigo();
                }
                else   if (CommandReader.ButtonDown(0,GameController.g.Manager.Control)|| contadorDeTempo>10)
                {
                    //CommandReader.useiAcao = true;
                    GameController.g.HudM.Painel.EsconderMensagem();
                    return true;
                }
            }

        return false;
    }

    protected virtual void iniciaApresentaInimigo()
    {
        CriatureBase C = inimigo.MeuCriatureBase;
        string textoBase = treinador 
            ?string.Format(BancoDeTextos.falacoesComChave[BancoDeTextos.linguaChave][ChaveDeTexto.apresentaInimigo][1],nomeTreinador)
            + BancoDeTextos.falacoesComChave[BancoDeTextos.linguaChave][ChaveDeTexto.apresentaInimigo][2]
            : BancoDeTextos.falacoesComChave[BancoDeTextos.linguaChave][ChaveDeTexto.apresentaInimigo][0];
        GameController.g.HudM.Painel.AtivarNovaMens(
            string.Format(
            textoBase,
            C.NomeID, C.G_XP.Nivel, C.CaracCriature.meusAtributos.PV.Corrente,
            C.CaracCriature.meusAtributos.PE.Corrente),30
            );
    }
}
