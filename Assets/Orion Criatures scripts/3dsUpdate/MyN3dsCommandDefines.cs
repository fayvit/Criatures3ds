using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyN3dsCommandDefines  {

    static bool esteQuadro = false;

    static bool CondicaoDeUsoDeAlternadores
    {
        get
        {
            bool retorno = true;
            CharacterManager manager = GameController.g.Manager;
            if (manager.CriatureAtivo != null)
            {
                CreatureManager.CreatureState estadoDoCriature = manager.CriatureAtivo.Estado;
                if (manager.Estado == EstadoDePersonagem.parado && manager.Estado != EstadoDePersonagem.comMeuCriature)
                    retorno = false;
                else if (manager.Estado == EstadoDePersonagem.comMeuCriature
                    &&
                    (estadoDoCriature == CreatureManager.CreatureState.parado || estadoDoCriature == CreatureManager.CreatureState.morto))
                    retorno = false;
            }
            else
                retorno = false;

            return retorno;

        }
    }

    public static void DirForMove()
    {

    }

    public static Vector2 DirForCam()
    {
        float x, y;
        Controlador c = GameController.g.Manager.Control;
        if (!(CommandReader.PressionadoBotao(4, (int)c)&&CommandReader.PressionadoBotao(2,(int)c)))
        {
            x = CommandReader.GetAxis("Xcam", c);// * caracteristicas.xSpeed * 0.02f;
            y = CommandReader.GetAxis("Ycam",c);// * caracteristicas.ySpeed * 0.02f;
        }
        else
        {
            x = Mathf.Min(CommandReader.GetAxis("Xcam", c)+CommandReader.GetAxis("horizontal",c),1);
            y = Mathf.Min(CommandReader.GetAxis("Ycam", c) + CommandReader.GetAxis("vertical", c), 1);
        }

        return new Vector2(x, y);
    }

    public static bool FocarCamera()
    {
        int c = (int)GameController.g.Manager.Control;

        if (CommandReader.PressionadoBotao(4, c) && !CommandReader.PressionadoBotao(5, c) && !esteQuadro)
        {
            return CommandReader.ButtonDown(1,c);
        }else return CommandReader.ButtonDown(8, c)
                || CommandReader.ButtonDown(9, c);

    }

    public static void StandardButtons()
    {
        if (!GameController.g.HudM.MenuDePause.EmPause)
        {
            CharacterManager manager = GameController.g.Manager;
            int c = (int)GameController.g.Manager.Control;
            if (!CommandReader.PressionadoBotao(4, c) && !CommandReader.PressionadoBotao(5, c) && !esteQuadro)
            {
                if (CommandReader.ButtonDown(1, GameController.g.Manager.Control))//|| ValorDeGatilhos("Jump") > 0)
                    GameController.g.BotaoPulo();

                if (CommandReader.ButtonDown(0, GameController.g.Manager.Control)
               && manager.Estado == EstadoDePersonagem.comMeuCriature)
                    GameController.g.BotaoAtaque();

                /*
                if (CommandReader.ButtonDown(3, GameController.g.Manager.Control))
                {
                    GameController.g.MyKeys.MudaShift(KeyShift.estouNoTuto, true);

                    Debug.Log("estou no tuto para retirar");
                    Debug.Log(GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto) + " : " + GameController.g.EmEstadoDeAcao());
                }*/

                if (GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto))
                    if (ActionManager.ButtonUp(3, GameController.g.Manager.Control)
                        && GameController.g.EmEstadoDeAcao()
                        && !GameController.g.estaEmLuta
                        && !esteQuadro
                        )
                    {
                        GameController.g.BotaoAlternar();
                    }

                if (ActionManager.ButtonUp(7, GameController.g.Manager.Control))
                {
                    GameController.g.HudM.MenuDePause.PausarJogo();
                }

                if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                {
                    ActionManager.VerificaAcao();
                }

            }
            else if (CommandReader.PressionadoBotao(4, c) && !CommandReader.PressionadoBotao(5, c) && !esteQuadro)
            {
                if (CommandReader.ButtonDown(0, GameController.g.Manager.Control) && GameController.g.EmEstadoDeAcao() && manager.Dados.Itens.Count > 0)
                    GameController.g.BotaUsarItem();

                if (CommandReader.ButtonDown(3, GameController.g.Manager.Control) && GameController.g.EmEstadoDeAcao() && manager.Dados.CriaturesAtivos.Count > 1)
                    GameController.g.BotaTrocarCriature();
            }
            else if (!CommandReader.PressionadoBotao(4, c) && CommandReader.PressionadoBotao(5, c) && !esteQuadro)
            {
                //float val = CommandReader.ValorDeGatilhos("VDpad", GameController.g.Manager.Control);
                if (CommandReader.ButtonDown(1,c)&& manager.Estado == EstadoDePersonagem.comMeuCriature)
                    if (manager.CriatureAtivo.Estado != CreatureManager.CreatureState.parado
                        &&
                        manager.CriatureAtivo.Estado != CreatureManager.CreatureState.morto
                        )
                        GameController.g.BotaoMaisAtaques(1);


                if (CommandReader.ButtonDown(3, c) && manager.Dados.CriaturesAtivos.Count > 1 && CondicaoDeUsoDeAlternadores)
                    GameController.g.BotaoMaisCriature(1);

                int val = CommandReader.ButtonDown(0, c) ? 1 : CommandReader.ButtonDown(2, c) ? -1 : 0;

                if (val != 0 && manager.Dados.Itens.Count > 0 && CondicaoDeUsoDeAlternadores)
                    GameController.g.BotaItens(val > 0 ? 1 : -1);
            }
        }
    }

    public static void StandardButtons_b()
    {
        if (!GameController.g.HudM.MenuDePause.EmPause)
        {
            CharacterManager manager = GameController.g.Manager;

            if (!ActionManager.useiCancel)
            {
                if (CommandReader.ButtonDown(1,GameController.g.Manager.Control))//|| ValorDeGatilhos("Jump") > 0)
                    GameController.g.BotaoPulo();

                

                if (CommandReader.ButtonDown(3, GameController.g.Manager.Control))
                {
                    GameController.g.MyKeys.MudaShift(KeyShift.estouNoTuto, true);

                    Debug.Log("estou no tuto para retirar");
                    Debug.Log(GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto)+" : "+ GameController.g.EmEstadoDeAcao());
                }

                if (GameController.g.MyKeys.VerificaAutoShift(KeyShift.estouNoTuto))
                    if (ActionManager.ButtonUp(3, GameController.g.Manager.Control)
                        && GameController.g.EmEstadoDeAcao()
                        && !GameController.g.estaEmLuta
                        &&!esteQuadro
                        )
                    {
                        GameController.g.BotaoAlternar();
                    }

                if (ActionManager.ButtonUp(7, GameController.g.Manager.Control))
                {
                    GameController.g.HudM.MenuDePause.PausarJogo();
                }
            }
            else
                ActionManager.useiCancel = false;

            if (CommandReader.ButtonDown(0, GameController.g.Manager.Control) 
               && manager.Estado == EstadoDePersonagem.comMeuCriature)
                GameController.g.BotaoAtaque();
       //     else if (useiAcao)
         //       useiAcao = false;

            float val = CommandReader.ValorDeGatilhos("VDpad", GameController.g.Manager.Control);
            if (val < 0  && manager.Estado == EstadoDePersonagem.comMeuCriature)
                if (manager.CriatureAtivo.Estado != CreatureManager.CreatureState.parado
                    &&
                    manager.CriatureAtivo.Estado != CreatureManager.CreatureState.morto
                    )
                    GameController.g.BotaoMaisAtaques(1);


            if (val > 0 && manager.Dados.CriaturesAtivos.Count > 1 && CondicaoDeUsoDeAlternadores)
                GameController.g.BotaoMaisCriature(1);

            val = CommandReader.ValorDeGatilhos("HDpad", GameController.g.Manager.Control);
            if (val != 0 && manager.Dados.Itens.Count > 0 && CondicaoDeUsoDeAlternadores)
                GameController.g.BotaItens(val > 0 ? 1 : -1);

            if (CommandReader.ButtonDown(4, GameController.g.Manager.Control) && GameController.g.EmEstadoDeAcao() && manager.Dados.Itens.Count > 0)
                GameController.g.BotaUsarItem();

            if (CommandReader.ButtonDown(5, GameController.g.Manager.Control) && GameController.g.EmEstadoDeAcao() && manager.Dados.CriaturesAtivos.Count > 1)
                GameController.g.BotaTrocarCriature();

            if (ActionManager.ButtonUp(0, GameController.g.Manager.Control))
                ActionManager.VerificaAcao();
        }
    }
}
