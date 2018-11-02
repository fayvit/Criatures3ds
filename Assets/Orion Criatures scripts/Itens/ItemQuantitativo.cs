using UnityEngine;
using System.Collections;

public class ItemQuantitativo
{
    public static bool UsaItemDeRecuperacao(CriatureBase meuCriature)
    {
        Atributos A = meuCriature.CaracCriature.meusAtributos;
        if (A.PV.Corrente < A.PV.Maximo&&A.PV.Corrente>0)
            return true;
        else
            return false;
    }

    public static bool UsaItemDeEnergia(CriatureBase meuCriature)
    {
        
        Atributos A = meuCriature.CaracCriature.meusAtributos;
        if (A.PE.Corrente < A.PE.Maximo && A.PE.Corrente >= 0 && A.PV.Corrente>0)
            return true;
        else
            return false;
    }

    public static bool PrecisaDePerfeicao(CriatureBase meuCriature)
    {
        return UsaItemDeRecuperacao(meuCriature) || UsaItemDeEnergia(meuCriature) || meuCriature.StatusTemporarios.Count>0;
    }

    public static void RecuperaPV(Atributos meusAtributos, int tanto)
    {
        int contador = meusAtributos.PV.Corrente;
        int maximo = meusAtributos.PV.Maximo;

        if (contador + tanto < maximo)
            meusAtributos.PV.Corrente += tanto;
        else
            meusAtributos.PV.Corrente = meusAtributos.PV.Maximo;
    }

    public static void RecuperaPE(Atributos meusAtributos, int tanto)
    {
        int contador = meusAtributos.PE.Corrente;
        int maximo = meusAtributos.PE.Maximo;

        if (contador + tanto < maximo)
            meusAtributos.PE.Corrente += tanto;
        else
            meusAtributos.PE.Corrente = meusAtributos.PE.Maximo;
    }

    public static void AplicacaoDoItemComMenu(CharacterManager manager,CriatureBase C,int valor,System.Action umaAcao)
    {
        /*
        Atributos A = C.CaracCriature.meusAtributos;

        switch (Q)
        {
            case TipoQuantitativo.PV:
                RecuperaPV(A, valor);
            break;
            case TipoQuantitativo.PE:
                RecuperaPE(A, valor);
            break;
            case TipoQuantitativo.perfeicao:
                CriatureBase.EnergiaEVidaCheia(C);
            break;
        }*/
        
        PainelStatus ps = GameController.g.HudM.P_EscolheUsoDeItens;
        GameController.g.HudM.AtualizaDadosDaHudVida(false);
        GameController.g.StartCoroutine(
            ParticulaDeCoisasBoas.ParticulasMaisBotao(ps.GetComponent<RectTransform>(),
            ()=> {
                ps.ReligarMeusBotoes();
                umaAcao();
            })
            );

        ps.DesligarMeusBotoes();
        ps.BtnMeuOutro(manager.Dados.CriaturesAtivos.IndexOf(C));
    }
}

public enum TipoQuantitativo
{
    PV,
    PE,
    perfeicao
}
