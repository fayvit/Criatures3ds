using UnityEngine;
using System.Collections;

public class PassouDeNivelEmLuta
{
    private float contadorDeTempo = 0;
    private CriatureBase oNivelado;
    private GolpePersonagem gp;
    private FasesDoPassouDeNivel fase = FasesDoPassouDeNivel.mostrandoNivel;

    private enum FasesDoPassouDeNivel
    {
        emEspera,
        mostrandoNivel,
        aprendeuGolpe,
        tentandoAprender,
        painelAprendeuGolpeAberto,
        finalizar
    }
    public PassouDeNivelEmLuta(CriatureBase oNivelado)
    {
        this.oNivelado = oNivelado;
        GameController.g.HudM.Painel.AtivarNovaMens(
            string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.passouDeNivel),
            oNivelado.NomeEmLinguas,
            oNivelado.CaracCriature.mNivel.Nivel)
            , 32);
    }

    public bool Update()
    {
        switch (fase)
        {
            case FasesDoPassouDeNivel.mostrandoNivel:
                if (CommandReader.SubmitButtonDown(GameController.g.Manager.Control))
                {
                    GameController.g.HudM.Painel.EsconderMensagem();

                    gp = oNivelado.GerenteDeGolpes.VerificaGolpeDoNivel(
                        oNivelado.NomeID,oNivelado.CaracCriature.mNivel.Nivel
                        );

                    if (gp.Nome != nomesGolpes.nulo && !oNivelado.GerenteDeGolpes.TemEsseGolpe(gp.Nome))
                    {
                        contadorDeTempo = 0;
                        AprendoOuTentoAprender();
                    }
                    else
                    {
                        return true;
                    }
                }
            break;
            case FasesDoPassouDeNivel.aprendeuGolpe:
                contadorDeTempo += Time.deltaTime;
                if (contadorDeTempo > 0.5f)
                {
                    GameController.g.HudM.Painel.AtivarNovaMens(
                        string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.aprendeuGolpe),
                        oNivelado.NomeEmLinguas,
                        GolpeBase.NomeEmLinguas(gp.Nome))
                        , 30
                        );
                    GameController.g.HudM.P_Golpe.Aciona(PegaUmGolpeG2.RetornaGolpe(gp.Nome));
                    fase = FasesDoPassouDeNivel.painelAprendeuGolpeAberto;
                }
            break;
            case FasesDoPassouDeNivel.painelAprendeuGolpeAberto:
                if (CommandReader.SubmitButtonDown(GameController.g.Manager.Control))
                {
                    fase = FasesDoPassouDeNivel.finalizar;
                }
            break;
            case FasesDoPassouDeNivel.tentandoAprender:
                contadorDeTempo += Time.deltaTime;
                if (contadorDeTempo > 0.5f)
                {
                    HudManager hudM = GameController.g.HudM;
                    hudM.Painel.AtivarNovaMens(
                        string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.tentandoAprenderGolpe),
                        oNivelado.NomeEmLinguas,
                        GolpeBase.NomeEmLinguas(gp.Nome))
                        , 30
                        );
                    
                    
                    hudM.P_Golpe.Aciona(PegaUmGolpeG2.RetornaGolpe(gp.Nome));

                    //ActionManager.ModificarAcao(GameController.g.transform, GameController.g.HudM.H_Tenta.AcaoLocal);
                    hudM.H_Tenta.Aciona(oNivelado, gp.Nome,
                        string.Format(BancoDeTextos.RetornaFraseDoIdioma(ChaveDeTexto.precisaEsquecer), oNivelado.NomeEmLinguas)
                        , FechandoH_Tenta);
                    fase = FasesDoPassouDeNivel.emEspera;
                }
            break;
            case FasesDoPassouDeNivel.finalizar:
                GameController.g.HudM.Painel.EsconderMensagem();
                GameController.g.HudM.P_Golpe.gameObject.SetActive(false);
            return true;
        }
        return false;
    }

    void FechandoH_Tenta(bool aprendeu)
    {
        fase = FasesDoPassouDeNivel.painelAprendeuGolpeAberto;
    }

    void AprendoOuTentoAprender()
    {
        if (oNivelado.GerenteDeGolpes.meusGolpes.Count < 4)
        {
            fase = FasesDoPassouDeNivel.aprendeuGolpe;
            oNivelado.GerenteDeGolpes.meusGolpes.Add(PegaUmGolpeG2.RetornaGolpe(gp.Nome));
        }
        else
        {
            fase = FasesDoPassouDeNivel.tentandoAprender;
        }
    }
}