using UnityEngine;
using System.Collections.Generic;

public class ReplaceManager
{
    private AnimaBraco animaB;
    private FluxoDeRetorno fluxo;
    private CharacterManager manager;
    private bool estouTrocandoDeCriature = false;

    public FluxoDeRetorno Fluxo
    {
        get { return fluxo; }
    }

    public bool EstouTrocandoDeCriature
    {
        get { return estouTrocandoDeCriature; }
    }
    public ReplaceManager(CharacterManager manager,Transform alvo,FluxoDeRetorno fluxo)
    {
        this.fluxo = fluxo;
        this.manager = manager;
        List<CriatureBase> lista = manager.Dados.CriaturesAtivos;
        CriatureBase temp = lista[0];
        lista[0] = lista[manager.Dados.CriatureSai+1];
        lista[manager.Dados.CriatureSai+1] = temp;

        manager.Estado = EstadoDePersonagem.parado;
        manager.CriatureAtivo.PararCriatureNoLocal();
        manager.Mov.Animador.PararAnimacao();

        GameController.g.HudM.ModoLimpo();
        GameController.g.HudM.Painel.EsconderMensagem();

        estouTrocandoDeCriature = true;        
        animaB = new AnimaBraco(manager.transform, alvo);
    }

    public bool Update()
    {
        if (estouTrocandoDeCriature)
        {
            if (!animaB.AnimaTroca())
            {
                if (!animaB.AnimaEnvia())
                {
                    if (fluxo == FluxoDeRetorno.heroi || fluxo==FluxoDeRetorno.menuHeroi)
                    {
                        manager.AoHeroi();
                    }

                    manager.Mov.Animador.ResetaTroca();
                    manager.Mov.Animador.ResetaEnvia();
                    estouTrocandoDeCriature = false;
                }
            }
        }

        return !estouTrocandoDeCriature;
    }
}

public enum FluxoDeRetorno
{
    heroi,
    criature,
    menuHeroi,
    menuCriature,
    armagedom
}
