using UnityEngine;
using System.Collections.Generic;

public class VerificaAplicaStatusEnvenenado : MonoBehaviour
{
    public static void VerificaAplicaStatus(CriatureBase atacante, CreatureManager cDoAtacado,IGolpeBase golpe,int dano)
    {
        CriatureBase atacado = cDoAtacado.MeuCriatureBase;

        if (VerificaAplicaStatusEnvenenado.VaiColocarStatus(
            golpe,
            atacante.CaracCriature.meusAtributos,
            atacado.CaracCriature.meusAtributos,
            atacado.CaracCriature.contraTipos[(int)nomeTipos.Veneno].Mod
            ))
        {

            Debug.Log("Aplicou Envenenamento");

            VerificaAplicaStatusEnvenenado.InsereStatus(cDoAtacado,
                new DatesForTemporaryStatus()
                {
                    Quantificador = dano,
                    TempoSignificativo = 50,
                    Tipo = TipoStatus.envenenado
                }
                );
        }
    }
    public static bool VaiColocarStatus(IGolpeBase ativa,Atributos bateu,Atributos levou,float contraTipoVeneno)
    {
        bool retorno = false;
        switch (ativa.Nome)
        {
            case nomesGolpes.agulhaVenenosa:
            case nomesGolpes.ondaVenenosa:
            case nomesGolpes.chuvaVenenosa:

                if (contraTipoVeneno > 0)
                {
                    float ff = ativa.PotenciaCorrente *
                        Mathf.Max(1,
                                  Random.Range(0.75f, 1f) * bateu.Poder.Corrente -
                                  Random.Range(0, 0.75f) * levou.Defesa.Corrente);
                    if (contraTipoVeneno * ff + Random.Range(0, 100) > 80)
                        retorno = true;
                }

            break;
        }

        return retorno;
    }

    public static void InsereStatus(CreatureManager levou, DatesForTemporaryStatus dadosDoStatus)
    {
        InsereStatus(levou, levou.MeuCriatureBase, dadosDoStatus);
    }

    /*
    public static void InserindoNovoStatus(CreatureManager levou, CriatureBase C, DatesForTemporaryStatus dadosDoStatus,bool eLoad = false)
    {
        C.StatusTemporarios.Add(dadosDoStatus);

        

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
    }*/

    public static void InsereStatus(CreatureManager levou,CriatureBase C, DatesForTemporaryStatus dadosDoStatus)
    {
        int numStatus = StatusTemporarioBase.ContemStatus(TipoStatus.envenenado,C);
        
        if (numStatus == -1)
        {
            StatusTemporarioBase S = new Envenenado()
            {
                Dados = dadosDoStatus,
                CDoAfetado = levou,
                OAfetado = C
            };

            AdicionaStatusSimples.InserindoNovoStatus(levou, C, S);
            //InserindoNovoStatus(levou, C, dadosDoStatus);
        }
        else
        {
            DatesForTemporaryStatus d = C.StatusTemporarios[numStatus];
            d.Quantificador = Mathf.Max(dadosDoStatus.Quantificador, d.Quantificador + 1);
            d.TempoSignificativo *= (14f / 15f);
        }
    }
}
