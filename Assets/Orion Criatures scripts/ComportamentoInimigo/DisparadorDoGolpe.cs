using UnityEngine;
using System.Collections;

public class DisparadorDoGolpe
{

    public static bool Dispara(CriatureBase meuCriatureBase,GameObject gameObject)
    {
        Atributos A = meuCriatureBase.CaracCriature.meusAtributos;
        GerenciadorDeGolpes ggg = meuCriatureBase.GerenteDeGolpes;
        IGolpeBase gg = ggg.meusGolpes[ggg.golpeEscolhido];
        
        if (gg.UltimoUso + gg.TempoDeReuso < Time.time && A.PE.Corrente >= gg.CustoPE)
        {
            AplicadorDeGolpe aplG = gameObject.AddComponent<AplicadorDeGolpe>();
            A.PE.Corrente -= gg.CustoPE;
            gg.UltimoUso = Time.time;
            aplG.esseGolpe = gg;

            GameController.g.HudM.AtualizaDadosDaHudVida(false);

            if (GameController.g.estaEmLuta)
                GameController.g.HudM.AtualizaDadosDaHudVida(true);
            
           // if(!GameController.g.estaEmLuta)
             //   //GameController.g.HudM.AtualizaHudHeroi(meuCriatureBase);

            return true;
        }
        else
            return false;
    }
}
