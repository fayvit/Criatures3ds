using UnityEngine;
using System.Collections;

public class AdicionaStatusSimples
{
    public static void InsereStatusSimples(CreatureManager levou,  StatusTemporarioBase S,int numStatus)
    {
        //int numStatus = StatusTemporarioBase.ContemStatus(TipoStatus.envenenado, C);
        CriatureBase C = levou.MeuCriatureBase;
        if (numStatus == -1)
        {
            InserindoNovoStatus(levou, C, S);
        }
        else
        {
            DatesForTemporaryStatus d = C.StatusTemporarios[numStatus];
            d.Quantificador = Mathf.Max(S.Dados.Quantificador, d.Quantificador + 1);
            d.TempoSignificativo += (15f / 14f)*S.Dados.TempoSignificativo;
        }
    }

    public static void InserindoNovoStatus(
        CreatureManager levou, 
        CriatureBase C, 
        StatusTemporarioBase S, bool eLoad = false)
    {
        C.StatusTemporarios.Add(S.Dados);        

        if (levou != null)
        {

            if (levou.name == "CriatureAtivo")
            {
                GameController.g.ContStatus.AdicionaStatusAoHeroi(S);
            }
            else
            {
                GameController.g.ContStatus.AdicionaStatusAoInimigo(S);

            }
        }
        else
            GameController.g.ContStatus.AdicionaStatusAoHeroi(S);
    }
}